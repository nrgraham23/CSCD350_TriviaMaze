﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350{
    class Short_Answer_Question : IQuestion, IObservable<Short_Answer_Question>{
        
        private String Question;
        private String Answer;
        private List<IObserver<Short_Answer_Question>> observers;

        public Short_Answer_Question(String Q, String A) {
            this.Question      = Q;
            this.Answer        = A;
            this.observers     = new List<IObserver<Short_Answer_Question>>();
        }

        //=====================================================================
        //
        private Boolean CheckAnswer(String playerAnswer) {

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
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        public IDisposable Subscribe(IObserver<Short_Answer_Question> observer) {
            if (!this.observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        //=====================================================================
        //allows for the removal of an observer
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        private class Unsubscriber : IDisposable {
            private List<IObserver<Short_Answer_Question>> _observers;
            private IObserver<Short_Answer_Question> _observer;

            public Unsubscriber(List<IObserver<Short_Answer_Question>> observers, IObserver<Short_Answer_Question> observer) {
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
