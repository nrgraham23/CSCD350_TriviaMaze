/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - QuestionShort, is the short answer question.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class QuestionShort : Question {

        //=====================================================================
        //Comment- Constructor
        public QuestionShort() {
            SetChoiceArray(new String[4] { "correct", "", "", "" });
            SetText("DEFAULT: The answer is correct.");
            SetAnswer(1);
            SetQType(1);
            SetAuxiliary(1);
            SetAuxFile("NULL");
        }

        //=====================================================================
        //Comment-
        public override String ToString() {
            String result = GetText();
            return result;
        }

        //=====================================================================
        //Comment-
        public override bool CheckAnswer(String answer) {
            if (answer.ToLower().Equals(GetChoice(1).ToLower()))
                return true;
            return false;
        }

    }
}
