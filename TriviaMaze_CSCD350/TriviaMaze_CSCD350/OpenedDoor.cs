using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class OpenedDoor : IDoor {

        public bool Enter() {
            return true;
        }

        //=====================================================================

        public String GetFileName() {
            return "door_open.png";
        }

        //=====================================================================

        public bool Passable() {
            return true;
        }
    }
}
