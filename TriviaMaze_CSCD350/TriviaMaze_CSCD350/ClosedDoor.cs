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
        //Random ran = new Random();

        //=====================================================================
        
        public ClosedDoor() {
            //ran = new Random();
            //int temp = ran.Next(3);
            RandomQuestionFactory rand = new RandomQuestionFactory();
            this.question = rand.GetRandQuestion();
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
    }
}
