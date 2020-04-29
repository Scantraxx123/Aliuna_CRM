using Aliuna_Kundenmanagement.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            DatabaseHelper dh = DatabaseHelper.GetInstance();
            dh.CloseConnection();
        }
    }
}
