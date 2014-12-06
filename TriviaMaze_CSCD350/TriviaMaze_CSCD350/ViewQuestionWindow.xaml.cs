using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TriviaMaze_CSCD350 {
    /// <summary>
    /// Interaction logic for ViewQuestionWindow.xaml
    /// </summary>
    public partial class ViewQuestionWindow : Window {

        QuestionDatabase dbInstance = QuestionDatabase.GetInstance();
        Question[] QArray = new Question[5] {null, null, null, null, null};
        int currentPage;

        //=====================================================================
        //Comment- 
        public ViewQuestionWindow() {
            InitializeComponent();
            currentPage = 1;
            LoadPageQuestions(currentPage);
            SetRowsFromQuestions();
        }

        //=====================================================================
        //Comment- Load questions into array that correspond to IDs of given page
        private void LoadPageQuestions(int page) {
            int offset = (page - 1) * 5;
            for (int i = 0; i < 5; i++)
                QArray[i] = dbInstance.GetQuestion(offset + i + 1);
        }

        //=====================================================================
        //Comment- Load text boxes from question array for display
        private void SetRowsFromQuestions() {
            for (int row = 1; row <= 5; row++) {
                for (int col = 1; col <= 10; col++) {
                    GetTextBoxInstance(row, col).Text = GetQuestionTextField(row, col);
                }
            }
        }

        //=====================================================================
        //Comment- From a given row and column, return the instance of the corresponding TextBox
        private TextBox GetTextBoxInstance(int row, int col) {
            switch (row) {
                case 1:
                    switch (col) {
                        case 1: return this.Row1_ID;
                        case 2: return this.Row1_Question;
                        case 3: return this.Row1_Type;
                        case 4: return this.Row1_Answer;
                        case 5: return this.Row1_Option1;
                        case 6: return this.Row1_Option2;
                        case 7: return this.Row1_Option3;
                        case 8: return this.Row1_Option4;
                        case 9: return this.Row1_Auxiliary;
                        case 10:return this.Row1_AuxFile;
                    }
                    break;
                case 2:
                    switch (col) {
                        case 1: return this.Row2_ID;
                        case 2: return this.Row2_Question;
                        case 3: return this.Row2_Type;
                        case 4: return this.Row2_Answer;
                        case 5: return this.Row2_Option1;
                        case 6: return this.Row2_Option2;
                        case 7: return this.Row2_Option3;
                        case 8: return this.Row2_Option4;
                        case 9: return this.Row2_Auxiliary;
                        case 10:return this.Row2_AuxFile;
                    }
                    break;
                case 3:
                    switch (col) {
                        case 1: return this.Row3_ID;
                        case 2: return this.Row3_Question;
                        case 3: return this.Row3_Type;
                        case 4: return this.Row3_Answer;
                        case 5: return this.Row3_Option1;
                        case 6: return this.Row3_Option2;
                        case 7: return this.Row3_Option3;
                        case 8: return this.Row3_Option4;
                        case 9: return this.Row3_Auxiliary;
                        case 10:return this.Row3_AuxFile;
                    }
                    break;
                case 4:
                    switch (col) {
                        case 1: return this.Row4_ID;
                        case 2: return this.Row4_Question;
                        case 3: return this.Row4_Type;
                        case 4: return this.Row4_Answer;
                        case 5: return this.Row4_Option1;
                        case 6: return this.Row4_Option2;
                        case 7: return this.Row4_Option3;
                        case 8: return this.Row4_Option4;
                        case 9: return this.Row4_Auxiliary;
                        case 10:return this.Row4_AuxFile;
                    }
                    break;
                case 5:
                    switch (col) {
                        case 1: return this.Row5_ID;
                        case 2: return this.Row5_Question;
                        case 3: return this.Row5_Type;
                        case 4: return this.Row5_Answer;
                        case 5: return this.Row5_Option1;
                        case 6: return this.Row5_Option2;
                        case 7: return this.Row5_Option3;
                        case 8: return this.Row5_Option4;
                        case 9: return this.Row5_Auxiliary;
                        case 10:return this.Row5_AuxFile;
                    }
                    break;
            }
            return null;
        }

        //=====================================================================
        //Comment- From a given row and column, return the string corresponding to the question holder thingymaboober
        private string GetQuestionTextField(int row, int col) {
            if (QArray[row - 1] == null)
                return "";
            switch (col) {
                case 1: return "" + ((currentPage - 1) * 5) + row;
                case 2: return QArray[row-1].GetText();
                case 3: return "" + QArray[row-1].GetQType();
                case 4: return "" + QArray[row - 1].GetAnswer();
                case 5: return QArray[row - 1].GetChoice(1);
                case 6: return QArray[row - 1].GetChoice(2);
                case 7: return QArray[row - 1].GetChoice(3);
                case 8: return QArray[row - 1].GetChoice(4);
                case 9: return "" + QArray[row - 1].GetAuxiliary();
                case 10:return QArray[row - 1].GetAuxFile();
                default:return "";
            }
        }

        //=====================================================================
        //Comment- 
        private void GoToLastPage() {
            currentPage = dbInstance.GetEntries() / 5;
            LoadPageQuestions(currentPage);
            SetRowsFromQuestions();
        }

        //=====================================================================
        //Comment- 
        private void Button_New_Click(object sender, RoutedEventArgs e) {
            if (this.Radio_Short.IsChecked == true)
                QArray[3] = new QuestionShort();
            else if (this.Radio_TF.IsChecked == true)
                QArray[3] = new QuestionTF();
            else if (this.Radio_Multi.IsChecked == true)
                QArray[3] = new QuestionMulti();
            SetRowsFromQuestions();
        }

    }
}
