using Aliuna.Controller;
using Aliuna.Model;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Aliuna.View
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

        private void InitializeWindow()
        {
            ResetDatagrid();
            ResetTextBoxes();
        }

        private void ResetDatagrid()
        {
            //var temp1 = BaseModel<Order>.GetAll();
            var temp = BaseModel<Customer>.GetAll();
            customerTable.ItemsSource = temp;
        }

        private void ResetTextBoxes()
        {
            idTB.Text = string.Empty;
            companyTB.Text = string.Empty;
            fnTB.Text = string.Empty;
            lnTB.Text = string.Empty;
            emailTB.Text = string.Empty;
            streetTB.Text = string.Empty;
            hnTB.Text = string.Empty;
            pcTB.Text = string.Empty;
            cityTB.Text = string.Empty;
            countryTB.Text = string.Empty;
            faxTB.Text = string.Empty;
            phoneTB.Text = string.Empty;
        }

        private void LoadDatabaseButtonCT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create OpenFileDialog 
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "LiteDB Database |*.db"; // Filter files by extension

                // Get the selected file name and display in a TextBox 
                if (dlg.ShowDialog() == true)
                {
                    var result = Interaction.InputBox("Enter Password (if no Password, then let it empty)?", "Password");
                    DatabaseController.SetDatabase(dlg.FileName, result);
                    InitializeWindow();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
                return;
            }

        }

        private void NewDatabaseButtonCT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "database"; // Default file name
                dlg.DefaultExt = ".db"; // Default file extension
                dlg.Filter = "LiteDB Database |*.db"; // Filter files by extension

                // Process save file dialog box results
                if (dlg.ShowDialog() == true)
                {
                    var result = Interaction.InputBox("Enter Password (if no Password, then let it empty)?", "Password");
                    DatabaseController.SetDatabase(dlg.FileName, result);
                    InitializeWindow();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
                return;
            }

        }

        private void DeleteCustomerButtonCT_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            var selectedItems = customerTable.SelectedItems;
            List<Customer> toDeleteList = new List<Customer>();
            Customer toDeleteTemp;
            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    if (!item.Equals(CollectionView.NewItemPlaceholder))
                    {
                        toDeleteTemp = (Customer)item;
                        toDeleteList.Add(toDeleteTemp);
                        msg += $"{toDeleteTemp.Id}\n";
                    }

                }
                if (!msg.Equals(string.Empty))
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to delete the following customers?\nIDs:\n" + msg,
                                              "Confirmation",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        for (int i = 0; i < toDeleteList.Count; i++)
                        {
                            toDeleteList[i].Delete();
                        }

                    }
                }
            }
            ResetDatagrid();
            ResetTextBoxes();

        }

        private void customerTable_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (customerTable.SelectedItem != null)
            {
                var customer = (Customer)customerTable.SelectedItem;
                idTB.Text = $"{customer.Id}";
                companyTB.Text = $"{customer.CompanyName}";
                fnTB.Text = $"{customer.FirstName}";
                lnTB.Text = $"{customer.LastName}";
                emailTB.Text = $"{customer.Email}";
                streetTB.Text = $"{customer.Street}";
                hnTB.Text = $"{customer.Housenumber}";
                pcTB.Text = $"{customer.Postcode}";
                cityTB.Text = $"{customer.City}";
                countryTB.Text = $"{customer.Country}";
                phoneTB.Text = $"{customer.PhoneNumber}";
                faxTB.Text = $"{customer.FaxNumber}";
            }
        }

        private void NewDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (companyTB.Text.Equals(string.Empty) && fnTB.Text.Equals(string.Empty) && lnTB.Text.Equals(string.Empty))
            {
                MessageBox.Show("Please enter the companyname or at least first AND last name!");
            }
            else
            {
                if (companyTB.Text.Equals(string.Empty) && !fnTB.Text.Equals(string.Empty) && lnTB.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Please enter the companyname or at least first AND last name!");
                }
                else if (companyTB.Text.Equals(string.Empty) && fnTB.Text.Equals(string.Empty) && !lnTB.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Please enter the companyname or at least first AND last name!");
                }
                else
                {
                    if (App.isDBConOpen)
                    {
                        Customer toSave = new Customer
                        {
                            CompanyName = companyTB.Text,
                            FirstName = fnTB.Text,
                            LastName = lnTB.Text,
                            Email = emailTB.Text,
                            Street = streetTB.Text,
                            Housenumber = hnTB.Text,
                            Postcode = pcTB.Text,
                            City = cityTB.Text,
                            Country = countryTB.Text,
                            PhoneNumber = phoneTB.Text,
                            FaxNumber = faxTB.Text
                        };
                        toSave.Save();
                        ResetDatagrid();
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
            if (customerTable.SelectedItem != null)
            {
                var customer = (Customer)customerTable.SelectedItem;
                customer.CompanyName = companyTB.Text;
                customer.FirstName = fnTB.Text;
                customer.LastName = lnTB.Text;
                customer.Email = emailTB.Text;
                customer.Street = streetTB.Text;
                customer.Housenumber = hnTB.Text;
                customer.Postcode = pcTB.Text;
                customer.City = cityTB.Text;
                customer.Country = countryTB.Text;
                customer.FaxNumber = faxTB.Text;
                customer.PhoneNumber = phoneTB.Text;
                customer.UpdatedAt = DateTime.Now;
                customer.Save();
                ResetDatagrid();
                ResetTextBoxes();
            }
            else MessageBox.Show("To update a set of data, you have to select one item!");

        }

        private void searchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.isDBConOpen)
            {
                if (!searchTB.Text.Equals(string.Empty))
                {
                    customerTable.ItemsSource = BaseModel<Customer>.GetAll(x => x.City.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                        || x.CompanyName.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.FirstName.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.LastName.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.Email.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.Street.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.Housenumber.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.City.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.Postcode.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.Country.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.PhoneNumber.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || x.FaxNumber.IndexOf(searchTB.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    customerTable.ItemsSource = BaseModel<Customer>.GetAll();
                }
            }
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            var customer = (Customer)row.Item;
            OrderWindow order = new OrderWindow(customer);
            order.Show();
            // Some operations with this row
        }

        private void configureEmployeesMI_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow ew = new EmployeeWindow();
            ew.Show();
        }

        private void configureProductsMI_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow pw = new ProductWindow();
            pw.Show();
        }
    }
}


