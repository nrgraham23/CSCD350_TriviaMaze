/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - QuestionTF, is the true and false question.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class QuestionTF : Question{

        //=====================================================================
        //Constructor
        public QuestionTF() {
            SetChoiceArray(new String[4] { "TRUE", "FALSE", "", "" });
            SetText("DEFAULT: This is a true or false question.");
            SetAnswer(1);
            SetQType(2);
            SetAuxiliary(1);
            SetAuxFile("");
        }

        //=====================================================================
        
        public override String ToString() {
            String result = GetText() + "\n\nA. True\nB. False";
            return result;
        }

        //=====================================================================
        
        public override bool CheckAnswer(String answer) {
            if ((answer.Equals("TRUE") && GetAnswer() == 1) || (answer.Equals("FALSE") && GetAnswer() == 2))
                return true;
            return false;
        }

    }
}
