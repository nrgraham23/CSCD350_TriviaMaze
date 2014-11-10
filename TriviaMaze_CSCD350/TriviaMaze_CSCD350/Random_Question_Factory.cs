using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
    class Random_Question_Factory{
        public IQuestion createQuestion(){

            IQuestion Q = new TF_Answer_Question();

           return Q;
        }
    }
}
