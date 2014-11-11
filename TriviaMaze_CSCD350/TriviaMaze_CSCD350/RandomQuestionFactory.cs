/* Twenty Hats
 * Daniel Moore
 * CSCD 350
 *
 * Brief Description- Factory for making questions
 * Things To Do-
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
    class RandomQuestionFactory{
        public IQuestion createQuestion(String type, String Question, String Answer){
            IQuestion Q = null;

            if(type.Equals("TF Question")){
                //create TF question 
            }
            else if(type.Equals("Multi Question")){
                //create Multi question
            }
            else if (type.Equals("Short Question")) {
                //create Short Question
            }
            else {
                Console.WriteLine("*Error* - createQuestion in Random_Question_Factory");
            }

           return Q;
        }
    }
}
