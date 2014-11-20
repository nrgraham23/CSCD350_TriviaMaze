using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class Question {

        enum QType { SHORT = 1, TF, MULTI };
        enum QAux { TEXT = 1, IMAGE, SOUND };

        String[] questionChoices;
        String questionText, questionAuxFile;
        int questionAnswer, questionType, questionAuxiliary;

        //=====================================================================
        // initialize values for default question object
        public Question() {
            questionChoices = new String[4] { "Correct", "Wrong", "Nope", "False" };
            questionText = "Default question";
            questionAuxFile = "NULL";
            questionAnswer = 1;
            questionType = 3;
            questionAuxiliary = 1;
        }

        //=====================================================================

        public void SetText(String text) {
            questionText = text;
        }

        public void SetAnswer(int answer) {
            questionAnswer = answer;
        }

        public void SetQType(int type) {
            questionType = type;
        }

        public void SetAuxiliary(int aux) {
            questionAuxiliary = aux;
        }

        public void SetAuxFile(String path) {
            questionAuxFile = path;
        }

        public void SetChoices(String[] choices) {
            for (int i = 0; i < 4; i++)
                questionChoices[i] = choices[i];
        }

        public void SetChoice(int index, String choice) {
            if (index >= 1 && index <= 4)
                questionChoices[index] = choice;
        }

        //=====================================================================

        public String GetText() {
            return questionText;
        }

        public int GetAnswer() {
            return questionAnswer;
        }

        public int GetQType() {
            return questionType;
        }

        public int GetAuxiliary() {
            return questionAuxiliary;
        }

        public String GetAuxFile() {
            return questionAuxFile;
        }

        public String[] GetChoices() {
            return questionChoices;
        }

        public String GetChoice(int index) {
            if (index >= 1 && index <= 4)
                return questionChoices[index];
            return "";
        }

        //=====================================================================

        public bool CorrectShortAnswer(String answer) {
            if (answer.ToLower().Equals(GetChoice(1).ToLower()))
                return true;
            return false;
        }

        public bool CorrectTFAnswer(bool answer) {
            if ((answer && GetAnswer() == 1) || (!answer && GetAnswer() == 2))
                return true;
            return false;
        }

        public bool CorrectMultiAnswer(int answer) {
            if (answer == GetAnswer())
                return true;
            return false;
        }

        //=====================================================================
    }
}
