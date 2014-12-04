/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - VictoryDoor, 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class VictoryDoor : IDoor {

        //=====================================================================
        //Comment-
        public bool Enter() {
            MainWindow.GameWon();
            return false;
        }

        //=====================================================================
        //Comment-
        public string GetFileName() {
            return "door_victory.png";
        }

        //=====================================================================
        //Comment-
        public bool Passable() {
            return true;
        }

        //=====================================================================
        //Comment-
        public int FloorChange() {
            return 0;
        }
    }
}
