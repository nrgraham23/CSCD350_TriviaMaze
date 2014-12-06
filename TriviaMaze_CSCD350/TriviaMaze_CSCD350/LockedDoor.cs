/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - LockedDoor
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class LockedDoor : IDoor {

        //=====================================================================
        //Comment-
        public LockedDoor() {
        }

        //=====================================================================
        //Comment-
        public bool Enter() {
            return false;
        }

        //=====================================================================
        //Comment-
        public String GetFileName() {
            return "door_locked.png";
        }

        //=====================================================================
        //Comment-
        public bool Passable() {
            return false;
        }

        //=====================================================================
        //Comment-
        public int FloorChange() {
            return 0;
        }

        //=====================================================================

        public bool IsOpen() {
            return false;
        }
    }
}
