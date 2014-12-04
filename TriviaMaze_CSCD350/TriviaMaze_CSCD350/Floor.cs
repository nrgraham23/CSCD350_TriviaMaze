﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class Floor {
        private int size;
        private Room[,] floor;
        Point entry;
        Point exit;

        public Floor() {
            this.size = 5;
            this.floor = new Room[size, size];
            InitFloor();
        }

        //=====================================================================

        private void InitFloor() {
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    this.floor[i, j] = new Room();
                }
            }
        }

        //=====================================================================

        public int GetSize() {
            return this.size;
        }
        public Room GetRoom(Point point) {
            return floor[point.GetRow(), point.GetCol()];
        }
        public Point GetEntry() {
            return this.entry;
        }
        public Point GetExit() {
            return this.exit;
        }

        //=====================================================================

        public void SetEntry(Point entry) {
            this.entry = entry;
        }
        public void SetExit(Point exit) {
            this.exit = exit;
        }
    }
}
