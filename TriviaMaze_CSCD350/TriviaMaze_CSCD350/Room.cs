/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - Room, The Room class holds walls which in turn
 * holds doors, and an enterfrom char. The enterfrom is
 * important because thats how we determin the direction
 * of the doors.
 */

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
        //Comment- Enter Methods
        public void EnterNorth() {
            this.nWall.GetDoor().Enter();
        }

        public void EnterEast() {
            this.eWall.GetDoor().Enter();
        }

        public void EnterSouth() {
            this.sWall.GetDoor().Enter();
        }

        public void EnterWest() {
            this.wWall.GetDoor().Enter();
        }

        //=====================================================================
        //Comment- Get Methods
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
        //Comment- Set Methods
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
