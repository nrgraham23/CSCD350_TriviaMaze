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
    /// Interaction logic for SaveGameWindow.xaml
    /// </summary>

    public partial class SaveGameWindow : Window {
        private MainWindow mainWindow;

        public SaveGameWindow(MainWindow mainWindow) {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void saveGameOKButton_Click(object sender, RoutedEventArgs e) {
            mainWindow.SaveGameInput(this.fileNameTextBox.Text);
            this.Close();
        }
    }
}
