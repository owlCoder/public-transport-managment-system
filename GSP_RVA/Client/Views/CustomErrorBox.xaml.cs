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

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for CustomErrorBox.xaml
    /// </summary>
    public partial class CustomErrorBox : Window
    {
        public CustomErrorBox(string message)
        {
            InitializeComponent();
            DataContext = this;
            Message = message;
        }

        public string Message { get; set; }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Zatvorite dijalog prozor
            this.Close();
        }
    }
}
