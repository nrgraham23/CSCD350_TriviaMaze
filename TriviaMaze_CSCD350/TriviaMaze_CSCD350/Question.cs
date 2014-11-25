using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze_CSCD350 {
    abstract class Question : IObservable<Question> {

        String[] questionChoices = new String[4] { "", "", "", "" };
        String questionText, questionAuxFile;
        int questionAnswer, questionType, questionAuxiliary;
        protected static List<IObserver<Question>> observers;

        public Question() {
            if (observers == null) {
                observers = new List<IObserver<Question>>();
            }
        }

        //=====================================================================
        // SET METHODS
        public void SetText(String text) {
            questionText = text;
        }

        public void SetAnswer(int answer) {
            questionAnswer = answer;
        }

        public void SetQType(int type) {
            questionType = type;
        }

        public void SetAuxiliary(int aux) {
            questionAuxiliary = aux;
        }

        public void SetAuxFile(String path) {
            questionAuxFile = path;
        }

        public void SetChoiceArray(String[] choices) {
            for (int i = 0; i < 4; i++)
                questionChoices[i] = choices[i];
        }

        public void SetChoice(int index, String choice) {
            if (index >= 1 && index <= 4)
                questionChoices[index] = choice;
        }

        //=====================================================================
        // GET METHODS
        public String GetText() {
            return questionText;
        }

        public int GetAnswer() {
            return questionAnswer;
        }

        public int GetQType() {
            return questionType;
        }

        public int GetAuxiliary() {
            return questionAuxiliary;
        }

        public String GetAuxFile() {
            return questionAuxFile;
        }

        public String[] GetChoiceArray() {
            return questionChoices;
        }

        public String GetChoice(int index) {
            if (index >= 1 && index <= 4)
                return questionChoices[index];
            return "";
        }

        //=====================================================================
        // ABSTRACT METHODS
        public abstract override String ToString();

        public abstract bool CheckAnswer(String answer);

        //=====================================================================

        public void AskQuestion() {
            observers[0].OnNext(this);
        }

        //=====================================================================

        public IDisposable Subscribe(IObserver<Question> observer) {
            if (!Question.observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        //=====================================================================
        //allows for the removal of an observer
        //taken directly from: http://msdn.microsoft.com/en-us/library/dd990377%28v=vs.110%29.aspx
        private class Unsubscriber : IDisposable {
            private List<IObserver<Question>> _observers;
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

        public virtual void SetMultiChoiceList(String[] newList) {
            throw new NotImplementedException();
        }
    }
}
