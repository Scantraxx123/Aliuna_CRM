using Aliuna_Kundenmanagement.Helper;
using Aliuna_Kundenmanagement.Model;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        }

        private void ResetWindow()
        {
            customerTable.ItemsSource = DatabaseHelper.GetCustomers();

        }

        private void ResetTextBoxes()
        {
            idTB.Text = $"";
            companyTB.Text = $"";
            fnTB.Text = $"";
            lnTB.Text = $"";
            emailTB.Text = $"";
            streetTB.Text = $"";
            hnTB.Text = $"";
            pcTB.Text = $"";
            cityTB.Text = $"";
            countryTB.Text = $"";
        }

        private void LoadDatabaseButtonCT_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQLite Database |*.db"; // Filter files by extension

            // Get the selected file name and display in a TextBox 
            if (dlg.ShowDialog() == true) InitializeWindow(dlg.FileName);
        }

        private void NewDatabaseButtonCT_Click(object sender, RoutedEventArgs e)
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

        private void DeleteCustomerButtonCT_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            var selectedItems = customerTable.SelectedItems;
            List<int> toDeleteCustomers = new List<int>();
            int toDeleteTemp;
            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    if (item != CollectionView.NewItemPlaceholder)
                    {
                        toDeleteTemp = ((Customer)item).ID;
                        toDeleteCustomers.Add(toDeleteTemp);
                        msg += $"{toDeleteTemp}\n";
                    }

                }
                if (!msg.Equals(String.Empty))
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to delete the following customers?\n" + msg,
                                              "Confirmation",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        for (int i = 0; i < toDeleteCustomers.Count; i++)
                        {
                            DatabaseHelper.DeleteCustomer(toDeleteCustomers[0]);
                        }
                        ResetWindow();
                    }
                }
            }

        }

        private void customerTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (customerTable.SelectedItem != null)
            {
                idTB.IsReadOnly = true;
                var customer = (Customer)customerTable.SelectedItem;
                idTB.Text = $"{customer.ID}";
                companyTB.Text = $"{customer.company}";
                fnTB.Text = $"{customer.firstName}";
                lnTB.Text = $"{customer.lastName}";
                emailTB.Text = $"{customer.email}";
                streetTB.Text = $"{customer.street}";
                hnTB.Text = $"{customer.housenumber}";
                pcTB.Text = $"{customer.postcode}";
                cityTB.Text = $"{customer.city}";
                countryTB.Text = $"{customer.country}";
            }
        }

        private void NewDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (companyTB.Text.Equals(String.Empty) && fnTB.Text.Equals(String.Empty) && lnTB.Text.Equals(String.Empty))
            {
                MessageBox.Show("Please enter the companyname or at least first AND last name!");
            }
            else
            {
                if (companyTB.Text.Equals(String.Empty) && !fnTB.Text.Equals(String.Empty) && lnTB.Text.Equals(String.Empty))
                {
                    MessageBox.Show("Please enter the companyname or at least first AND last name!");
                }
                if (companyTB.Text.Equals(String.Empty) && fnTB.Text.Equals(String.Empty) && !lnTB.Text.Equals(String.Empty))
                {
                    MessageBox.Show("Please enter the companyname or at least first AND last name!");
                }
                else
                {
                    Customer toSave = new Customer(companyTB.Text, fnTB.Text, lnTB.Text, emailTB.Text, streetTB.Text, hnTB.Text, pcTB.Text, cityTB.Text, countryTB.Text);
                    if (DatabaseHelper.isConnectionOpen())
                    {
                        DatabaseHelper.AddCustomer(toSave);
                        ResetWindow();
                        ResetTextBoxes();
                    }
                    else MessageBox.Show("Please establish a database connection!");
                }

            }

        }

        private void ClearDataButton_Click(object sender, RoutedEventArgs e)
        {
            customerTable.SelectedIndex = -1;
            ResetTextBoxes();
        }

        private void UpdateDataButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


