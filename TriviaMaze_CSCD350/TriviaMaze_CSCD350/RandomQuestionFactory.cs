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
       

        //Creates a Random Question from the Database
        public IQuestion createRandomQuestion() {
            throw new NotImplementedException();
        }

        //=====================================================================
        //Create a specific question, mostly for testing.
        public IQuestion createQuestion(String type, String Question, String Answer){
            IQuestion Q = null;

            if(type.Equals("TF Question")){
                //create TF question 
                Q = new TFAnswerQuestion(Question, Answer);
            }
            else if (type.Equals("Short Question")) {
                //create Short Question
                Q = new ShortAnswerQuestion(Question, Answer);
            }else {
                Console.WriteLine("*Error* - createQuestion in Random_Question_Factory");
            }

           return Q;
        }

        //=====================================================================
        //Creates a specific question for testing, but its for multi question
        public IQuestion createQuestion(String type, String Question, String Answer, String A, String B, String C, String D) {
            IQuestion Q = null;
            
            if(type.Equals("Multi Question")){
                //create Multi question
                Q = new MultiAnswerQuestion(Question, Answer, A, B, C, D);
            }else{
                Console.WriteLine("*Error* - createQuestion in Random_Question_Factory");
            }

            return Q;
        }

    }
}
