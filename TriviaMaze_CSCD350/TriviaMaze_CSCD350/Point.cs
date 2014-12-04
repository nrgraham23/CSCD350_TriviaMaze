/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - Point, point is a class that is a helper
 * to get an exact point on the maze with row,col.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class Point {
        private int row;
        private int column;

        //=====================================================================
        //Comment- Constructor
        public Point(int row, int col) {
            this.row = row;
            this.column = col;
        }

        //=====================================================================
        //Comment- Get Methods
        public int GetRow() {
            return this.row;
        }
        public int GetCol() {
            return this.column;
        }

        //=====================================================================
        //Comment- Set Methods
        public void SetRow(int row) {
            this.row = row;
        }
        public void SetCol(int col) {
            this.column = col;
        }
    }
}
