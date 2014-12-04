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
    /// Interaction logic for DifficultyInput.xaml
    /// </summary>
    public partial class DifficultyInput : Window {
        private GameCore gameCore;

        public DifficultyInput(GameCore gameCore) {
            InitializeComponent();
            this.gameCore = gameCore;
        }

        private void DifficultInputButton_Click(object sender, RoutedEventArgs e) {
            String input = this.DifficultInputTextBox.Text;

            if(input.Equals("1") || input.ToLower().Equals("easy")) {
                gameCore.InitializeMaze(0);
                this.Close();
            } else if (input.Equals("2") || input.ToLower().Equals("hard")) {
                gameCore.InitializeMaze(1);
                this.Close();
            } else {
                MessageBox.Show("You must enter a valid difficulty value in the text box above!");
            }
        }
    }
}
