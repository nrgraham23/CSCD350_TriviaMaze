/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - MazeBuilderDirector, 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    class MazeBuilderDirector {

        //=====================================================================
        //if 0 is passed no extra doors are closed, if 1 is passed doors are closed to add difficulty
        public Maze Construct(int difficulty) {
            MazeBuilder builder = new MazeBuilder();
            bool validMaze = false;

            builder.MakeWalls();

            while (!validMaze) {
                builder.InitDoors();
                builder.PlaceStairs();
                builder.PlaceExit();
                builder.PlaceStart();
                if (difficulty == 1) {
                    builder.CloseDoors(.1);
                    if (builder.GetMaze().IsSolvable()) {
                        validMaze = true;
                    }
                } else if (difficulty == 2) {
                    builder.CloseDoors(.2);
                    if (builder.GetMaze().IsSolvable()) {
                        validMaze = true;
                    }
                } else {
                    validMaze = true;
                }
            }

            return builder.GetMaze();
        }
    }
}
