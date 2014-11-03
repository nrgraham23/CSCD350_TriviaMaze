using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class Maze {
        Floor mazeFloor;
        Room curRoom;
        Point exit;
        Point curPoint;

        public Maze() {
            this.mazeFloor = new Floor();
            PlaceExit();
            PickStart();
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
    }
}
