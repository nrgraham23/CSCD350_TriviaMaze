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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using System.Windows.Threading;
using System.Media;

namespace TriviaMaze_CSCD350 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /*
     * NOTES:
     *      
     */

    public partial class MainWindow : Window, IObserver<Maze>, IObserver<Question> {
        private GameCore gameCore;
        private Question subscribeQuestion;
        private Question currentQuestion;
        private bool askingQuestion;
        private static bool flagEndGame = false;

        public MainWindow() {
            this.askingQuestion = false;
            InitializeComponent();
            DrawMiniMap();
            this.subscribeQuestion = new QuestionTF();
            this.subscribeQuestion.Subscribe(this);
            ResetQuestion();
        }

        //=====================================================================

        private void DrawMiniMap() {
            DrawOutline();
            DrawLines();
            DrawVerticleDoors();
            DrawHorizontalDoors();

        }

        private void FindOpenedClosedDoors(Floor curFloor) {  //0 is unknown, 1 is red, 2 is green
            for (int x = 0; x < 5; x++) {
                for (int y = 0; y < 4; y++) {
                    Point newPoint = new Point(x, y);
                    Room aRoom = curFloor.GetRoom(newPoint);
                    if (aRoom.GetVisited()) {
                        if (aRoom.GetEDoor().Passable() == true && aRoom.GetEDoor().IsOpen() == true) {
                            DrawVerticleDoor(y, x, 1);
                        }
                        if (aRoom.GetEDoor().Passable() == false) {
                            DrawVerticleDoor(y, x, 2);
                        }
                        //------------------------
                        if (aRoom.GetEDoor().Passable() == true && aRoom.GetEDoor().IsOpen() == true) {
                            DrawVerticleDoor(y, x, 2);
                        }
                        if (aRoom.GetWDoor().Passable() == true && aRoom.GetWDoor().IsOpen() == true) {
                            DrawVerticleDoor(y, x, 2);
                        }
                        if (aRoom.GetEDoor().Passable() == false && aRoom.GetWDoor().Passable() == false) {
                            DrawVerticleDoor(y, x, 1);
                        }
                    }
                }
            }

            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 5; y++) {
                    Point newPoint = new Point(x, y);
                    Room aRoom = curFloor.GetRoom(newPoint);
                    if (aRoom.GetVisited()) {
                        if (aRoom.GetSDoor().Passable() == true && aRoom.GetSDoor().IsOpen() == true) {
                            DrawHorizontalDoor(y, x, 1);
                        }
                        if (aRoom.GetSDoor().Passable() == false) {
                            DrawHorizontalDoor(y, x, 2);
                        }
                        //------------------------
                        if (aRoom.GetSDoor().Passable() == true && aRoom.GetSDoor().IsOpen() == true) {
                            DrawHorizontalDoor(y, x, 2);
                        }
                        if (aRoom.GetNDoor().Passable() == true && aRoom.GetNDoor().IsOpen() == true) {
                            DrawHorizontalDoor(y, x, 2);
                        }
                        if (aRoom.GetSDoor().Passable() == false && aRoom.GetNDoor().Passable() == false) {
                            DrawHorizontalDoor(y, x, 1);
                        }
                    }
                }
            }
        }

        //=====================================================================

        private void ReDrawLine(int x, int y) {
            System.Windows.Shapes.Rectangle line;

            line = new System.Windows.Shapes.Rectangle();
            //line.Stroke = new SolidColorBrush(Colors.Black);
            line.Width = 25;
            line.Height = 25;
            line.Fill = new SolidColorBrush(Colors.LawnGreen);
            Canvas.SetLeft(line, 30 + (x * 50));
            Canvas.SetTop(line, 15 + (y * 50));
            MapCanvas.Children.Add(line);
        }

        private void MoveTriangle(int row, int col, char enteredFrom) {

            DrawTriangle(row, col, enteredFrom);

            if (enteredFrom == 'n') {
                EnteredFromN(row, col, enteredFrom);
            }
            if (enteredFrom == 'e') {
                EnteredFromE(row, col, enteredFrom);
            }
            if (enteredFrom == 's') {
                EnteredFromS(row, col, enteredFrom);
            }
            if (enteredFrom == 'w') {
                EnteredFromW(row, col, enteredFrom);
            }
        }

        //=====================================================================

        private void EnteredFromN(int row, int col, char enteredFrom) {
            if (row == 0) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row, col - 1);                
            } else if (row == 4) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row, col - 1);                
            } else if (col == 4) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row, col - 1);                
            } else {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row, col - 1);                
            }
        }

        //=====================================================================

        private void EnteredFromE(int row, int col, char enteredFrom) {
            if (col == 0) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row + 1, col);                
            } else if (col == 4) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row + 1, col);                
            } else {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row + 1, col);                
            }
        }

        //=====================================================================

        private void EnteredFromS(int row, int col, char enteredFrom) {
            if (row == 0) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row, col + 1);                
            } else if (row == 4) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row, col + 1);                
            } else if (col == 0) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row, col + 1);                
            } else {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row, col + 1);                
            }
        }

        //=====================================================================

        private void EnteredFromW(int row, int col, char enteredFrom) {
            if (row == 0)
                DrawTriangle(row, col, enteredFrom);
            else if (col == 0) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row - 1, col);                
            } else if (col == 4) {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row - 1, col);                
            } else {
                DrawTriangle(row, col, enteredFrom);
                ReDrawLine(row - 1, col);                
            }
        }

        //=====================================================================

        private void DrawTriangle(int row, int col, char enteredFrom) {

            System.Windows.Shapes.Line leftTriangle;
            System.Windows.Shapes.Line rightTriangle;

            leftTriangle = new System.Windows.Shapes.Line();
            rightTriangle = new System.Windows.Shapes.Line();

            leftTriangle.Stroke = new SolidColorBrush(Colors.Black);
            rightTriangle.Stroke = new SolidColorBrush(Colors.Black);

            if (enteredFrom == 'n') {
                leftTriangle.X1 = 25;
                leftTriangle.Y1 = 30;
                leftTriangle.X2 = 15;
                leftTriangle.Y2 = 15;

                rightTriangle.X1 = 25;
                rightTriangle.Y1 = 30;
                rightTriangle.X2 = 35;
                rightTriangle.Y2 = 15;
            }

            if (enteredFrom == 'e') {
                leftTriangle.X1 = 15;
                leftTriangle.Y1 = 20;
                leftTriangle.X2 = 30;
                leftTriangle.Y2 = 10;

                rightTriangle.X1 = 15;
                rightTriangle.Y1 = 20;
                rightTriangle.X2 = 30;
                rightTriangle.Y2 = 30;
            }

            if (enteredFrom == 's') {
                leftTriangle.X1 = 25;
                leftTriangle.Y1 = 15;
                leftTriangle.X2 = 15;
                leftTriangle.Y2 = 30;

                rightTriangle.X1 = 25;
                rightTriangle.Y1 = 15;
                rightTriangle.X2 = 35;
                rightTriangle.Y2 = 30;
            }

            if (enteredFrom == 'w') {
                leftTriangle.X1 = 30;
                leftTriangle.Y1 = 20;
                leftTriangle.X2 = 15;
                leftTriangle.Y2 = 10;

                rightTriangle.X1 = 30;
                rightTriangle.Y1 = 20;
                rightTriangle.X2 = 15;
                rightTriangle.Y2 = 30;
            }

            Canvas.SetLeft(leftTriangle, 20 + (row * 50));
            Canvas.SetTop(leftTriangle, 10 + (col * 50));

            Canvas.SetLeft(rightTriangle, 20 + (row * 50));
            Canvas.SetTop(rightTriangle, 10 + (col * 50));

            MapCanvas.Children.Add(leftTriangle);
            MapCanvas.Children.Add(rightTriangle);
        }

        //=====================================================================

        private void DrawOutline() {
            System.Windows.Shapes.Rectangle outline;

            outline = new System.Windows.Shapes.Rectangle();
            outline.Stroke = new SolidColorBrush(Colors.Black);
            outline.Width = 250;
            outline.Height = 250;
            outline.StrokeThickness = 2;
            Canvas.SetLeft(outline, 20);
            Canvas.SetTop(outline, 10);
            MapCanvas.Children.Add(outline);
        }

        //=====================================================================

        private void DrawLines() {
            for (int x = 0; x < 5; x++) {
                for (int y = 0; y < 5; y++) {
                    DrawLine(x, y);
                }
            }
        }

        //=====================================================================

        private void DrawLine(int x, int y) {
            System.Windows.Shapes.Rectangle line;

            line = new System.Windows.Shapes.Rectangle();
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.Width = 50;
            line.Height = 50;
            line.Fill = new SolidColorBrush(Colors.LawnGreen);
            Canvas.SetLeft(line, 20 + (x * 50));
            Canvas.SetTop(line, 10 + (y * 50));
            MapCanvas.Children.Add(line);
        }

        //=====================================================================

        private void DrawVerticleDoors() {
            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 5; y++) {
                    DrawVerticleDoor(x, y, 0);
                }
            }
        }

        //=====================================================================

        private void DrawVerticleDoor(int x, int y, int color) {
            System.Windows.Shapes.Rectangle verticleDoor;

            verticleDoor = new System.Windows.Shapes.Rectangle();
            verticleDoor.Stroke = new SolidColorBrush(Colors.Black);
            verticleDoor.Width = 10;
            verticleDoor.Height = 10;
            if(color == 0)
                verticleDoor.Fill = new SolidColorBrush(Colors.White);
            else if (color == 1)
                verticleDoor.Fill = new SolidColorBrush(Colors.Red);
            else if (color == 2)
                verticleDoor.Fill = new SolidColorBrush(Colors.Green);
            Canvas.SetLeft(verticleDoor, 65 + (x * 50));
            Canvas.SetTop(verticleDoor, 30 + (y * 50));
            MapCanvas.Children.Add(verticleDoor);
        }

        //=====================================================================

        private void DrawHorizontalDoors() {
            for (int x = 0; x < 5; x++) {
                for (int y = 0; y < 4; y++) {
                    DrawHorizontalDoor(x, y, 0);
                }
            }
        }

        //=====================================================================

        private void DrawHorizontalDoor(int x, int y, int color) {
            System.Windows.Shapes.Rectangle horizontalDoor;

            horizontalDoor = new System.Windows.Shapes.Rectangle();
            horizontalDoor.Stroke = new SolidColorBrush(Colors.Black);
            horizontalDoor.Width = 10;
            horizontalDoor.Height = 10;
            if(color == 0)
                horizontalDoor.Fill = new SolidColorBrush(Colors.White);
            else if(color == 1)
                horizontalDoor.Fill = new SolidColorBrush(Colors.Red);
            else if(color == 2)
                horizontalDoor.Fill = new SolidColorBrush(Colors.Green);
            Canvas.SetLeft(horizontalDoor, 40 + (x * 50));
            Canvas.SetTop(horizontalDoor, 55 + (y * 50));
            MapCanvas.Children.Add(horizontalDoor);
        }

        //=====================================================================

        /*private void DrawExit(int row) {
            System.Windows.Shapes.Rectangle exit;
            exit = new System.Windows.Shapes.Rectangle();
            exit.Stroke = new SolidColorBrush(Colors.Black);
            exit.Width = 15;
            exit.Height = 15;
            exit.Fill = new SolidColorBrush(Colors.Chocolate);
            Canvas.SetLeft(exit, 55 + (4 * 50));
            Canvas.SetTop(exit, 25 + (row * 50));
            MapCanvas.Children.Add(exit);
        }*/

        //=====================================================================

        public void newGameMenuItemClick(object sender, RoutedEventArgs e) {
            DrawMiniMap();
            MapCanvas.IsEnabled = true;
            this.gameCore = new GameCore(this);
            this.askingQuestion = false;
            ResetQuestion();

            CDoorCanvas.IsEnabled = true;
            RDoorCanvas.IsEnabled = true;
            LDoorCanvas.IsEnabled = true;
            BDoorCanvas.IsEnabled = true;
            flagEndGame = false;
            
        }

        //=====================================================================

        public void SubscribeToMaze() {
            this.gameCore.GetMaze().Subscribe(this);
        }

        //=====================================================================

        private void saveGameMenuItemClick(object sender, RoutedEventArgs e) {
            SaveGameWindow saveGameWindow = new SaveGameWindow(this);
            saveGameWindow.Show();
        }

        //=====================================================================

        public void SaveGameInput(String fileName) {
            this.gameCore.SaveGame(fileName);
        }

        //=====================================================================

        private void loadGameMenuItemClick(object sender, RoutedEventArgs e) {
            LoadGameWindow loadGameWindow = new LoadGameWindow(this);
            loadGameWindow.Show();
        }

        //=====================================================================

        public void LoadGameInput(String fileName) {
            if (!this.gameCore.LoadGame(fileName)) {
                MessageBox.Show("The file you specified does not exist!");
            }
            this.gameCore.GetMaze().Subscribe(this);
            DrawMiniMap();
            this.gameCore.UpdateMazeView();
        }

        //=====================================================================

        private void exitMenuItemClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("Thanks for playing!");
            Application.Current.Shutdown();
        }

        //=====================================================================

        private void aboutGameMenuItemClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("Welcome to the Trivia maze!\n\nDeveloped by the Twenty Hats team!\nVersion 1.0", "About");
        }

        //=====================================================================

        private void controlsGameMenuItemClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("The bottom left hand corner you will see a map of\n" +
                            "the maze. \n\nUse it to find your way to the exit " +
                            "\"The Brown Square\" \nby clicking on the doorways," +
                            "or by using the arrow keys,\nto move either forward," +
                            "backward, left, or right. \n\nThe arrow on the minimap will show you the direction\nyou're facing", "Controls");

        }

        //=====================================================================

        private void addQuestionGameMenuItemClick(object sender, RoutedEventArgs e) {
            //MessageBox.Show("Add Question Game");
            //
            //
            //
            //
            //
            //

        }

        //=====================================================================

        private void viewQuestionsGameMenuItemClick(object sender, RoutedEventArgs e) {
            ViewQuestionWindow newWindwo = new ViewQuestionWindow();
            newWindwo.Show();
        }

        //=====================================================================

        void IObserver<Maze>.OnCompleted() {
            //not used in this context
        }

        //=====================================================================

        void IObserver<Maze>.OnError(Exception error) {
            //not used in this context
        }

        //=====================================================================

        void IObserver<Maze>.OnNext(Maze value) {
            Room curRoom = value.GetCurRoom();
            Point curPosition = value.GetCurPoint();
            Floor curFloor = value.GetFloor(value.GetCurFloor());
            
            int playerRow = curPosition.GetRow();
            int playerCol = curPosition.GetCol();

            FindOpenedClosedDoors(curFloor);

            if (curRoom.GetEnteredFrom() == 'n') {
                NorthEntry(curRoom); ;
                MoveTriangle(playerCol, playerRow, 'n');
            } else if (curRoom.GetEnteredFrom() == 'e') {
                EastEntry(curRoom);
                MoveTriangle(playerCol, playerRow, 'e');
            } else if (curRoom.GetEnteredFrom() == 's') {
                SouthEntry(curRoom);
                MoveTriangle(playerCol, playerRow, 's');
            } else if (curRoom.GetEnteredFrom() == 'w') {
                WestEntry(curRoom);
                MoveTriangle(playerCol, playerRow, 'w');
            } else {
                throw new Exception(); //TODO: find the right exception to throw
            }
            this.BDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\Images\door_back.png", UriKind.Relative)));
            
        }

        //=====================================================================

        private void NorthEntry(Room room) {
            String rFilePath = @"..\..\Images\r" + room.GetWDoor().GetFileName();
            String cFilePath = @"..\..\Images\c" + room.GetSDoor().GetFileName();
            String lFilePath = @"..\..\Images\l" + room.GetEDoor().GetFileName();

            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(rFilePath, UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(lFilePath, UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(cFilePath, UriKind.Relative)));
        }

        //=====================================================================

        private void EastEntry(Room room) {
            String rFilePath = @"..\..\Images\r" + room.GetNDoor().GetFileName();
            String cFilePath = @"..\..\Images\c" + room.GetWDoor().GetFileName();
            String lFilePath = @"..\..\Images\l" + room.GetSDoor().GetFileName();

            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(rFilePath, UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(lFilePath, UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(cFilePath, UriKind.Relative)));
        }

        //=====================================================================

        private void SouthEntry(Room room) {
            String rFilePath = @"..\..\Images\r" + room.GetEDoor().GetFileName();
            String cFilePath = @"..\..\Images\c" + room.GetNDoor().GetFileName();
            String lFilePath = @"..\..\Images\l" + room.GetWDoor().GetFileName();

            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(rFilePath, UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(lFilePath, UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(cFilePath, UriKind.Relative)));
        }

        //=====================================================================

        private void WestEntry(Room room) {
            String rFilePath = @"..\..\Images\r" + room.GetSDoor().GetFileName();
            String cFilePath = @"..\..\Images\c" + room.GetEDoor().GetFileName();
            String lFilePath = @"..\..\Images\l" + room.GetNDoor().GetFileName();

            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(rFilePath, UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(lFilePath, UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(cFilePath, UriKind.Relative)));
        }

        //=====================================================================

        void IObserver<Question>.OnCompleted() {
            //not used in this context
        }

        //=====================================================================

        void IObserver<Question>.OnError(Exception error) {
            //not used in this context
        }

        //=====================================================================
        //Question Changes here
        void IObserver<Question>.OnNext(Question value) {
            this.currentQuestion = value;
            this.askingQuestion = true;

            

            /*Test Block
            QuestionMulti tempQuestion = new QuestionMulti();

            //tempQuestion.SetAuxFile("door_closed.png");
            tempQuestion.SetAuxFile("test_sound.wav");
            tempQuestion.SetQType(5);



            this.currentQuestion = tempQuestion;
            //*/

            QuestionBox.Text = this.currentQuestion.ToString();

            EnterButton.IsEnabled = true;

            if (this.currentQuestion.GetQType() == 1) {
                //short
                A_TrueRadioButton.Visibility = Visibility.Hidden;
                B_FalseRadioButton.Visibility = Visibility.Hidden;
                C_RadioButton.Visibility = Visibility.Hidden;
                D_RadioButton.Visibility = Visibility.Hidden;
                AnswerBox.IsEnabled = true;
                AnswerBox.Text = "";

            } else if (this.currentQuestion.GetQType() == 2) {
                //TF
                A_TrueRadioButton.IsEnabled = true;
                A_TrueRadioButton.Content = "True";
                B_FalseRadioButton.IsEnabled = true;
                B_FalseRadioButton.Content = "False";
                C_RadioButton.Visibility = Visibility.Hidden;
                D_RadioButton.Visibility = Visibility.Hidden;
                AnswerBox.Visibility = Visibility.Hidden;
                AnswerBox.IsEnabled = false;
            } else if (this.currentQuestion.GetQType() == 3) {
                //Multi
                A_TrueRadioButton.IsEnabled = true;
                A_TrueRadioButton.Content = "A";
                B_FalseRadioButton.IsEnabled = true;
                B_FalseRadioButton.Content = "B";
                C_RadioButton.IsEnabled = true;
                D_RadioButton.IsEnabled = true;
                AnswerBox.Visibility = Visibility.Hidden;
                AnswerBox.IsEnabled = false;

            } else if(this.currentQuestion.GetQType() == 4 || this.currentQuestion.GetQType() == 5) {
                //Picture & Sound
                A_TrueRadioButton.IsEnabled = true;
                A_TrueRadioButton.Content = "A";
                B_FalseRadioButton.IsEnabled = true;
                B_FalseRadioButton.Content = "B";
                C_RadioButton.IsEnabled = true;
                D_RadioButton.IsEnabled = true;
                AnswerBox.Visibility = Visibility.Hidden;
                AnswerBox.IsEnabled = false;
                PlayButton.IsEnabled = true;
                PlayButton.Visibility = Visibility.Visible;
            }
            else {
                Console.WriteLine("*Error* - MainWindow - Question.OnNext - getType");
            }

        }

        //=====================================================================

        private void RDoorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (!this.askingQuestion && !GameEnds()) {
                this.gameCore.RightDoorClick();
            }
            else if (this.askingQuestion) {
                MessageBox.Show("You must answer the question before proceeding!");
            }
        }

        private void CDoorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (!this.askingQuestion && !GameEnds()) {
                this.gameCore.CenterDoorClick();
            }
            else if (this.askingQuestion) {
                MessageBox.Show("You must answer the question before proceeding!");
            }
        }

        private void LDoorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (!this.askingQuestion && !GameEnds()) {
                this.gameCore.LeftDoorClick();
            }
            else if (this.askingQuestion) {
                MessageBox.Show("You must answer the question before proceeding!");
            }
        }

        private void BDoorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (!this.askingQuestion && !GameEnds()) {
                this.gameCore.BackDoorClick();
            }
            else if (this.askingQuestion) {
                MessageBox.Show("You must answer the question before proceeding!");
            }
        }

        //=====================================================================

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (!this.askingQuestion) {
                try {
                    if (e.Key == Key.Up)
                        this.gameCore.CenterDoorClick();
                    if (e.Key == Key.Right)
                        this.gameCore.RightDoorClick();
                    if (e.Key == Key.Down)
                        this.gameCore.BackDoorClick();
                    if (e.Key == Key.Left)
                        this.gameCore.LeftDoorClick();
                } catch (NullReferenceException) {

                }
            } else if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.Left) {
                MessageBox.Show("You must answer the question before proceeding!");
            } else if (e.Key == Key.P) {
                char from = this.gameCore.GetMaze().GetCurRoom().GetEnteredFrom();
                int col = this.gameCore.GetMaze().GetCurPoint().GetCol();
                int row = this.gameCore.GetMaze().GetCurPoint().GetRow();

                this.gameCore.QuestionAnswered(true);
                QuestionAnswered(row, col, from, 1);
                this.askingQuestion = false;
                ResetQuestion();
            }
        }

        //=====================================================================

        private void ResetQuestion() {

            A_TrueRadioButton.IsEnabled = false;
            A_TrueRadioButton.Visibility = Visibility.Visible;
            A_TrueRadioButton.IsChecked = false;
            A_TrueRadioButton.Content = "A / True";

            B_FalseRadioButton.IsEnabled = false;
            B_FalseRadioButton.Visibility = Visibility.Visible;
            B_FalseRadioButton.IsChecked = false;
            B_FalseRadioButton.Content = "B / False";

            C_RadioButton.IsEnabled = false;
            C_RadioButton.Visibility = Visibility.Visible;
            C_RadioButton.IsChecked = false;

            D_RadioButton.IsEnabled = false;
            D_RadioButton.Visibility = Visibility.Visible;
            D_RadioButton.IsChecked = false;

            EnterButton.IsEnabled = false;

            AnswerBox.IsEnabled = false;
            AnswerBox.Visibility = Visibility.Visible;
            AnswerBox.Text = "Enter Answer";

            PlayButton.IsEnabled = false;
            PlayButton.Visibility = Visibility.Hidden;

            QuestionBox.Text = "Question....";
        }

        //=====================================================================

        private void EnterButton_Click(object sender, RoutedEventArgs e) {

            //Get Current Answer
            String currentAnswer = "A";

            if (this.currentQuestion.GetQType() == 1) { //Short
                currentAnswer = AnswerBox.Text;
            } else if (this.currentQuestion.GetQType() == 2) { //True/False
                if (A_TrueRadioButton.IsChecked.HasValue && A_TrueRadioButton.IsChecked.Value) {
                    currentAnswer = "TRUE";
                } else {
                    currentAnswer = "FALSE";
                }
            }
            else if (this.currentQuestion.GetQType() == 3 || this.currentQuestion.GetQType() == 4 || this.currentQuestion.GetQType() == 5) { //Multiple Choice
                if (A_TrueRadioButton.IsChecked.HasValue && A_TrueRadioButton.IsChecked.Value) {
                    currentAnswer = "A";
                } else if (B_FalseRadioButton.IsChecked.HasValue && B_FalseRadioButton.IsChecked.Value) {
                    currentAnswer = "B";
                } else if (C_RadioButton.IsChecked.HasValue && C_RadioButton.IsChecked.Value) {
                    currentAnswer = "C";
                } else if (D_RadioButton.IsChecked.HasValue && D_RadioButton.IsChecked.Value) {
                    currentAnswer = "D";
                }

            } else {
                Console.WriteLine("*Error* - EnterButton_Click If Statment");
            }
            CheckQuestionAnswer(currentAnswer);
        }

        //=====================================================================
        //Check if quesiton is correct + Open or Lock Door
        private void CheckQuestionAnswer(String currentAnswer) {
            char from = this.gameCore.GetMaze().GetCurRoom().GetEnteredFrom();
            int col = this.gameCore.GetMaze().GetCurPoint().GetCol();
            int row = this.gameCore.GetMaze().GetCurPoint().GetRow();
            if (this.currentQuestion.CheckAnswer(currentAnswer)) {
                MessageBox.Show("CORRECT!");                
                this.gameCore.QuestionAnswered(true);
                QuestionAnswered(row, col, from, 2);
            } else {
                MessageBox.Show("INCORRECT!");
                this.gameCore.QuestionAnswered(false);
                QuestionAnswered(row, col, from, 1);
            }

            this.askingQuestion = false;

            //reset question boxes
            ResetQuestion();
        }

        private void QuestionAnswered(int row, int col, char from, int incorrect) {
            if (from == 'n') {
                if (this.gameCore.getClickDirection().Equals("center")) {
                    DrawHorizontalDoor(col, row - 1, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("back")) {
                    DrawHorizontalDoor(col, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("left")) {
                    DrawVerticleDoor(col - 1, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("right")) {
                    DrawVerticleDoor(col, row, incorrect);
                }
            }
            if (from == 'e') {
                if (this.gameCore.getClickDirection().Equals("center")){
                    DrawVerticleDoor(col - 1, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("back")){
                    DrawVerticleDoor(col, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("left")) {
                    DrawHorizontalDoor(col, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("right")) {
                    DrawHorizontalDoor(col, row - 1, incorrect);
                }
            }
            if (from == 's') {
                if (this.gameCore.getClickDirection().Equals("center")) {
                    DrawHorizontalDoor(col, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("back")) {
                    DrawHorizontalDoor(col, row - 1, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("left")) {
                    DrawVerticleDoor(col - 1, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("right")) {
                    DrawVerticleDoor(col, row, incorrect);
                }
            }
            if (from == 'w') {
                if (this.gameCore.getClickDirection().Equals("center")){
                    DrawVerticleDoor(col, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("back")){
                    DrawVerticleDoor(col - 1, row, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("left")){
                    DrawHorizontalDoor(col, row - 1, incorrect);
                }
                else if (this.gameCore.getClickDirection().Equals("right")){
                    DrawHorizontalDoor(col, row, incorrect);
                }
            }
            
        }
        //=====================================================================

        private void Floor1Button_Click(object sender, RoutedEventArgs e) {
            FindOpenedClosedDoors(this.gameCore.GetMazeFloor(0));
        }

        private void Floor2Button_Click(object sender, RoutedEventArgs e) {
            FindOpenedClosedDoors(this.gameCore.GetMazeFloor(1));
        }

        private void Floor3Button_Click(object sender, RoutedEventArgs e) {
            FindOpenedClosedDoors(this.gameCore.GetMazeFloor(2));
        }

        private void Floor4Button_Click(object sender, RoutedEventArgs e) {
            FindOpenedClosedDoors(this.gameCore.GetMazeFloor(3));
        }

        private void Floor5Button_Click(object sender, RoutedEventArgs e) {
            FindOpenedClosedDoors(this.gameCore.GetMazeFloor(4));
        }

        //=====================================================================

        public static void GameWon() {
            MessageBox.Show("You Won!");
            flagEndGame = true;

            //add more game won stuff here
        }

        //=====================================================================

        public static void GameLost() {
            MessageBox.Show("You lost!");
            flagEndGame = true;

            //add game lost stuff here
        }

        public bool GameEnds() {
            if (flagEndGame) {
                CDoorCanvas.IsEnabled = false;
                RDoorCanvas.IsEnabled = false;
                LDoorCanvas.IsEnabled = false;
                BDoorCanvas.IsEnabled = false;
                return true;
            }
            else {
                return false;
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e) {
            
            if(this.currentQuestion.GetQType()==4){
                ViewPictureWindow tempNewWindow = new ViewPictureWindow();
                tempNewWindow.Show();
                String qFilePath = @"..\..\Images\" + this.currentQuestion.GetAuxFile();
                tempNewWindow.PictureCanvas.Background = new ImageBrush(new BitmapImage(new Uri(qFilePath, UriKind.Relative)));

            }
            else if(this.currentQuestion.GetQType()==5){
                String qFilePath = @"..\..\Sounds\" + this.currentQuestion.GetAuxFile();


                SoundPlayer waveFile = new SoundPlayer(qFilePath);
                waveFile.PlaySync();
            }
        }
    }
}
