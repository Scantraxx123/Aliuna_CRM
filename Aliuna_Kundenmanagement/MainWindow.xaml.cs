using Aliuna_Kundenmanagement.Helper;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows;


namespace Aliuna_Kundenmanagement
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeWindow(string path)
        {
            DatabaseHelper.CloseConnection();
            DatabaseHelper.EstablishConnection(path);
            customerTable.ItemsSource = DatabaseHelper.GetCustomers();
            customerTable.Visibility = Visibility.Visible;
        }

        private void loadDatabaseButtonCT_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog();

            // Get the selected file name and display in a TextBox 
            if (dlg.ShowDialog() == true) InitializeWindow(dlg.FileName);
        }



        private void newDatabaseButtonCT_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "database"; // Default file name
            dlg.DefaultExt = ".db"; // Default file extension
            dlg.Filter = "SQLite Database |*.db"; // Filter files by extension

            // Process save file dialog box results
            if (dlg.ShowDialog() == true)
            {
                DatabaseHelper.CreateDatabase(dlg.FileName);
                InitializeWindow(dlg.FileName);
            }

        }




    }
}

