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

        //=====================================================================
        //Comment-
        public ClosedDoor() {
            RandomQuestionFactory rand = new RandomQuestionFactory();
            this.question = rand.GetRandQuestion();
        }

        //=====================================================================
        //Comment-
        public bool Enter() {
            this.question.AskQuestion();
            return false;
        }

        //=====================================================================
        //Comment-
        public String GetFileName() {
            return "door_closed.png";
        }

        //=====================================================================
        //Comment-
        public bool Passable() {
            return true;
        }

        //=====================================================================
        //Comment-
        public int FloorChange() {
            return 0;
        }
    }
}
