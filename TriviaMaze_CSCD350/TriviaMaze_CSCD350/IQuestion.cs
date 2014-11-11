using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
    class IQuestion : IObservable<IQuestion> {

        private String Question;
        private String Answer;
        private List<IObserver<IQuestion>> observers;

        public IQuestion(String Q, String A) {
            this.Question = Q;
            this.Answer = A;
            this.observers = new List<IObserver<IQuestion>>();
        }

        //This Is Always Overriden.
        Boolean CheckAnswer(String playerAnswer) {
            Console.WriteLine("*Error* - CheckAnswer in IQuestion");
            return true;
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
    }
}
