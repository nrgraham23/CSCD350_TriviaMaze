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

namespace TriviaMaze_CSCD350
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver<Maze>, IObserver<IQuestion>
    {
        private GameCore gameCore;

        public MainWindow()
        {
            InitializeComponent();
            DrawMiniMap();
            this.gameCore = new GameCore();
            this.gameCore.GetMaze().Subscribe(this);
            this.BDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\Images\door_back.png", UriKind.Relative)));

            DemoTheMap(); //It is important to delete this method call before we implement the game logic

        }

        //=====================================================================
        //Delete this DemoTheMap method when finish the software

        private void DemoTheMap()
        {
            /*//FillInVerticleDoor(0, 0, false);
            FillInHorizonalDoor(0, 0, true);
            FillInHorizonalDoor(1, 0, true);
            /*FillInVerticleDoor(1, 0, false);
            FillInVerticleDoor(2, 0, true);
            FillInHorizonalDoor(2, 0, false);
            FillInHorizonalDoor(2, 1, false);
            FillInVerticleDoor(2, 1, true);
            FillInVerticleDoor(2, 2, true);
            FillInVerticleDoor(2, 3, true);
            FillInHorizonalDoor(2, 2, false);
            FillInHorizonalDoor(2, 3, false);
            FillInVerticleDoor(2, 4, false);
            FillInVerticleDoor(3, 4, true);
            FillInHorizonalDoor(3, 3, false);
            FillInVerticleDoor(3, 3, false);
            FillInHorizonalDoor(4, 3, false);*/

            //MoveTriangle(0, 0);
            //MoveTriangle(0, 1);
            //DrawTriangle(0, 0);
        }

        //=====================================================================
        //

        private void DrawMiniMap()
        {
            DrawOutline();
            DrawLines();
            DrawVerticleDoors();
            DrawHorizontalDoors();
            DrawExit();
        }

        //=====================================================================
        //

        private void MoveTriangle(int x, int y, char enteredFrom)
        {
            if (enteredFrom == 'n')
            {
                DrawLine(x, y - 1);
                DrawVerticleDoor(x, y - 1);
                DrawHorizontalDoor(x, y - 1);
                DrawTriangle(x, y, enteredFrom);
            }
            if (enteredFrom == 'e')
            {
                DrawLine(x - 1, y);
                DrawVerticleDoor(x - 1, y);
                DrawHorizontalDoor(x - 1, y);
                DrawTriangle(x, y, enteredFrom);
            }
            if (enteredFrom == 's')
            {
                DrawLine(x, y + 1);
                DrawVerticleDoor(x, y + 1);
                DrawHorizontalDoor(x, y + 1);
                DrawTriangle(x, y, enteredFrom);
            }
            if (enteredFrom == 'w')
            {
                DrawLine(x + 1, y);
                DrawVerticleDoor(x + 1, y);
                DrawHorizontalDoor(x + 1, y);
                DrawTriangle(x, y, enteredFrom);
            }
        }

        //=====================================================================
        //

        private void DrawTriangle(int x, int y, char enteredFrom)
        {

            System.Windows.Shapes.Line leftTriangle;
            System.Windows.Shapes.Line rightTriangle;

            leftTriangle = new System.Windows.Shapes.Line();
            rightTriangle = new System.Windows.Shapes.Line();

            leftTriangle.Stroke = new SolidColorBrush(Colors.Black);
            rightTriangle.Stroke = new SolidColorBrush(Colors.Black);

            if (enteredFrom == 'n')
            {
                leftTriangle.X1 = 25;
                leftTriangle.Y1 = 30;
                leftTriangle.X2 = 15;
                leftTriangle.Y2 = 15;

                rightTriangle.X1 = 25;
                rightTriangle.Y1 = 30;
                rightTriangle.X2 = 35;
                rightTriangle.Y2 = 15;
            }

            if (enteredFrom == 'e')
            {
                leftTriangle.X1 = 15;
                leftTriangle.Y1 = 20;
                leftTriangle.X2 = 30;
                leftTriangle.Y2 = 10;

                rightTriangle.X1 = 15;
                rightTriangle.Y1 = 20;
                rightTriangle.X2 = 30;
                rightTriangle.Y2 = 30;
            }

            if (enteredFrom == 's')
            {
                leftTriangle.X1 = 25;
                leftTriangle.Y1 = 15;
                leftTriangle.X2 = 15;
                leftTriangle.Y2 = 30;

                rightTriangle.X1 = 25;
                rightTriangle.Y1 = 15;
                rightTriangle.X2 = 35;
                rightTriangle.Y2 = 30;
            }

            if (enteredFrom == 'w')
            {
                leftTriangle.X1 = 30;
                leftTriangle.Y1 = 20;
                leftTriangle.X2 = 15;
                leftTriangle.Y2 = 10;

                rightTriangle.X1 = 30;
                rightTriangle.Y1 = 20;
                rightTriangle.X2 = 15;
                rightTriangle.Y2 = 30;
            }

            Canvas.SetLeft(leftTriangle, 50 + (x * 50));
            Canvas.SetTop(leftTriangle, 10 + (y * 50));

            Canvas.SetLeft(rightTriangle, 50 + (x * 50));
            Canvas.SetTop(rightTriangle, 10 + (y * 50));

            MapCanvas.Children.Add(leftTriangle);
            MapCanvas.Children.Add(rightTriangle);
        }

        //=====================================================================
        //

        private void FillInVerticleDoor(int row, int column, bool wrong)
        {
            System.Windows.Shapes.Rectangle verticleDoor;

            verticleDoor = new System.Windows.Shapes.Rectangle();
            verticleDoor.Stroke = new SolidColorBrush(Colors.Black);
            verticleDoor.Width = 10;
            verticleDoor.Height = 10;
            if (wrong == true)
                verticleDoor.Fill = new SolidColorBrush(Colors.Red);
            else
                verticleDoor.Fill = new SolidColorBrush(Colors.Green);
            Canvas.SetLeft(verticleDoor, 95 + (row * 50));
            Canvas.SetTop(verticleDoor, 30 + (column * 50));
            MapCanvas.Children.Add(verticleDoor);
        }

        //=====================================================================
        //

        private void FillInHorizonalDoor(int row, int column, bool wrong)
        {
            System.Windows.Shapes.Rectangle horizontalDoor;

            horizontalDoor = new System.Windows.Shapes.Rectangle();
            horizontalDoor.Stroke = new SolidColorBrush(Colors.Black);
            horizontalDoor.Width = 10;
            horizontalDoor.Height = 10;
            if (wrong == true)
                horizontalDoor.Fill = new SolidColorBrush(Colors.Red);
            else
                horizontalDoor.Fill = new SolidColorBrush(Colors.Green);
            Canvas.SetLeft(horizontalDoor, 70 + (row * 50));
            Canvas.SetTop(horizontalDoor, 55 + (column * 50));
            MapCanvas.Children.Add(horizontalDoor);
        }

        //=====================================================================
        //

        private void DrawOutline()
        {
            System.Windows.Shapes.Rectangle outline;

            outline = new System.Windows.Shapes.Rectangle();
            outline.Stroke = new SolidColorBrush(Colors.Black);
            outline.Width = 250;
            outline.Height = 250;
            outline.StrokeThickness = 2;
            Canvas.SetLeft(outline, 50);
            Canvas.SetTop(outline, 10);
            MapCanvas.Children.Add(outline);
        }

        //=====================================================================
        //

        private void DrawLines()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    DrawLine(x, y);
                }
            }
        }

        //=====================================================================
        //

        private void DrawLine(int x, int y)
        {
            System.Windows.Shapes.Rectangle line;

            line = new System.Windows.Shapes.Rectangle();
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.Width = 50;
            line.Height = 50;
            line.Fill = new SolidColorBrush(Colors.LawnGreen);
            Canvas.SetLeft(line, 50 + (x * 50));
            Canvas.SetTop(line, 10 + (y * 50));
            MapCanvas.Children.Add(line);
        }

        //=====================================================================
        //

        private void DrawVerticleDoors()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    DrawVerticleDoor(x, y);
                }
            }
        }

        //=====================================================================
        //

        private void DrawVerticleDoor(int x, int y)
        {
            System.Windows.Shapes.Rectangle verticleDoor;

            verticleDoor = new System.Windows.Shapes.Rectangle();
            verticleDoor.Stroke = new SolidColorBrush(Colors.Black);
            verticleDoor.Width = 10;
            verticleDoor.Height = 10;
            verticleDoor.Fill = new SolidColorBrush(Colors.White);
            Canvas.SetLeft(verticleDoor, 95 + (x * 50));
            Canvas.SetTop(verticleDoor, 30 + (y * 50));
            MapCanvas.Children.Add(verticleDoor);
        }

        //=====================================================================
        //

        private void DrawHorizontalDoors()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    DrawHorizontalDoor(x, y);
                }
            }
        }

        //=====================================================================
        //

        private void DrawHorizontalDoor(int x, int y)
        {
            System.Windows.Shapes.Rectangle horizontalDoor;

            horizontalDoor = new System.Windows.Shapes.Rectangle();
            horizontalDoor.Stroke = new SolidColorBrush(Colors.Black);
            horizontalDoor.Width = 10;
            horizontalDoor.Height = 10;
            horizontalDoor.Fill = new SolidColorBrush(Colors.White);
            Canvas.SetLeft(horizontalDoor, 70 + (x * 50));
            Canvas.SetTop(horizontalDoor, 55 + (y * 50));
            MapCanvas.Children.Add(horizontalDoor);
        }

        //=====================================================================
        //

        private void DrawExit(/*int exitColumn*/)
        {
            System.Windows.Shapes.Rectangle exit;
            exit = new System.Windows.Shapes.Rectangle();
            exit.Stroke = new SolidColorBrush(Colors.Black);
            exit.Width = 15;
            exit.Height = 15;
            exit.Fill = new SolidColorBrush(Colors.Chocolate);
            Canvas.SetLeft(exit, 85 + (4 * 50));
            Canvas.SetTop(exit, 25 + (4 /*exitColumn*/ * 50));
            MapCanvas.Children.Add(exit);
        }

        //=====================================================================

        private void newGameMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        //=====================================================================

        private void saveGameMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        //=====================================================================

        private void loadGameMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        //=====================================================================

        private void exitMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        //=====================================================================

        private void aboutGameMenuItemClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome to the Trivia maze!\n\nDeveloped by the Twenty Hats team!\nVersion 1.0", "About");
        }

        //=====================================================================

        private void controlsGameMenuItemClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The bottom left hand corner you will see a map of\n" +
                            "the maze. \n\nUse it to find your way to the exit " +
                            "\"The Brown Square\" \nby clicking on the doorways to move either forward,\n" +
                            "backward, left, or right.\n\nThe arrow will show you the direction you're facing", "Controls");
        }

        //=====================================================================

        private void addQuestionGameMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        //=====================================================================

        private void viewQuestionsGameMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        //=====================================================================

        void IObserver<Maze>.OnCompleted()
        {
            //not used in this context
        }

        //=====================================================================

        void IObserver<Maze>.OnError(Exception error)
        {
            //not used in this context
        }

        //=====================================================================

        void IObserver<Maze>.OnNext(Maze value)
        {
            Room curRoom = value.GetCurRoom();

            if (curRoom.GetEnteredFrom() == 'n')
            {
                NorthEntry(curRoom);
            }
            else if (curRoom.GetEnteredFrom() == 'e')
            {
                EastEntry(curRoom);
            }
            else if (curRoom.GetEnteredFrom() == 's')
            {
                SouthEntry(curRoom);
            }
            else if (curRoom.GetEnteredFrom() == 'w')
            {
                WestEntry(curRoom);
            }
            else
            {
                throw new Exception(); //TODO: find the right exception to throw
            }
        }

        //=====================================================================

        private void NorthEntry(Room room)
        {
            String rFilePath = @"..\..\Images\r" + room.GetWDoor().GetFileName();
            String cFilePath = @"..\..\Images\c" + room.GetSDoor().GetFileName();
            String lFilePath = @"..\..\Images\l" + room.GetEDoor().GetFileName();

            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(rFilePath, UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(lFilePath, UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(cFilePath, UriKind.Relative)));
        }

        //=====================================================================

        private void EastEntry(Room room)
        {
            String rFilePath = @"..\..\Images\r" + room.GetNDoor().GetFileName();
            String cFilePath = @"..\..\Images\c" + room.GetWDoor().GetFileName();
            String lFilePath = @"..\..\Images\l" + room.GetSDoor().GetFileName();

            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(rFilePath, UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(lFilePath, UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(cFilePath, UriKind.Relative)));
        }

        //=====================================================================

        private void SouthEntry(Room room)
        {
            String rFilePath = @"..\..\Images\r" + room.GetEDoor().GetFileName();
            String cFilePath = @"..\..\Images\c" + room.GetNDoor().GetFileName();
            String lFilePath = @"..\..\Images\l" + room.GetWDoor().GetFileName();

            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(rFilePath, UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(lFilePath, UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(cFilePath, UriKind.Relative)));
        }

        //=====================================================================

        private void WestEntry(Room room)
        {
            String rFilePath = @"..\..\Images\r" + room.GetSDoor().GetFileName();
            String cFilePath = @"..\..\Images\c" + room.GetEDoor().GetFileName();
            String lFilePath = @"..\..\Images\l" + room.GetNDoor().GetFileName();

            this.RDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(rFilePath, UriKind.Relative)));
            this.LDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(lFilePath, UriKind.Relative)));
            this.CDoorCanvas.Background = new ImageBrush(new BitmapImage(new Uri(cFilePath, UriKind.Relative)));
        }

        //=====================================================================
        //
        void IObserver<IQuestion>.OnCompleted()
        {
            //not used in this context
        }

        //=====================================================================
        //
        void IObserver<IQuestion>.OnError(Exception error)
        {
            //not used in this context
        }

        //=====================================================================
        //
        void IObserver<IQuestion>.OnNext(IQuestion value)
        {

        }

        //=====================================================================
        //
        private void toolsMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RDoorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.gameCore.RightDoorClick();
        }

        private void CDoorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.gameCore.CenterDoorClick();
        }

        private void LDoorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.gameCore.LeftDoorClick();
        }

        private void BDoorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.gameCore.BackDoorClick();
        }



    }
}
