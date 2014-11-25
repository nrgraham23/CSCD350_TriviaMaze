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
            RandomQuestionFactory rand = new RandomQuestionFactory();
            this.question = rand.GetRandQuestion();
        }

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
    }
}
