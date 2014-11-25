using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public class QuestionTF : Question{

        public QuestionTF() {
            SetChoiceArray(new String[4] { "TRUE", "FALSE", "", "" });
            SetText("DEFAULT: This is a true or false question");
            SetAuxFile("NULL");
            SetAnswer(1);
            SetQType(2);
            SetAuxiliary(1);
        }

        public override String ToString() {
            String result = GetText() + "\n\tA. True\n\tB. False";
            return result;
        }

        public override bool CheckAnswer(String answer) {
            if ((answer.Equals("TRUE") && GetAnswer() == 1) || (answer.Equals("FALSE") && GetAnswer() == 2))
                return true;
            return false;
        }

    }
}
