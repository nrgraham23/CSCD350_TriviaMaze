/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - NullDoor, represents the walls of the maze.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class NullDoor : IDoor {

        //=====================================================================
        
        public bool Enter() {
            return false;
        }

        //=====================================================================
        
        public String GetFileName() {
            return "door_wall.png";
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

        //=====================================================================

        public bool IsWall() {
            return true;
        }
    }
}
