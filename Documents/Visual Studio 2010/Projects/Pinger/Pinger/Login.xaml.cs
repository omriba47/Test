using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pinger
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Selected.selectedTeam = "Hamal";
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Selected.selectedTeam = "Tzyr";
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Selected.selectedTeam = "Darom";
            this.Close();
        }
    }
}
