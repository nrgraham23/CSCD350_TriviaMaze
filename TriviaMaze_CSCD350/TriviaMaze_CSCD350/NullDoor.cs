﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class NullDoor : IDoor {
        public void Enter() {

        }

        public void Display() {

        }

        public bool Passable() {
            return false;
        }
    }
}