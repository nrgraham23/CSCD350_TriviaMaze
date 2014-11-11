/* Twenty Hats
 * Daniel Moore
 * CSCD 350
 *
 * Brief Description- Test for my Questions.
 * Things To Do-
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    class TestQuestion {

        private static String type;
        private static String Q = "What is your favorite color?";
        private static String A = "Blue";
        private static RandomQuestionFactory factory = new RandomQuestionFactory();
        private static IQuestion Question;

       // static void Main(string[] args) {
       //     TestFactory();
        //}

        private static void TestFactory() {
            type = "TF Question";

            Question = factory.createQuestion(type,Q,A);

            Question.PrintAnswer();

            Question.PrintQuestion();

            type = "Multi Question";

            Question = factory.createQuestion(type,Q,A,"A.dog","B.cat","C.train","D.Pure Rage");

            Question.PrintAnswer();

            Question.PrintQuestion();
        }

        private static void TestTFAnswerQuestion() {

        }

        private static void TestShortAnswerQuestion() {

        }

        private static void TestMultiAnswerQuestion() {

        }
    }
}
