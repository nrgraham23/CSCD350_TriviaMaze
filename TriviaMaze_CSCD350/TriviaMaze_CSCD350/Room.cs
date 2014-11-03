﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class Room {
        private IDoor nDoor;
        private IDoor eDoor;
        private IDoor sDoor;
        private IDoor wDoor;

        public Room() {
            this.nDoor = new ClosedDoor();
            this.eDoor = new ClosedDoor();
            this.sDoor = new ClosedDoor();
            this.wDoor = new ClosedDoor();
        }

        public void EnterNorth() {
            nDoor.Enter();
        }

        public void EnterEast() {
            eDoor.Enter();
        }

        public void EnterSouth() {
            sDoor.Enter();
        }

        public void EnterWest() {
            wDoor.Enter();
        }

        public bool GetNPassable() {
            return nDoor.Passable();
        }
        public bool GetEPassable() {
            return eDoor.Passable();
        }
        public bool GetSPassable() {
            return sDoor.Passable();
        }
        public bool GetWPassable() {
            return wDoor.Passable();
        }

        public void SetNDoor(IDoor door) {
            this.nDoor = door;
        }
        public void SetEDoor(IDoor door) {
            this.nDoor = door;
        }
        public void SetSDoor(IDoor door) {
            this.nDoor = door;
        }
        public void SetWDoor(IDoor door) {
            this.nDoor = door;
        }
    }
}
