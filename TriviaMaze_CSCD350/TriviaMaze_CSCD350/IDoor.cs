/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - IDoor , interface for all doors.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    public interface IDoor {

        bool Enter(); //returns true if the user can enter the door 
        String GetFileName(); 
        bool Passable(); //returns true if the door is not locked or a wall
        int FloorChange(); //returns an int to be added to the current floor number variable in maze
        bool IsOpen(); //used by the mini map to tell whether a door is open.  Stair cases count as open
    }
}
