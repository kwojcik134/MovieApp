using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;

namespace MovieAppWPF
{
    public class Database
    {
        public Database() { }

        // Creates tables if they don't exist
        public void DatabaseStart() //App.xaml.cs
        {
            using (var connection = new SqliteConnection("Data Source=database.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                // Create Users table
                command.CommandText = @" 
                                              CREATE TABLE If NOT EXISTS Users (
                                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                              Username TEXT NOT NULL UNIQUE,
                                          );";
                command.ExecuteNonQuery();

                // Create Movies table
                command.CommandText = @"CREATE TABLE If NOT EXISTS Movies (
                                               Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                               Title TEXT NOT NULL,
                                               Director TEXT NOT NULL,
                                               Year TEXT NOT NULL
                                           );";
                command.ExecuteNonQuery();

                // Create MovieReviews table
                command.CommandText = @"CREATE TABLE If NOT EXISTS MovieReviews (
                                                     Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                     UserId INTEGER NOT NULL,
                                                     MovieId INTEGER NOT NULL,
                                                     Rating TEXT NOT NULL,
                                                     Review TEXT
                                                 );";
                command.ExecuteNonQuery();
            }
        }
        // Adding movies
        public static void AddMovie(string title, string director, string year)
        {

            using var connection = new SqliteConnection("Data Source=Movies.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                                         INSERT INTO Movies (Title, Director, Year)
                                         VALUES ($title), ($director), ($year);
                                         ";
            command.Parameters.AddWithValue("$title", title);
            command.Parameters.AddWithValue("$director", director);
            command.Parameters.AddWithValue("$year", year);
            command.ExecuteNonQuery();
        }
        //Deleting movies
        public static void DeleteMovie(string title)
        {

            using var connection = new SqliteConnection("Data Source=Movies.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                                         DELETE FROM Movies
                                         WHERE Title = $title;
                                         ";
            command.Parameters.AddWithValue("$title", title);
            command.ExecuteNonQuery();
        }

        //Adding users
        public void AddUser(string username) // AddUser.xaml.cs
        {

            using var connection = new SqliteConnection("Data Source=Movies.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                                         INSERT INTO Users (Username)
                                         VALUES ($username);
                                         ";
            command.Parameters.AddWithValue("$username", username);
            command.ExecuteNonQuery();
        }
        //Deleting users
        public void DeleteUser(string? username) //UserList.xalm.cs
        {

            using var connection = new SqliteConnection("Data Source=Movies.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                                         DELETE FROM Users
                                         WHERE Username = $username;
                                         ";
            command.Parameters.AddWithValue("$username", username);
            command.ExecuteNonQuery();
        }

        // Adding a review
        public static void AddMovieReview(string username, string title, int rating, string review)
        {
            var un = username;
            var tit = title;

            //Finding UserId by Username
            using (var connection = new SqliteConnection("Data Source=Movies.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                                            SELECT Id 
                                            FROM Users
                                            WHERE Username = $username;
                                      ";
                command.Parameters.AddWithValue("$username", un);
                var uId = command.ExecuteScalar();

                //Finding MovieId by Title

                command.CommandText = @"
                                            SELECT Id 
                                            FROM Movies
                                            WHERE Title = $title;
                                      ";
                command.Parameters.AddWithValue("$title", tit);
                var mId = command.ExecuteScalar();

                //Adding review to the table
                command.CommandText = @"
                                         INSERT INTO MovieReviews (UserId, MovieId, Rating, Review)
                                         VALUES ($userid), ($movieid), ($rating), ($review);
                                         ";
                command.Parameters.AddWithValue("$userid", uId);
                command.Parameters.AddWithValue("$movieid", mId);
                command.Parameters.AddWithValue("$rating", rating);
                command.Parameters.AddWithValue("$review", review);
                command.ExecuteNonQuery();
            }
        }

        //Displaying users
        public List<string> DisplayUsers() // UserList.xaml.cs
        {
            var Users = new List<string>();
            using (var connection = new SqliteConnection("Data Source=Movies.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Username
                                        FROM Users;
                                       ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Users.Add(reader.GetString(0));
                    }
                }
            }
            return Users;
        }

        //Displaying all of user's reviews
        public List<Movie> UserReviews(string username)
        {
            var userReviews = new List<Movie>();
            var un = username;
            using (var connection = new SqliteConnection("Data Source=Movies.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                //Getting UserId
                command.CommandText = @"
                                            SELECT Id 
                                            FROM Users
                                            WHERE Username = $username;
                                      ";
                command.Parameters.AddWithValue("$username", un);
                var uId = command.ExecuteScalar();
                //Getting movies 
                command.CommandText = @"
                                        SELECT Title, Director, Year, Rating, Review
                                        FROM Movies
                                        JOIN MovieReviews on Movies.Id = MovieReviews.MovieId
                                        WHERE Movies.UserId = $uId;
                                       ";
                command.Parameters.AddWithValue("$uId", uId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var title = reader.GetString(0);
                        var director = reader.GetString(1);
                        var year = reader.GetString(2);
                        var rating = reader.GetString(3);
                        var review = reader.GetString(4);
                        userReviews.Add(new Movie(title, director, year, rating, review));
                    }
                }
            }
            return userReviews;
        }

        //Displaying all of movie's reviews
        public List<string> Reviews(string title)
        {
            var reviews = new List<string>();
            var tit = title;
            using (var connection = new SqliteConnection("Data Source=Movies.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                // Getting MovieId
                command.CommandText = @"
                                            SELECT Id 
                                            FROM Movies
                                            WHERE Title = $title;
                                      ";
                command.Parameters.AddWithValue("$title", tit);
                var mId = command.ExecuteScalar();
                // Getting reviews
                command.CommandText = @"SELECT Review
                                        FROM MovieReviews
                                        WHERE MovieId = $mId;
                                       ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(reader.GetString(0));
                    }
                }
            }
            return reviews;
        }

        //Movie average rating
        public int? AverageRating(string title)
        {
            int? rating;
            var tit = title;
            using (var connection = new SqliteConnection("Data Source=Movies.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                // Getting MovieId
                command.CommandText = @"
                                            SELECT Id 
                                            FROM Movies
                                            WHERE Title = $title;
                                      ";
                command.Parameters.AddWithValue("$title", tit);
                var mId = command.ExecuteScalar();
                // Average rating
                command.CommandText = @"
                                            SELECT AVG(Rating)
                                            FROM MovieReviews
                                            WHERE MovieId = $mId;
                                      ";
                command.Parameters.AddWithValue("$mId", mId);
                rating = (int?)command.ExecuteScalar();
            }
            if (rating == null) { return 0; }
            return rating;
        }
    }
    
}

