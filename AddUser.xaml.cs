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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        string? username;
        private void UsernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            username = UsernameBox.Text;
            if (username == null)
            {
                MessageBox.Show("Type the new user name");
            }
            else
            {
                Database.AddUser(username);
                MessageBox.Show("User added successfully");
            }
        }
    }
}
