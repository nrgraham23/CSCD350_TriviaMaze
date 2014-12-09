/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - QuestionMulti, is the multipule choice question.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class QuestionMulti : Question {

        //=====================================================================
        
        public QuestionMulti() {
            SetChoiceArray(new String[4] { "Apple", "Orange", "Cherry", "Strawberry" });
            SetText("DEFAULT: Which of these fruits is not red?");
            SetAnswer(2);
            SetQType(3);
            SetAuxiliary(1);
            SetAuxFile("");
        }

        //=====================================================================
        
        public override String ToString() {
            String result = GetText() + "\n\nA. " + GetChoice(1) + "\nB. " + GetChoice(2) + "\nC. " + GetChoice(3) + "\nD. " + GetChoice(4);
            return result;
        }

        //=====================================================================
        
        public override bool CheckAnswer(String answer) {
            if ((answer.Equals("A") && GetAnswer() == 1) || (answer.Equals("B") && GetAnswer() == 2) ||
                (answer.Equals("C") && GetAnswer() == 3) || (answer.Equals("D") && GetAnswer() == 4))
                return true;
            return false;
        }

    }
}
