using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    interface IDoor {

        void Enter();
        void Display();
        bool Passable();
    }
}
