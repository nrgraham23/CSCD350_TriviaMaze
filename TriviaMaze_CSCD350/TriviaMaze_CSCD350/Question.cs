/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - Question, is the abstract class that is the
 * parent for the child questions.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {

    [Serializable]
    public abstract class Question : IObservable<Question> {

        String[] questionChoices = new String[4] { "", "", "", "" };
        String questionText;
        int questionAnswer, questionType;
        [NonSerialized]
        protected static List<IObserver<Question>> observers;

        //=====================================================================
        //Comment- Constructor
        public Question() {
            if (observers == null) {
                observers = new List<IObserver<Question>>();
            }
        }

        //=====================================================================
        //Comment- Set Methods
        public void SetText(String text) {
            questionText = text;
        }

        public void SetAnswer(int answer) {
            questionAnswer = answer;
        }

        public void SetQType(int type) {
            questionType = type;
        }

        public void SetChoiceArray(String[] choices) {
            for (int i = 0; i < 4; i++)
                questionChoices[i] = choices[i];
        }

        public void SetChoice(int index, String choice) {
            if (index >= 1 && index <= 4)
                questionChoices[index-1] = choice;
        }

        //=====================================================================
        //Comment- Get Methods
        public String GetText() {
            return questionText;
        }

        public int GetAnswer() {
            return questionAnswer;
        }

        public int GetQType() {
            return questionType;
        }

        public String[] GetChoiceArray() {
            return questionChoices;
        }

        public String GetChoice(int index) {
            if (index >= 1 && index <= 4)
                return questionChoices[index-1];
            return "";
        }

        //=====================================================================
        //Comment- Abstract Methods
        public abstract override String ToString();

        public abstract bool CheckAnswer(String answer);

        //=====================================================================
        //Comment-
        public void AskQuestion() {
            observers[0].OnNext(this);
        }

        //=====================================================================
        //Comment-
        public IDisposable Subscribe(IObserver<Question> observer) {
            if (!Question.observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        //=====================================================================
        //Comment- allows for the removal of an observer
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        private class Unsubscriber : IDisposable {
            [NonSerialized]
            private List<IObserver<Question>> _observers;
            [NonSerialized]
            private IObserver<Question> _observer;

            public Unsubscriber(List<IObserver<Question>> observers, IObserver<Question> observer) {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose() {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        //=====================================================================
        //Comment-
        public virtual void SetMultiChoiceList(String[] newList) {
            throw new NotImplementedException();
        }
    }
}
