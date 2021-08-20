using Macrix_Application.View;
using Macrix_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Macrix_Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MainWindow _app;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var xmlFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            var context = new MainWindowViewModel(xmlFilePath + "\\Macrix_Application_Data.xml");
            _app = new MainWindow() { DataContext = context};
            _app.Show();
        }
    }
}
