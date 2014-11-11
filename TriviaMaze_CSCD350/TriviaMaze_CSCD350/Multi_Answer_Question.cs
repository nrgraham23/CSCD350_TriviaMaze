﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
    class MultiAnswerQuestion : IQuestion{

        private String Question;
        private String Answer;
        private String A;
        private String B;
        private String C;
        private String D;

        public MultiAnswerQuestion(String Q, String A) {
        }

        private Boolean CheckAnswer(String playerAnswer) {

            //This code is to print out both answer to the screen for the developers.
            Console.WriteLine("Player's Answer: " + playerAnswer);
            Console.WriteLine("Correct Answer: " + this.Answer);
            //

            Boolean result = false;

            if (this.Answer.Equals(playerAnswer)) {
                result = true;
            }

            return result;
        }

        bool IQuestion.CheckAnswer(string playerAnswer) {
            throw new NotImplementedException();
        }
    }
}
