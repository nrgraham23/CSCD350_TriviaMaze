using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class ClosedDoor : IDoor {

        public bool Enter() {
            return false; //TODO: ask a question, if right return true, if wrong return false
        }

        //=====================================================================

        public String GetFileName() {
            return "door_closed.png";
        }

        //=====================================================================
        
        public bool Passable() {
            return true;
        }
    }
}
