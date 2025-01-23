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
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        Database database = new Database();
        public UserList()
        {
            InitializeComponent();
            List<string> Users = database.DisplayUsers();
            UserListBox.ItemsSource = Users;
        }

        // Choose user to display
        private void ChooseUser(object sender, RoutedEventArgs e)
        {
            var selectedUser = (string?)UserListBox.SelectedItem;
            if (selectedUser != null) 
            {   
                UserWindow userWindow = new UserWindow(selectedUser);
                userWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a User");
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            var selectedUser = (string?)UserListBox.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + selectedUser, "Delete User", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                database.DeleteUser(selectedUser);
                MessageBox.Show("User deleted successfully");
            }
           
        }

        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }
    }
}
