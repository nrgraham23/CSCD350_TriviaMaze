using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class NullDoor : IDoor {
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
    }
}
