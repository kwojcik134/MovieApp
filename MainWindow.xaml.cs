﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewUserList(object sender, RoutedEventArgs e)
        {
            UserList userlist = new UserList();
            userlist.Show();
        }

        private void ViewMovieList(object sender, RoutedEventArgs e)
        {
            MoviesList movieslist = new MoviesList();
            movieslist.Show();
        }
    }
}