using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class GameCore {
        private Maze maze;

        public GameCore() {
            MazeBuilderDirector director = new MazeBuilderDirector();
            
            this.maze = director.Construct();
        }

        public bool RightDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();

            if (dirEntered == 'n') {
                return this.maze.MoveWest();
            } else if (dirEntered == 'w') {
                return this.maze.MoveSouth();
            } else if (dirEntered == 's') {
                return this.maze.MoveEast();
            } else if (dirEntered == 'e') {
                return this.maze.MoveNorth();
            }
            return false;
        }

        public bool CenterDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();

            if (dirEntered == 'n') {
                return this.maze.MoveSouth();
            } else if (dirEntered == 'w') {
                return this.maze.MoveEast();
            } else if (dirEntered == 's') {
                return this.maze.MoveNorth();
            } else if (dirEntered == 'e') {
                return this.maze.MoveWest();
            }
            return false;
        }

        public bool LeftDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();

            if (dirEntered == 'n') {
                return this.maze.MoveEast();
            } else if (dirEntered == 'w') {
                return this.maze.MoveNorth();
            } else if (dirEntered == 's') {
                return this.maze.MoveWest();
            } else if (dirEntered == 'e') {
                return this.maze.MoveSouth();
            }
            return false;
        }

        public bool BackDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();

            if (dirEntered == 'n') {
                return this.maze.MoveNorth();
            } else if (dirEntered == 'w') {
                return this.maze.MoveWest();
            } else if (dirEntered == 's') {
                return this.maze.MoveSouth();
            } else if (dirEntered == 'e') {
                return this.maze.MoveEast();
            }
            return false;
        }

        //should only ever be used for subscription purposes
        public Maze GetMaze() {
            return this.maze;
        }
    }
}
