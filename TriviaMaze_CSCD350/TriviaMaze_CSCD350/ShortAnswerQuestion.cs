using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
    class ShortAnswerQuestion : IQuestion{



        public ShortAnswerQuestion(String Q, String A): base(Q, A) {
            
        }

        //=====================================================================
        //
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

        
    }
}
