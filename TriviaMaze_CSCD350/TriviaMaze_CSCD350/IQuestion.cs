/* Twenty Hats
 * Daniel Moore
 * CSCD 350
 *
 * Brief Description- The base class for Quesions, Most of the action happens here.
 * Things To Do- 
 * -Change IQuestion to Question
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
        class IQuestion : IObservable<IQuestion> {

        protected String Question;
        protected String Answer;
        protected List<IObserver<IQuestion>> observers;

        public IQuestion(String Q, String A) {
            this.Question = Q;
            this.Answer = A;
            this.observers = new List<IObserver<IQuestion>>();
        }


        //=====================================================================
        //Get and Set for Question and Answer
        public String GetQuestion() {
            return this.Question;
        }

        public void SetQuestion(String Q) {
            this.Question = Q;
        }

        public String GetAnswer(){
            return this.Answer;
        }

        public void SetAnswer(String A){
            this.Answer = A;
        }

        //=====================================================================
        //Checks to see if the answer to the question is right
        public Boolean CheckAnswer(String playerAnswer) {

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

        //=====================================================================
        //Prints out Answer
        public void PrintAnswer() {
            Console.WriteLine(this.Answer);
        }

        //=====================================================================
        //Prints out Question
        public virtual void PrintQuestion() {
            Console.WriteLine(this.Question);
        }

        //=====================================================================
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        public IDisposable Subscribe(IObserver<IQuestion> observer) {
            if (!this.observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        //=====================================================================
        //allows for the removal of an observer
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        private class Unsubscriber : IDisposable {
            private List<IObserver<IQuestion>> _observers;
            private IObserver<IQuestion> _observer;

            public Unsubscriber(List<IObserver<IQuestion>> observers, IObserver<IQuestion> observer) {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose() {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public virtual void SetMultiChoiceList(String[] newList) {
            throw new NotImplementedException();
        }
    }
}
