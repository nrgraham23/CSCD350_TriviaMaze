using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    
    class MazeBuilder {
        private Maze maze;

        public MazeBuilder() {
            this.maze = new Maze();
        }

        //=====================================================================

        public void MakeWalls() {
            Floor mazeFloor = this.maze.GetFloor();

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

        //=====================================================================
        //initializes walls by sharing walls between rooms.
        public void InitDoors() {
            Floor floor = maze.GetFloor();
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

        //=====================================================================

        public Maze GetMaze() {
            return this.maze;
        }
    }
}
