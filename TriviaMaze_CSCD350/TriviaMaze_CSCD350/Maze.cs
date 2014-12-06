/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - Maze, this is the highest level of the core
 * of the game, maze is the gateway to most of the classes.
 * As it holds floors, current pos, current room, and
 * current clicked door.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class Maze : IObservable<Maze> {
        private Floor[] mazeFloors;
        private Room curRoom;
        private Point curPoint;
        private int curFloorNum;
        private Wall clickedWall;
        private int numFloors;

        [NonSerialized]
        private List<IObserver<Maze>> observers;

        //=====================================================================

        public Maze() {
            this.observers = new List<IObserver<Maze>>();
            this.numFloors = 5;
            this.curFloorNum = 0;
            InitFloors();
        }

        //=====================================================================

        private void InitFloors() {
            this.mazeFloors = new Floor[this.numFloors];
            for (int i = 0; i < this.numFloors; i++) {
                this.mazeFloors[i] = new Floor();
            }
        }

        //=====================================================================

        public bool IsSolvable() {
            for (int i = this.curFloorNum; i < this.numFloors; i++) {
                int size = mazeFloors[i].GetSize();
                int testYCoord, testXCoord;
                bool[,] visitedMap = new bool[size, size]; //keep track of visited rooms
                if (i == this.curFloorNum) {
                    testYCoord = this.curPoint.GetRow();
                    testXCoord = this.curPoint.GetCol();
                }
                else {
                    testYCoord = mazeFloors[i].GetEntry().GetRow();
                    testXCoord = mazeFloors[i].GetEntry().GetCol();
                }
                int exitY = mazeFloors[i].GetExit().GetRow();
                int exitX = mazeFloors[i].GetExit().GetCol();

                if (!RecurseSearch(visitedMap, testYCoord, testXCoord, i, exitY, exitX)) {
                    return false;
                }
            }
            return true;
        }

        //=====================================================================
        //recursive part of IsSolvable
        private bool RecurseSearch(bool[,] visitedMap, int testYCoord, int testXCoord, int testZCoord, int exitY, int exitX) {
            Room curTestRoom = mazeFloors[testZCoord].GetRoom(new Point(testYCoord, testXCoord));

            if (testYCoord == exitY && testXCoord == exitX) { //check for exit first
                return true;
            }

            visitedMap[testYCoord, testXCoord] = true; //update visited

            if (curTestRoom.GetNPassable() && !visitedMap[testYCoord - 1, testXCoord]) { //try north
                if (RecurseSearch(visitedMap, (testYCoord - 1), testXCoord, testZCoord, exitY, exitX)) {
                    return true;
                }
            }
            if (curTestRoom.GetEPassable() && !visitedMap[testYCoord, testXCoord + 1]) { //try east
                if (RecurseSearch(visitedMap, testYCoord, (testXCoord + 1), testZCoord, exitY, exitX)) {
                    return true;
                }
            }
            if (curTestRoom.GetSPassable() && !visitedMap[testYCoord + 1, testXCoord]) { //try south
                if (RecurseSearch(visitedMap, (testYCoord + 1), testXCoord, testZCoord, exitY, exitX)) {
                    return true;
                }
            }
            if (curTestRoom.GetWPassable() && !visitedMap[testYCoord, testXCoord - 1]) { //try west
                if (RecurseSearch(visitedMap, testYCoord, (testXCoord - 1), testZCoord, exitY, exitX)) {
                    return true;
                }
            }
            visitedMap[testYCoord, testXCoord] = false; //update visited
            return false;
        }

        //=====================================================================

        public Room GetCurRoom() {
            return this.curRoom;
        }
        public Floor GetFloor(int floorNum) {
            return this.mazeFloors[floorNum];
        }
        public int GetNumFloors() {
            return this.numFloors;
        }
        public Point GetCurPoint() {
            return this.curPoint;
        }
        public int GetCurFloor() {
            return this.curFloorNum;
        }

        //=====================================================================

        public void SetCurRoom(Room curRoom) {
            this.curRoom = curRoom;
        }
        public void SetCurPoint(Point curPoint) {
            this.curPoint = curPoint;
        }

        //=====================================================================

        public bool MoveNorth() {
            if (curRoom.GetNDoor().Enter()) {
                this.curFloorNum = curFloorNum + curRoom.GetNDoor().FloorChange();
                this.curPoint.SetRow(this.curPoint.GetRow() - 1);
                this.curRoom = this.mazeFloors[this.curFloorNum].GetRoom(curPoint);
                this.curRoom.SetEnteredFrom('s');
                observers[0].OnNext(this);
                return true;
            }
            else {
                this.clickedWall = this.curRoom.GetNWall();
            }
            return false;
        }

        //=====================================================================

        public bool MoveEast() {
            if (curRoom.GetEDoor().Enter()) {
                this.curFloorNum = curFloorNum + curRoom.GetEDoor().FloorChange();
                this.curPoint.SetCol(this.curPoint.GetCol() + 1);
                this.curRoom = this.mazeFloors[this.curFloorNum].GetRoom(curPoint);
                this.curRoom.SetEnteredFrom('w');
                observers[0].OnNext(this);
                return true;
            }
            else {
                this.clickedWall = this.curRoom.GetEWall();
            }
            return false;
        }

        //=====================================================================

        public bool MoveSouth() {
            if (curRoom.GetSDoor().Enter()) {
                this.curFloorNum = curFloorNum + curRoom.GetSDoor().FloorChange();
                this.curPoint.SetRow(this.curPoint.GetRow() + 1);
                this.curRoom = this.mazeFloors[this.curFloorNum].GetRoom(curPoint);
                this.curRoom.SetEnteredFrom('n');
                observers[0].OnNext(this);
                return true;
            }
            else {
                this.clickedWall = this.curRoom.GetSWall();
            }
            return false;
        }

        //=====================================================================

        public bool MoveWest() {
            if (curRoom.GetWDoor().Enter()) {
                this.curFloorNum = curFloorNum + curRoom.GetWDoor().FloorChange();
                this.curPoint.SetCol(this.curPoint.GetCol() - 1);
                this.curRoom = this.mazeFloors[this.curFloorNum].GetRoom(curPoint);
                this.curRoom.SetEnteredFrom('e');
                observers[0].OnNext(this);
                return true;
            }
            else {
                this.clickedWall = this.curRoom.GetWWall();
            }
            return false;
        }

        //=====================================================================
        //for use when a question is answered wrong
        public void LockDoor() {
            this.clickedWall.SetDoor(new LockedDoor());
            if (!this.IsSolvable()) {
                MainWindow.GameLost();
            }
            Update();
        }

        //=====================================================================
        //for use when a question is answered correctly
        public void OpenDoor() {
            this.clickedWall.SetDoor(new OpenedDoor());
            Update();
        }

        //=====================================================================

        public void Update() {
            observers[0].OnNext(this);
        }

        //=====================================================================
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        public IDisposable Subscribe(IObserver<Maze> observer) {
            if (this.observers == null) {
                this.observers = new List<IObserver<Maze>>();
            }
            if (!this.observers.Contains(observer)) {
                observers.Add(observer);
                observers[0].OnNext(this);
            }
            return new Unsubscriber(observers, observer);
        }

        //=====================================================================
        //Comment- allows for the removal of an observer
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        private class Unsubscriber : IDisposable {
            [NonSerialized]
            private List<IObserver<Maze>> _observers;
            [NonSerialized]
            private IObserver<Maze> _observer;

            public Unsubscriber(List<IObserver<Maze>> observers, IObserver<Maze> observer) {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose() {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
