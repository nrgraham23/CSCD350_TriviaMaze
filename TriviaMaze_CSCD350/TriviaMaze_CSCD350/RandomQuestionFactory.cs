using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    class RandomQuestionFactory {
        public Question GetRandQuestion() {
            Random rand = new Random();
            int type = rand.Next(3);

            if (type == 0) {
                return new QuestionMulti();
            } else if (type == 1) {
                return new QuestionShort();
            }
            else if (type == 2) {
                return new QuestionTF();
            }else {
                Console.WriteLine("*Error* - Random Question Factory int type");
                return new QuestionMulti();
            }
        }
    }
}
