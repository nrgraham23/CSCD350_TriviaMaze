using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class VictoryDoor : IDoor {
        public bool Enter() {
            throw new NotImplementedException();
        }

        public string GetFileName() {
            throw new NotImplementedException();
        }

        public bool Passable() {
            return true;
        }
    }
}
