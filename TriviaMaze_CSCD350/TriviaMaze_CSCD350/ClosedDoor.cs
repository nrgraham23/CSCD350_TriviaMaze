/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - ClosedDoor
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class ClosedDoor : IDoor {

        private Question question;
        
        public ClosedDoor() {
            QuestionDatabase db = QuestionDatabase.GetInstance();
            this.question = db.RandomQuestion();
        }

        //=====================================================================
        
        public bool Enter() {
            this.question.AskQuestion();
            return false;
        }

        //=====================================================================
        
        public String GetFileName() {
            return "door_closed.png";
        }

        //=====================================================================
        
        public bool Passable() {
            return true;
        }

        //=====================================================================
        
        public int FloorChange() {
            return 0;
        }

        //=====================================================================

        public bool IsOpen() {
            return false;
        }

        //=====================================================================

        public bool IsWall() {
            return false;
        }
    }
}
