using System;
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
        private char enteredFrom;

        public Room() {
            /*  Note: if you want the doors to all be open, change the closed doors to open doors in the initdoors method of MazeBuilder class
            this.nDoor = new OpenedDoor();
            this.eDoor = new OpenedDoor();
            this.sDoor = new OpenedDoor();
            this.wDoor = new OpenedDoor();

            /* commented out for testing
            this.nDoor = new ClosedDoor();
            this.eDoor = new ClosedDoor();
            this.sDoor = new ClosedDoor();
            this.wDoor = new ClosedDoor();
             * */
        }


        //=====================================================================

        public void EnterNorth() {
            nDoor.Enter();
        }

        //=====================================================================

        public void EnterEast() {
            eDoor.Enter();
        }

        //=====================================================================

        public void EnterSouth() {
            sDoor.Enter();
        }

        //=====================================================================

        public void EnterWest() {
            wDoor.Enter();
        }

        //=====================================================================

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
        public char GetEnteredFrom() {
            return this.enteredFrom;
        }
        public IDoor GetNDoor() {
            return this.nDoor;
        }
        public IDoor GetEDoor() {
            return this.eDoor;
        }
        public IDoor GetSDoor() {
            return this.sDoor;
        }
        public IDoor GetWDoor() {
            return this.wDoor;
        }

        //=====================================================================

        public void SetNDoor(IDoor door) {
            this.nDoor = door;
        }
        public void SetEDoor(IDoor door) {
            this.eDoor = door;
        }
        public void SetSDoor(IDoor door) {
            this.sDoor = door;
        }
        public void SetWDoor(IDoor door) {
            this.wDoor = door;
        }
        public void SetEnteredFrom(char direction) {
            this.enteredFrom = direction;
        }
    }
}
