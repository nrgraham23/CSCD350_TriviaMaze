/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - Question, is the abstract class that is the parent for the child questions.
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
        String questionText, questionAuxFile;
        int questionAnswer;     // 1-4 (corresponds to A-D)
        int questionType;       // 1 = Short, 2 = T/F, 3 = Multi
        int questionAuxiliary;  // 1 = Text, 2 = Image, 3 = Sound
        [NonSerialized]
        protected static List<IObserver<Question>> observers;

        //=====================================================================
        //Constructor
        public Question() {
            if (observers == null) {
                observers = new List<IObserver<Question>>();
            }
        }

        //=====================================================================
        //Set Methods
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

        public void SetAuxiliary(int aux) {
            questionAuxiliary = aux;
        }

        public void SetAuxFile(String path) {
            questionAuxFile = path;
        }

        //=====================================================================
        //Get Methods
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

        public int GetAuxiliary() {
            return questionAuxiliary;
        }

        public String GetAuxFile() {
            return questionAuxFile;
        }

        //=====================================================================
        //Abstract Methods
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
        
        public virtual void SetMultiChoiceList(String[] newList) {
            throw new NotImplementedException();
        }
    }
}
