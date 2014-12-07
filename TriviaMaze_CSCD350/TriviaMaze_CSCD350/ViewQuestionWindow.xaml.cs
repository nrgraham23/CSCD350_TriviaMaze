/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - QuestionTF, is the true and false question.
 */

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
        bool editing = false;

        //=====================================================================
        //Comment- Initialization of the window
        public ViewQuestionWindow() {
            InitializeComponent();
            currentPage = 0;
            UpdatePageView();
        }

        //=====================================================================
        //Comment- Loads questions from database and puts them in the fields
        private void UpdatePageView() {
            LoadPageQuestions();
            SetRowsFromQuestions();
            UpdateSlider();
        }

        //=====================================================================
        //Comment- Load questions into array that correspond to IDs of given page
        private void LoadPageQuestions() {
            int offset = currentPage * 5;
            for (int i = 0; i < 5; i++)
                QArray[i] = dbInstance.GetQuestion(offset + i + 1);
        }

        //=====================================================================
        //Comment- Load text boxes from question array for display
        private void SetRowsFromQuestions() {
            for (int row = 1; row <= 5; row++)
                for (int col = 1; col <= 10; col++)
                    GetTextBoxInstance(row, col).Text = GetQuestionTextField(row, col);
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
                case 1: return "" + ((currentPage * 5) + row);
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
        //Comment- Create a new question, add to database, and go to its page
        private void Button_New_Click(object sender, RoutedEventArgs e) {
            switch (RadioTypeCheck()) {
                case 1:
                    dbInstance.AddQuestionToDatabase(new QuestionShort());
                    break;
                case 2:
                    dbInstance.AddQuestionToDatabase(new QuestionTF());
                    break;
                case 3:
                    dbInstance.AddQuestionToDatabase(new QuestionMulti());
                    break;
                default:
                    break;
            }
            currentPage = (dbInstance.GetEntries() - 1) / 5;
            UpdatePageView();
        }

        //=====================================================================
        //Comment- 
        private void Button_Edit_Click(object sender, RoutedEventArgs e) {
            int row = RadioRowCheck();

            if (row == 0 || QArray[row-1] == null)
                return;

            if (editing == false) {
                // Change button text
                this.Button_Edit.Content = "Done";
                editing = true;

                // Open fields for editing
                for (int col = 2; col <= 10; col++)
                    GetTextBoxInstance(row, col).IsEnabled = true;

                // Disable other controls
                EnableControls(false);
                
            } else {
                // Check fields for issues
                if (!FinalizeEditedRow(row))
                    return;

                // Change button text
                this.Button_Edit.Content = "Edit";
                editing = false;

                // Close fields for editing
                for (int col = 2; col <= 10; col++)
                    GetTextBoxInstance(row, col).IsEnabled = false;

                // Enable other controls
                EnableControls(true);

                //Update Question object and change in database
                UpdatePageQuestion(row);
                UpdatePageView();
            }
        }

        private void EnableControls(bool choice) {
            this.Radio_Short.IsEnabled = choice;
            this.Radio_TF.IsEnabled = choice;
            this.Radio_Multi.IsEnabled = choice;
            this.Button_New.IsEnabled = choice;
            this.Button_Delete.IsEnabled = choice;
            this.Radio_Row1.IsEnabled = choice;
            this.Radio_Row2.IsEnabled = choice;
            this.Radio_Row3.IsEnabled = choice;
            this.Radio_Row4.IsEnabled = choice;
            this.Radio_Row5.IsEnabled = choice;
            this.Button_Next.IsEnabled = choice;
            this.Button_Prev.IsEnabled = choice;
            this.Slider.IsEnabled = choice;
        }

        //=====================================================================
        //Comment- Check if edited input is valid for database entry
        private bool FinalizeEditedRow(int row) {
            bool isGood = true;
            int check;
            TextBox tb = null;

            // Check Question Type
            try {
                tb = GetTextBoxInstance(row, 3);
                check = Convert.ToInt32(tb.Text);
                if (check < 1 || check > 3)
                    throw (new FormatException());
                else
                    tb.FontStyle = FontStyles.Normal;
            } catch (FormatException) {
                isGood = false;
                tb.FontStyle = FontStyles.Oblique;
                tb.Text = "" + QArray[row - 1].GetQType();
            }

            // Check Question Answer
            try {
                tb = GetTextBoxInstance(row, 4);
                check = Convert.ToInt32(tb.Text);
                if (check < 1 || check > 4)
                    throw (new FormatException());
                else
                    tb.FontStyle = FontStyles.Normal;
            } catch (FormatException) {
                isGood = false;
                tb.FontStyle = FontStyles.Oblique;
                tb.Text = "" + QArray[row - 1].GetAnswer();
            }

            // Check Auxiliary Type
            try {
                tb = GetTextBoxInstance(row, 9);
                check = Convert.ToInt32(tb.Text);
                if (check < 1 || check > 3)
                    throw (new FormatException());
                else
                    tb.FontStyle = FontStyles.Normal;
            } catch (FormatException) {
                isGood = false;
                tb.FontStyle = FontStyles.Oblique;
                tb.Text = "" + QArray[row - 1].GetAuxiliary();
            }

            // Let the user know if they goof up.
            if (!isGood)
                MessageBox.Show("Some forms require revision (marked in italics).\nImproper forms reset.");

            return isGood;
        }

        //=====================================================================
        //Comment- Update the question database with the edited entry
        private void UpdatePageQuestion(int index) {

            QArray[index - 1].SetText(GetTextBoxInstance(index, 2).Text);
            QArray[index - 1].SetQType(Convert.ToInt32(GetTextBoxInstance(index, 3).Text));
            QArray[index - 1].SetAnswer(Convert.ToInt32(GetTextBoxInstance(index, 4).Text));
            QArray[index - 1].SetChoice(1, GetTextBoxInstance(index, 5).Text);
            QArray[index - 1].SetChoice(2, GetTextBoxInstance(index, 6).Text);
            QArray[index - 1].SetChoice(3, GetTextBoxInstance(index, 7).Text);
            QArray[index - 1].SetChoice(4, GetTextBoxInstance(index, 8).Text);
            QArray[index - 1].SetAuxiliary(Convert.ToInt32(GetTextBoxInstance(index, 9).Text));
            QArray[index - 1].SetAuxFile(GetTextBoxInstance(index, 10).Text);

            int offset = currentPage * 5;
            dbInstance.ModifyQuestionInDatabase(offset + index, QArray[index-1]);
        }

        //=====================================================================
        //Comment- Return the index of which type radio button selected
        private int RadioTypeCheck() {
            if (this.Radio_Short.IsChecked == true)
                return 1;
            else if (this.Radio_TF.IsChecked == true)
                return 2;
            else if (this.Radio_Multi.IsChecked == true)
                return 3;
            return 0;
        }

        //=====================================================================
        //Comment- Return the index of which row radio button selected
        private int RadioRowCheck() {
            if (this.Radio_Row1.IsChecked == true)
                return 1;
            else if (this.Radio_Row2.IsChecked == true)
                return 2;
            else if (this.Radio_Row3.IsChecked == true)
                return 3;
            else if (this.Radio_Row4.IsChecked == true)
                return 4;
            else if (this.Radio_Row5.IsChecked == true)
                return 5;
            return 0;
        }

        //=====================================================================
        //Comment- Prevent the window from closing during editing mode.
        private void ThisWidow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (editing == true) {
                e.Cancel = true;
                MessageBox.Show("Cannot close window while editing!");
            }  
        }

        //=====================================================================
        //Comment- Delete indicated row determined by radio buttons
        private void Button_Delete_Click(object sender, RoutedEventArgs e) {
            int row = RadioRowCheck();

            if (row == 0 || QArray[row - 1] == null)
                return;
            if (dbInstance.GetEntries() == 1) {
                MessageBox.Show("Database must have at least one question in it!");
                return;
            }

            dbInstance.DeleteQuestionFromDatabase((currentPage * 5) + row);
            if (currentPage > ((dbInstance.GetEntries() - 1) / 5))
                currentPage--;
            UpdatePageView();
        }

        //=====================================================================
        //Comment- Go to next page
        private void Button_Next_Click(object sender, RoutedEventArgs e) {
            if (currentPage < ((dbInstance.GetEntries() - 1) / 5)) {
                currentPage++;
                UpdatePageView();
            }
        }

        //=====================================================================
        //Comment- Go to previous page
        private void Button_Prev_Click(object sender, RoutedEventArgs e) {
            if (currentPage != 0) {
                currentPage--;
                UpdatePageView();
            }
        }

        //=====================================================================
        //Comment- Set the slider to current page
        private void UpdateSlider() {
            this.Slider.Maximum = (dbInstance.GetEntries() - 1) / 5;
            this.Slider.Value = currentPage;
        }

        //=====================================================================
        //Comment- Go to the page the slider is on
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            currentPage = (int) this.Slider.Value;
            UpdatePageView();
        }

    }
}
