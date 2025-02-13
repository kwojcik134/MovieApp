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
    /// Interaction logic for MoviesList.xaml
    /// </summary>
    public partial class MoviesList : Window
    {
        Database database = new Database();
        List<string> Movies = new List<string>();
        public MoviesList()
        {
            InitializeComponent();
            Movies = database.DisplayMovies();
            MoviesListBox.ItemsSource = Movies;
        }

        // Choose user to display
        private void ChooseMovie(object sender, RoutedEventArgs e)
        {
            var selectedMovie = (string?)MoviesListBox.SelectedItem;
            if (selectedMovie != null)
            {
                MovieWindow movieWindow = new MovieWindow(selectedMovie);
                movieWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a movie");
            }
        }

        private void DeleteMovie(object sender, RoutedEventArgs e)
        {
            var selectedMovie = (string?)MoviesListBox.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + selectedMovie, "Delete Movie", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Database.DeleteMovie(selectedMovie);
                MessageBox.Show("Movie deleted successfully");
            }
        }

        private void AddNewMovie(object sender, RoutedEventArgs e)
        {
            AddMovie addMovie = new AddMovie();
            addMovie.Show();
        }
    }
}
