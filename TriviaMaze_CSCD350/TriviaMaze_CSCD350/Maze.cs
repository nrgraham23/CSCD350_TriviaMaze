using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class Maze : IObservable<Maze> {
        private Floor mazeFloor;
        private Room curRoom;
        private Point exit;
        private Point curPoint;
        private List<IObserver<Maze>> observers;

        public Maze() {
            this.mazeFloor = new Floor();
            this.observers = new List<IObserver<Maze>>();
            PlaceExit();
            PickStart();
            MakeWalls();
        }

        private void PlaceExit() {
            Random rand = new Random();
            int size = mazeFloor.GetSize();

            int exitRow = rand.Next(size);
            exit = new Point(exitRow, size - 1);
        }

        private void PickStart() {
            Random rand = new Random();
            Point startRoom = new Point(rand.Next(mazeFloor.GetSize()), 0);
            
            curRoom = mazeFloor.GetRoom(startRoom);
            curPoint = startRoom;
        }

        private void MakeWalls() {
            int size = this.mazeFloor.GetSize();
            for (int i = 0; i < size; i++) {
                mazeFloor.GetRoom(new Point(0, i)).SetNDoor(new NullDoor());
                mazeFloor.GetRoom(new Point(i, size - 1)).SetEDoor(new NullDoor());
                mazeFloor.GetRoom(new Point(size - 1, i)).SetSDoor(new NullDoor());
                mazeFloor.GetRoom(new Point(i, 0)).SetWDoor(new NullDoor());
            }
        }

        public bool IsSolvable() {
            int size = mazeFloor.GetSize();
            bool[,] visitedMap = new bool[size, size]; //keep track of visited rooms
            int testYCoord = this.curPoint.getRow();
            int testXCoord = this.curPoint.getCol();

            return RecurseSearch(visitedMap, testYCoord, testXCoord);
        }
        //recursive part of IsSolvable
        private bool RecurseSearch(bool[,] visitedMap, int testYCoord, int testXCoord) {
            Room curTestRoom = mazeFloor.GetRoom(new Point(testYCoord, testXCoord));

            if (testYCoord == exit.getRow() && testXCoord == exit.getCol()) { //check for exit first
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

        public void DisplayCurRoom() {
            observers[0].OnNext(this);
        }

        public void DisplayMiniMap() {
            //TODO display the minimap
        }

        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        public IDisposable Subscribe(IObserver<Maze> observer) {
            if (!this.observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        //allows for the removal of an observer
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        private class Unsubscriber : IDisposable {
            private List<IObserver<Maze>> _observers;
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
