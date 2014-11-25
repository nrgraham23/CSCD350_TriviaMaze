using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class LockedDoor : IDoor {

        public LockedDoor() {
        }

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
    }
}
