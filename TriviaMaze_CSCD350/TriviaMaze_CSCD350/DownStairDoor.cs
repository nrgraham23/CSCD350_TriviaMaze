/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - DownStairDoor 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class DownStairDoor : IDoor {

        //=====================================================================
        //Comment-
        public bool Enter() {
            return true;
        }

        //=====================================================================
        //Comment-
        public string GetFileName() {
            return "door_down.png";
        }

        //=====================================================================
        //Comment-
        public bool Passable() {
            return true;
        }

        //=====================================================================
        //Comment-
        public int FloorChange() {
            return -1;
        }
    }
}
