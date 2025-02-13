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
    /// Interaction logic for MovieWindow.xaml
    /// </summary>

    public partial class MovieWindow : Window
    {
        string movieTitle;
        Database database = new Database();
        List<string> reviews = new List<string>();
        public MovieWindow(string title)
        {
            InitializeComponent();
            movieTitle = title;
            reviews = database.Reviews(movieTitle);
            ReviewListBox.ItemsSource = reviews;
            Movie movieData = database.MovieData(movieTitle);
            MovieInfo.Text = movieData.Title + "  " + movieData.Year + "\n" + movieData.Director;
            float rating  = database.AverageRating(movieTitle);
            RatingAverage.Text = rating.ToString("F2");
        }

        
    }
}
