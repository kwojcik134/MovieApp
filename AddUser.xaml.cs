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

namespace MovieAppWPF
{
    /// <summary>
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        Database database = new Database();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = (string)UserName.Text;
            database.AddUser(username);
        }

        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
