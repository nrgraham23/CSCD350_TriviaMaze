/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - MazeBuilder, Main purpose of this class
 * is on start up of the game the maze is created
 * in here.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    
    class MazeBuilder {
        private Maze maze;

        //=====================================================================
        
        public MazeBuilder() {
            this.maze = new Maze();
        }

        //=====================================================================
        
        public void MakeWalls() {
            for (int floorNum = 0; floorNum < this.maze.GetNumFloors(); floorNum++) {
                Floor mazeFloor = this.maze.GetFloor(floorNum);

                int size = mazeFloor.GetSize();
                for (int i = 0; i < size; i++) {
                    mazeFloor.GetRoom(new Point(0, i)).SetNWall(new Wall());
                    mazeFloor.GetRoom(new Point(0, i)).SetNDoor(new NullDoor());
                    mazeFloor.GetRoom(new Point(i, size - 1)).SetEWall(new Wall());
                    mazeFloor.GetRoom(new Point(i, size - 1)).SetEDoor(new NullDoor());
                    mazeFloor.GetRoom(new Point(size - 1, i)).SetSWall(new Wall());
                    mazeFloor.GetRoom(new Point(size - 1, i)).SetSDoor(new NullDoor());
                    mazeFloor.GetRoom(new Point(i, 0)).SetWWall(new Wall());
                    mazeFloor.GetRoom(new Point(i, 0)).SetWDoor(new NullDoor());
                }
            }
        }

        //=====================================================================
        //initializes walls by sharing walls between rooms.
        public void InitDoors() {
            for (int floorNum = 0; floorNum < this.maze.GetNumFloors(); floorNum++) {
                Floor floor = maze.GetFloor(floorNum);
                Room curRoom;

                for (int i = 0; i < floor.GetSize(); i++) {
                    for (int j = 0; j < floor.GetSize(); j++) {
                        curRoom = floor.GetRoom(new Point(i, j));
                        if (i != floor.GetSize() - 1) {  //south walls
                            curRoom.SetSWall(new Wall());
                        }
                        if (j < floor.GetSize() - 1) {  //east walls
                            curRoom.SetEWall(new Wall());
                        }
                        if (i != 0) {  //north walls
                            curRoom.SetNWall(floor.GetRoom(new Point(i - 1, j)).GetSWall());
                        }
                        if (j != 0) {  //west walls
                            curRoom.SetWWall(floor.GetRoom(new Point(i, j - 1)).GetEWall());
                        }
                    }
                }
            }
        }

        //=====================================================================
        //as part of setting the stairs, the entry and exit point for each floor is set
        public void PlaceStairs() {
            int upY, upX, downY, downX, direction;
            int floorSize = this.maze.GetFloor(0).GetSize();
            Random rand = new Random();
            downX = downY = -1;

            for (int i = 0; i < this.maze.GetNumFloors() - 1; i++) {
                if(downX != -1 && downY != -1) {
                    SetFloorEntry(i, downY, downX);
                }
                do {
                    upY = rand.Next(floorSize - 2) + 1;
                    upX = rand.Next(floorSize - 2) + 1;
                } while (upY == downY && upX == downX);

                direction = rand.Next(4);

                if (direction == 0) {
                    SetNorthStairs(i, upY, upX);
                } else if (direction == 1) {
                    SetEastStairs(i, upY, upX);
                } else if (direction == 2) {
                    SetSouthStairs(i, upY, upX);
                } else {
                    SetWestStairs(i, upY, upX);
                }

                SetFloorExit(i, upY, upX);

                downX = upX;
                downY = upY;
            }
            SetFloorEntry(4, downY, downX); //set top floor's entry
        }

        //=====================================================================

        private void SetFloorEntry(int floorNum, int row, int col) {
            this.maze.GetFloor(floorNum).SetEntry(new Point(row, col));
        }

        //=====================================================================

        private void SetFloorExit(int floorNum, int row, int col) {
            this.maze.GetFloor(floorNum).SetExit(new Point(row, col));
        }

        //=====================================================================
        
        private void SetNorthStairs(int floorNum, int y, int x) {
            this.maze.GetFloor(floorNum).GetRoom(new Point(y, x)).SetNDoor(new UpStairDoor());
            this.maze.GetFloor(floorNum).GetRoom(new Point(y - 1, x)).SetSWall(new Wall());
            this.maze.GetFloor(floorNum).GetRoom(new Point(y - 1, x)).SetSDoor(new NullDoor());

            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y - 1, x)).SetSWall(new Wall());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y - 1, x)).SetSDoor(new DownStairDoor());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x)).SetNWall(new Wall());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x)).SetNDoor(new NullDoor());
        }

        //=====================================================================
        
        private void SetEastStairs(int floorNum, int y, int x) {
            this.maze.GetFloor(floorNum).GetRoom(new Point(y, x)).SetEDoor(new UpStairDoor());
            this.maze.GetFloor(floorNum).GetRoom(new Point(y, x + 1)).SetWWall(new Wall());
            this.maze.GetFloor(floorNum).GetRoom(new Point(y, x + 1)).SetWDoor(new NullDoor());

            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x + 1)).SetWWall(new Wall());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x + 1)).SetWDoor(new DownStairDoor());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x)).SetEWall(new Wall());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x)).SetEDoor(new NullDoor());
        }

        //=====================================================================
        
        private void SetSouthStairs(int floorNum, int y, int x) {
            this.maze.GetFloor(floorNum).GetRoom(new Point(y, x)).SetSDoor(new UpStairDoor());
            this.maze.GetFloor(floorNum).GetRoom(new Point(y + 1, x)).SetNWall(new Wall());
            this.maze.GetFloor(floorNum).GetRoom(new Point(y + 1, x)).SetNDoor(new NullDoor());

            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y + 1, x)).SetNWall(new Wall());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y + 1, x)).SetNDoor(new DownStairDoor());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x)).SetSWall(new Wall());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x)).SetSDoor(new NullDoor());
        }

        //=====================================================================
        
        private void SetWestStairs(int floorNum, int y, int x) {
            this.maze.GetFloor(floorNum).GetRoom(new Point(y, x)).SetWDoor(new UpStairDoor());
            this.maze.GetFloor(floorNum).GetRoom(new Point(y, x - 1)).SetEWall(new Wall());
            this.maze.GetFloor(floorNum).GetRoom(new Point(y, x - 1)).SetEDoor(new NullDoor());

            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x - 1)).SetEWall(new Wall());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x - 1)).SetEDoor(new DownStairDoor());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x)).SetWWall(new Wall());
            this.maze.GetFloor(floorNum + 1).GetRoom(new Point(y, x)).SetWDoor(new NullDoor());
        }

        //=====================================================================
        
        public void PlaceExit() {
            Random rand = new Random();
            int size = this.maze.GetFloor(0).GetSize();

            int exitRow = rand.Next(size);
            Point exitPoint = new Point(exitRow, size - 1);

            this.maze.GetFloor(this.maze.GetNumFloors() - 1).GetRoom(exitPoint).SetEDoor(new VictoryDoor());
            this.maze.GetFloor(this.maze.GetNumFloors() - 1).SetExit(exitPoint);
        }

        //=====================================================================
        
        public void PlaceStart() {
            Random rand = new Random();
            Point startRoom = new Point(rand.Next(this.maze.GetFloor(0).GetSize()), 0);

            this.maze.SetCurRoom(this.maze.GetFloor(0).GetRoom(startRoom));
            this.maze.GetCurRoom().SetEnteredFrom('w');
            this.maze.GetCurRoom().SetVisited(true);
            this.maze.SetCurPoint(startRoom);
            this.maze.GetFloor(0).SetEntry(startRoom);
        }

        //=====================================================================

        public void CloseDoors(double percentToClose) {
            Random rand = new Random();
            char directionToClose; 
            int row, col, floor;
            int numOfClosed = CalcNumDoorClose(percentToClose);
            int mazeSize = this.maze.GetFloor(0).GetSize();

            for (int i = 0; i < numOfClosed; i++) {
                row = rand.Next(mazeSize);
                col = rand.Next(mazeSize);
                floor = rand.Next(mazeSize);

                directionToClose = GetRandomDirection();

                if (directionToClose == 'n') {
                    if (this.maze.GetFloor(floor).GetRoom(new Point(row, col)).GetNPassable()
                        && this.maze.GetFloor(floor).GetRoom(new Point(row, col)).GetNDoor().FloorChange() == 0) { //if its not a wall or victory door and not a stairs
                            this.maze.GetFloor(floor).GetRoom(new Point(row, col)).SetNDoor(new NullDoor());
                    } else {
                        i--;
                    }
                } else if (directionToClose == 'e') {
                    if (this.maze.GetFloor(floor).GetRoom(new Point(row, col)).GetEPassable()
                        && this.maze.GetFloor(floor).GetRoom(new Point(row, col)).GetEDoor().FloorChange() == 0) { //if its not a wall or victory door and not a stairs
                        this.maze.GetFloor(floor).GetRoom(new Point(row, col)).SetEDoor(new NullDoor());
                    } else {
                        i--;
                    }
                } else if (directionToClose == 's') {
                    if (this.maze.GetFloor(floor).GetRoom(new Point(row, col)).GetSPassable()
                        && this.maze.GetFloor(floor).GetRoom(new Point(row, col)).GetSDoor().FloorChange() == 0) { //if its not a wall or victory door and not a stairs
                        this.maze.GetFloor(floor).GetRoom(new Point(row, col)).SetSDoor(new NullDoor());
                    } else {
                        i--;
                    }
                } else { //west
                    if (this.maze.GetFloor(floor).GetRoom(new Point(row, col)).GetWPassable()
                        && this.maze.GetFloor(floor).GetRoom(new Point(row, col)).GetWDoor().FloorChange() == 0) { //if its not a wall or victory door and not a stairs
                        this.maze.GetFloor(floor).GetRoom(new Point(row, col)).SetWDoor(new NullDoor());
                    } else {
                        i--;
                    }
                }
            }
        }

        //=====================================================================

        private int CalcNumDoorClose(double percentToClose) {
            int mazeSize = this.maze.GetFloor(0).GetSize();
            double totalRooms = mazeSize * mazeSize * mazeSize;
            return (int)(totalRooms * percentToClose);
        }

        //=====================================================================

        private char GetRandomDirection() {
            Random rand = new Random();
            int direction = rand.Next(4);

            if (direction == 0) {
                return 'n';
            } else if (direction == 1) {
                return 'e';
            } else if (direction == 2) {
                return 's';
            } else {
                return 'w';
            }
        }

        //=====================================================================
        
        public Maze GetMaze() {
            return this.maze;
        }
    }
}
