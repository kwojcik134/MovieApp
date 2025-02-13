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
    /// Interaction logic for AddMovie.xaml
    /// </summary>
    public partial class AddMovie : Window
    {
        public AddMovie()
        {
            InitializeComponent();
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Director_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Year_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var title = Title.Text;
            var director = Director.Text;
            var year = Year.Text;

            if (title == null || director == null || year  == null)
            {
                MessageBox.Show("Fill out all the information");
            }
            else
            {
                Database.AddMovie(title, director, year);
                MessageBox.Show("Movie added successfully!");
            }
        }
    }
}
