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
        
        public bool Enter() {
            return false;
        }

        //=====================================================================
        
        public String GetFileName() {
            return "door_locked.png";
        }

        //=====================================================================
        
        public bool Passable() {
            return false;
        }

        //=====================================================================
        
        public int FloorChange() {
            return 0;
        }

        //=====================================================================

        public bool IsOpen() {
            return false;
        }


        public bool IsWall() {
            return false;
        }
    }
}
