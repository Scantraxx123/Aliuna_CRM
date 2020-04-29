using Aliuna_Kundenmanagement.Controller;
using System.Windows;

namespace Aliuna_Kundenmanagement
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        //Method will get fired, when exiting the Application
        //Any connection will be closed after exiting
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            DatabaseController dh = DatabaseController.GetInstance();
            dh.CloseConnection();
        }
    }
}
