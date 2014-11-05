using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class GameCore {
        private Maze maze;

        public GameCore() {
            this.maze = new Maze();
        }

        public void SetLDoorImage(String fileName) {
            
        }

        public void SetMDoorImage(String fileName) {

        }

        public void SetRDoorImage(String fileName) {

        }

        //should only ever be used for subscription purposes
        public Maze GetMaze() {
            return this.maze;
        }
    }
}
