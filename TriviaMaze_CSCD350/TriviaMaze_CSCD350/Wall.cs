/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * This class is a container class for a door to make changing doors
 * when they are locked or opened much simpler.  Since each room shares
 * doors with adjacent rooms, it is much simpler to create a container
 * class that adjacent rooms can each share.  This way when you need to
 * change a door in one room, you are also changing the door in the adjacent
 * room without having to update two room pointers.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    public class Wall {
        private IDoor door;

        //=====================================================================
        //Comment- Constructor
        public Wall() {
            this.door = new OpenedDoor();  //this is where you change to open door if you want all doors to be open
        }

        //=====================================================================
        //Comment-
        public IDoor GetDoor() {
            return this.door;
        }

        //=====================================================================
        //Comment-
        public void SetDoor(IDoor door) {
            this.door = door;
        }
    }
}
