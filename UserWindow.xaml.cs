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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        string userName;
        Database database = new Database();
        public UserWindow(string username)
        {
            InitializeComponent();
            userName = username;
            Username.Text = userName;
            List<Movie> userReviews = new List<Movie>();
            userReviews = database.UserReviews(userName);
            UserMoviesGrid.ItemsSource = userReviews;
        }

        private void AddReview(object sender, RoutedEventArgs e)
        {
            AddReview addReview = new AddReview(userName);
            addReview.Show();
        }
    }
}
