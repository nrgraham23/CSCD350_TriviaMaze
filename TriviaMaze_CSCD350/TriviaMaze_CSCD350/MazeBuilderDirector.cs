using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    class MazeBuilderDirector {
        public Maze Construct() {
            MazeBuilder builder = new MazeBuilder();
            builder.MakeWalls();
            builder.InitDoors();

            return builder.GetMaze();
        }
    }
}
