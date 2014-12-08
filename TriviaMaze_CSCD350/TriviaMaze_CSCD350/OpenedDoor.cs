/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - OpenedDoor, 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class OpenedDoor : IDoor {

        //=====================================================================
        
        public OpenedDoor() {

        }

        //=====================================================================
        //Comment-
        public bool Enter() {
            return true;
        }

        //=====================================================================
        //Comment-
        public String GetFileName() {
            return "door_open.png";
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

        //=====================================================================

        public bool IsOpen() {
            return true;
        }


        public bool IsWall() {
            return false;
        }
    }
}
