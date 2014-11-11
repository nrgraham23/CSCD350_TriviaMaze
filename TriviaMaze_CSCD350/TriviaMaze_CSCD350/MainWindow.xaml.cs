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

namespace TriviaMaze_CSCD350 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver<Maze>, IObserver<Short_Answer_Question> {
        private GameCore gameCore;

        public MainWindow() {
            InitializeComponent();
            drawMiniMap();
            this.gameCore = new GameCore();
            this.gameCore.GetMaze().Subscribe(this);
            InitDoors();
            
        }

        private void drawMiniMap()
        {
            System.Windows.Shapes.Rectangle outline;
            System.Windows.Shapes.Rectangle line;
            
            outline = new System.Windows.Shapes.Rectangle();
            outline.Stroke = new SolidColorBrush(Colors.Black);
            outline.Width = 250;
            outline.Height = 250;
            outline.StrokeThickness = 2;
            Canvas.SetLeft(outline, 50);
            Canvas.SetTop(outline, 10);
            MapCanvas.Children.Add(outline);

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    line = new System.Windows.Shapes.Rectangle();
                    line.Stroke = new SolidColorBrush(Colors.Black);
                    line.Width = 50;
                    line.Height = 50;
                    Canvas.SetLeft(line, 50 + (x * 50));
                    Canvas.SetTop(line, 10 + (y * 50));
                    MapCanvas.Children.Add(line);
                }
            }

            //The following will fill in the indiviual squares of the minimap depending on the row and colum

            //System.Windows.Shapes.Rectangle fill;
            //fill = new System.Windows.Shapes.Rectangle();
            //fill.Fill = new SolidColorBrush(Colors.,color>);
            //fill.Width = 50;
            //fill.Height = 50;
            //Canvas.SetLeft(fill, 50 + (<row> * 50));
            //Canvas.SetTop(fill, 10 + (<colum> * 50));
            //MapCanvas.Children.Add(fill);
        }

        private void InitDoors() {
            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\Images\rdoor_closed.png", UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\Images\ldoor_closed.png", UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\Images\cdoor_closed.png", UriKind.Relative)));
            this.BDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\Images\door_back.png", UriKind.Relative)));
        }

        private void newGameMenuItemClick(object sender, RoutedEventArgs e) {

        }

        private void saveGameMenuItemClick(object sender, RoutedEventArgs e) {

        }

        private void loadGameMenuItemClick(object sender, RoutedEventArgs e) {

        }

        private void exitMenuItemClick(object sender, RoutedEventArgs e) {

        }

        private void aboutGameMenuItemClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("Welcome to the Trivia maze!\n\nDeveloped by the Twenty Hats team!\nVersion 1.0", "About");
        }

        private void controlsGameMenuItemClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("The bottom left hand corner you will see a map of\n" +
                            "the maze. \n\nUse it to find your way from where you entered \n" +
                            "(insert color here) to the exit (insert color here)\n" +
                            "by clicking or the doorways to move either forward,\n" +
                            "backward, left, or right.", "Controls");
        }

        private void addQuestionGameMenuItemClick(object sender, RoutedEventArgs e) {

        }

        private void viewQuestionsGameMenuItemClick(object sender, RoutedEventArgs e) {

        }

        void IObserver<Maze>.OnCompleted() {
            //not used in this context
        }

        void IObserver<Maze>.OnError(Exception error) {
            //not used in this context
        }

        void IObserver<Maze>.OnNext(Maze value) {
            //this.RDoorsImage.Source = new BitmapImage(new Uri(@"..\..\Images\rdoor_closed", UriKind.Relative));
            //Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\..\TriviaMaze\door.png", UriKind.Relative)));

            //update minimap
        }

        //=====================================================================
        //
        void IObserver<Short_Answer_Question>.OnCompleted() {
            //not used in this context
        }

        //=====================================================================
        //
        void IObserver<Short_Answer_Question>.OnError(Exception error) {
            //not used in this context
        }

        //=====================================================================
        //
        
        /*
        void IObserver<Short_Answer_Question>.OnNext(Maze value) {
            //Updated MainWindow stuff like enable check boxes
            //Get information to pass to Question classes to check answers
        }
        */
        //=====================================================================
        //
        private void toolsMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }


        void IObserver<Short_Answer_Question>.OnNext(Short_Answer_Question value) {
            throw new NotImplementedException();
        }
    }
}
