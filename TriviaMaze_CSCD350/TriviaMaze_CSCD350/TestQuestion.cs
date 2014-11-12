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

        static void Main(string[] args) { TestGetSetMulti(); }

        private static void TestFactory() {
            //Basic Test
            type = "TF Question";

            Question = factory.createQuestion(type,Q,A);

            Question.PrintAnswer();

            Question.PrintQuestion();

            //Testing Multi Question
            type = "Multi Question";
            A = "Tree";
            Q = "What is life?";

            Question = factory.createQuestion(type,Q,A,"A.dog","B.cat","C.train","D.Pure Rage");

            Question.PrintAnswer();

            Question.PrintQuestion();

            Console.WriteLine("CheckAnswer should be true");
            Console.WriteLine(Question.CheckAnswer("Tree"));

            Console.WriteLine("CheckAnswer should be false");
            Console.WriteLine(Question.CheckAnswer("xxx"));
        }

        private static void TestGetSetMulti() {
            type = "Multi Question";
            Question = factory.createQuestion(type, Q, A, "A.dog", "B.cat", "C.train", "D.Pure Rage");

            Question.PrintQuestion();
            Question.PrintAnswer();

            String[] tempString = new String[4];
            tempString[0] = "A.Car";
            tempString[1] = "B.Fluid";
            tempString[2] = "C.Lake House";
            tempString[3] = "D.Swing";
            Question.SetMultiChoiceList(tempString);

            Question.PrintQuestion();
            Question.PrintAnswer();

        }
    }
}
