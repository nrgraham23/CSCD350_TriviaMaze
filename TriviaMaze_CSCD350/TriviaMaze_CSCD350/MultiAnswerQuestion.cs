/* Twenty Hats
 * Daniel Moore
 * CSCD 350
 *
 * Brief Description- Child class of IQuestion for Multipule choice Questions.
 * Things To Do-
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
    class MultiAnswerQuestion : IQuestion{

        protected String[] MultiChoiceList = new String[4];

        public MultiAnswerQuestion(String Q, String A, String Aanswer, String Banswer, String Canswer, String Danswer): base(Q, A) {
            this.MultiChoiceList[0] = Aanswer;
            this.MultiChoiceList[1] = Banswer;
            this.MultiChoiceList[2] = Canswer;
            this.MultiChoiceList[3] = Danswer;
        }

        public override void PrintQuestion() {
            base.PrintQuestion();

            int i;
            for (i = 0; i < 4; i++) {
                Console.WriteLine(this.MultiChoiceList[i]);
            }
            
        }
    }
}
