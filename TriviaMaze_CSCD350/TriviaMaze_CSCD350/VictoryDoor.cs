using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class VictoryDoor : IDoor {

        public bool Enter() {
            MainWindow.GameWon();
            return false;
        }

        //=====================================================================

        public string GetFileName() {
            return "door_victory.png";
        }

        //=====================================================================

        public bool Passable() {
            return true;
        }

        //=====================================================================

        public int FloorChange() {
            return 0;
        }
    }
}
