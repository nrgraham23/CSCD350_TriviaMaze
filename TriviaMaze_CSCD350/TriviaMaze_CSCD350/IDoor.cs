﻿/* Twenty Hats
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

        bool Enter(); 
        String GetFileName();
        bool Passable();
        int FloorChange();
    }
}
