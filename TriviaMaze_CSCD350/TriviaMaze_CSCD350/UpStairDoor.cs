/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - UpStairDoor, 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class UpStairDoor : IDoor {

        //=====================================================================
        
        public bool Enter() {
            return true;
        }

        //=====================================================================
        
        public string GetFileName() {
            return "door_up.png";
        }

        //=====================================================================
        
        public bool Passable() {
            return true;
        }

        //=====================================================================
        
        public int FloorChange() {
            return 1;
        }


        public bool IsOpen() {
            return true;
        }


        public bool IsWall() {
            return false;
        }
    }
}
