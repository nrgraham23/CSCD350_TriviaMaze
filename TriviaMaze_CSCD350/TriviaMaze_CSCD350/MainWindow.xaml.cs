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
    public partial class MainWindow : Window {
        private GameCore gameCore;

        public MainWindow() {
            InitializeComponent();
            this.gameCore = new GameCore();
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
    }
}
