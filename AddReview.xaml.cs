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
    /// Interaction logic for AddReview.xaml
    /// </summary>
    /// 
    
    public partial class AddReview : Window
    {
        string title;
        int rating;
        string userName;
        Database database = new Database();
        public AddReview(string username)
        {
            InitializeComponent();
            userName = username;
            LoadData();
        }

        public void LoadData()
        {
            List<string> titles = new List<string>();
            titles = database.DisplayMovies();
            MovieTitle.ItemsSource = titles;
            List<int> ratings = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            Rating.ItemsSource = ratings;
        }

        private void MovieTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Rating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var review = Review.Text;

            if (MovieTitle.SelectedItem is string movie)
            {
                title = movie;
            }

            if (Rating.SelectedItem is int rate)
            {
                rating = rate;
            }

            if (!string.IsNullOrEmpty(title) && rating != 0)
            {
                database.AddMovieReview(userName, title, rating, review);
                MessageBox.Show("Review added successfully");
            }
            else
            {
                MessageBox.Show("Please select both a movie and a rating.");
            }
        }
    }
}
