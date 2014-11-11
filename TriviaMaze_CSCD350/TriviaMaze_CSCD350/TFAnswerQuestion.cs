/* Twenty Hats
 * Daniel Moore
 * CSCD 350
 *
 * Brief Description- Child class of IQuestion for True / False Questions.
 * Things To Do-
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
    class TFAnswerQuestion : IQuestion{

        public TFAnswerQuestion(String Q, String A): base(Q, A) {
            //Nothing needed here.
        }
    }
}
