using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class Point {
        private int row;
        private int column;

        public Point(int row, int col) {
            this.row = row;
            this.column = col;
        }

        public int getRow() {
            return this.row;
        }

        public int getCol() {
            return this.column;
        }
    }
}
