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
        //initialezes doors by sharing doors between rooms.
        public void InitDoors() {
            Floor floor = maze.GetFloor();
            Room curRoom;

            for (int i = 0; i < floor.GetSize(); i++) {
                for (int j = 0; j < floor.GetSize(); j++) {
                    curRoom = floor.GetRoom(new Point(i, j));
                    if (i != floor.GetSize() - 1) {  //south doors
                        curRoom.SetSDoor(new ClosedDoor());
                    }
                    if (j < floor.GetSize() - 1) {  //east doors
                        curRoom.SetEDoor(new ClosedDoor());
                    }
                    if (i != 0) {  //north doors
                        curRoom.SetNDoor(floor.GetRoom(new Point(i - 1, j)).GetSDoor());
                    }
                    if (j != 0) {  //west doors
                        curRoom.SetWDoor(floor.GetRoom(new Point(i, j - 1)).GetEDoor());
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
