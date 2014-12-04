using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class Room {
        private Wall nWall;
        private Wall eWall;
        private Wall sWall;
        private Wall wWall;
        private char enteredFrom;

        //=====================================================================

        public void EnterNorth() {
            this.nWall.GetDoor().Enter();
        }

        //=====================================================================

        public void EnterEast() {
            this.eWall.GetDoor().Enter();
        }

        //=====================================================================

        public void EnterSouth() {
            this.sWall.GetDoor().Enter();
        }

        //=====================================================================

        public void EnterWest() {
            this.wWall.GetDoor().Enter();
        }

        //=====================================================================

        public bool GetNPassable() {
            return this.nWall.GetDoor().Passable();
        }
        public bool GetEPassable() {
            return this.eWall.GetDoor().Passable();
        }
        public bool GetSPassable() {
            return this.sWall.GetDoor().Passable();
        }
        public bool GetWPassable() {
            return this.wWall.GetDoor().Passable();
        }
        public char GetEnteredFrom() {
            return this.enteredFrom;
        }
        public IDoor GetNDoor() {
            return this.nWall.GetDoor();
        }
        public IDoor GetEDoor() {
            return this.eWall.GetDoor();
        }
        public IDoor GetSDoor() {
            return this.sWall.GetDoor();
        }
        public IDoor GetWDoor() {
            return this.wWall.GetDoor();
        }
        public Wall GetNWall() {
            return this.nWall;
        }
        public Wall GetEWall() {
            return this.eWall;
        }
        public Wall GetSWall() {
            return this.sWall;
        }
        public Wall GetWWall() {
            return this.wWall;
        }

        //=====================================================================

        public void SetNDoor(IDoor door) {
            this.nWall.SetDoor(door);
        }
        public void SetEDoor(IDoor door) {
            this.eWall.SetDoor(door);
        }
        public void SetSDoor(IDoor door) {
            this.sWall.SetDoor(door);
        }
        public void SetWDoor(IDoor door) {
            this.wWall.SetDoor(door);
        }
        public void SetNWall(Wall wall) {
            this.nWall = wall;
        }
        public void SetEWall(Wall wall) {
            this.eWall = wall;
        }
        public void SetSWall(Wall wall) {
            this.sWall = wall;
        }
        public void SetWWall(Wall wall) {
            this.wWall = wall;
        }
        public void SetEnteredFrom(char direction) {
            this.enteredFrom = direction;
        }
    }
}
