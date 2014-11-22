using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class QuestionShort : Question {

        public QuestionShort() {
            SetChoiceArray(new String[4] { "correct", "", "", "" });
            SetText("DEFAULT: The answer is correct.");
            SetAuxFile("NULL");
            SetAnswer(1);
            SetQType(1);
            SetAuxiliary(1);
        }

        public override String ToString() {
            String result = GetText();
            return result;
        }

        public override bool CheckAnswer(String answer) {
            if (answer.ToLower().Equals(GetChoice(1).ToLower()))
                return true;
            return false;
        }

    }
}
