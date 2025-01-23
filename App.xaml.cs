using System.Configuration;
using System.Data;
using System.Windows;

namespace MovieAppWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Loading the database (creating tables if not there yet)
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Database database = new Database();
            database.DatabaseStart();
        }
    }

}
