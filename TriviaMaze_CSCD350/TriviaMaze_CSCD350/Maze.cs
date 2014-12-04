using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class Maze : IObservable<Maze> {
        private Floor mazeFloor;
        private Room curRoom;
        private Point exit;
        private Point curPoint;
        private IDoor clickedDoor;

        [NonSerialized]
        private List<IObserver<Maze>> observers;

        public Maze() {
            this.mazeFloor = new Floor();
            this.observers = new List<IObserver<Maze>>();
            PlaceExit();
            PickStart();
        }

        

        //=====================================================================

        private void PlaceExit() {
            Random rand = new Random();
            int size = mazeFloor.GetSize();

            int exitRow = rand.Next(size);
            exit = new Point(exitRow, size - 1);
        }

        //=====================================================================

        private void PickStart() {
            Random rand = new Random();
            Point startRoom = new Point(rand.Next(mazeFloor.GetSize()), 0);
            
            curRoom = mazeFloor.GetRoom(startRoom);
            curRoom.SetEnteredFrom('w');
            curPoint = startRoom;
        }

        //=====================================================================

        public bool IsSolvable() {
            int size = mazeFloor.GetSize();
            bool[,] visitedMap = new bool[size, size]; //keep track of visited rooms
            int testYCoord = this.curPoint.GetRow();
            int testXCoord = this.curPoint.GetCol();

            return RecurseSearch(visitedMap, testYCoord, testXCoord);
        }
        //recursive part of IsSolvable
        private bool RecurseSearch(bool[,] visitedMap, int testYCoord, int testXCoord) {
            Room curTestRoom = mazeFloor.GetRoom(new Point(testYCoord, testXCoord));

            if (testYCoord == exit.GetRow() && testXCoord == exit.GetCol()) { //check for exit first
                return true;
            }

            visitedMap[testYCoord, testXCoord] = true; //update visited

            if (curTestRoom.GetNPassable() && !visitedMap[testYCoord - 1, testXCoord]) { //try north
                if (RecurseSearch(visitedMap, (testYCoord - 1), testXCoord)) {
                    return true;
                }
            }
            if (curTestRoom.GetEPassable() && !visitedMap[testYCoord, testXCoord + 1]) { //try east
                if (RecurseSearch(visitedMap, testYCoord, (testXCoord + 1))) {
                    return true;
                }
            }
            if (curTestRoom.GetSPassable() && !visitedMap[testYCoord + 1, testXCoord]) { //try south
                if (RecurseSearch(visitedMap, (testYCoord + 1), testXCoord)) {
                    return true;
                }
            }
            if (curTestRoom.GetWPassable() && !visitedMap[testYCoord, testXCoord - 1]) { //try west
                if (RecurseSearch(visitedMap, testYCoord, (testXCoord - 1))) {
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
        public Floor GetFloor() {
            return this.mazeFloor;
        }

        //=====================================================================

        public bool MoveNorth() {
            if (curRoom.GetNDoor().Enter()) {
                this.curPoint.SetRow(this.curPoint.GetRow() - 1);
                this.curRoom = this.mazeFloor.GetRoom(curPoint);
                this.curRoom.SetEnteredFrom('s');
                observers[0].OnNext(this);
                return true;
            }
            return false;
        }

        //=====================================================================

        public bool MoveEast() {
            if (curRoom.GetEDoor().Enter()) {
                this.curPoint.SetCol(this.curPoint.GetCol() + 1);
                this.curRoom = this.mazeFloor.GetRoom(curPoint);
                this.curRoom.SetEnteredFrom('w');
                observers[0].OnNext(this);
                return true;
            }
            return false;
        }

        //=====================================================================

        public bool MoveSouth() {
            if (curRoom.GetSDoor().Enter()) {
                this.curPoint.SetRow(this.curPoint.GetRow() + 1);
                this.curRoom = this.mazeFloor.GetRoom(curPoint);
                this.curRoom.SetEnteredFrom('n');
                observers[0].OnNext(this);
                return true;
            }
            return false;
        }

        //=====================================================================

        public bool MoveWest() {
            if (curRoom.GetWDoor().Enter()) {
                this.curPoint.SetCol(this.curPoint.GetCol() - 1);
                this.curRoom = this.mazeFloor.GetRoom(curPoint);
                this.curRoom.SetEnteredFrom('e');
                observers[0].OnNext(this);
                return true;
            }
            return false;
        }

        //=====================================================================

        public void Update() {
            observers[0].OnNext(this);
        }

        //=====================================================================

        public Point GetCurPoint() {
            return this.curPoint;
        }
        public Point GetExit() {
            return this.exit;
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
        //allows for the removal of an observer
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
