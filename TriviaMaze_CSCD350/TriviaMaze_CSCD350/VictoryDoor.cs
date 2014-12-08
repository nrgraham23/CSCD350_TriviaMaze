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

    [Serializable]
    public class VictoryDoor : IDoor {

        //=====================================================================
        
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
