using Aliuna.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Aliuna.View
{
    /// <summary>
    /// Interaktionslogik für EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
            if (App.isDBConOpen) ResetDatagrid();
        }

        private void ResetDatagrid()
        {
            employeeTable.ItemsSource = BaseModel<Employee>.GetAll();
        }

        private void ResetTextBoxes()
        {
            idTB.Text = string.Empty;
            firstNameTB.Text = string.Empty;
            lastNameTB.Text = string.Empty;
            acronymTB.Text = string.Empty;
            departmentTB.Text = string.Empty;
        }

        private void ClearEmployeeFieldsBT_Click(object sender, RoutedEventArgs e)
        {
            employeeTable.SelectedIndex = -1;
            ResetTextBoxes();
        }

        private void CreateEmployeeBT_Click(object sender, RoutedEventArgs e)
        {
            if (!firstNameTB.Text.Equals(string.Empty) && !lastNameTB.Text.Equals(string.Empty) && !departmentTB.Text.Equals(string.Empty) && !acronymTB.Text.Equals(string.Empty))
            {
                if (App.isDBConOpen)
                {
                    var toSave = new Employee
                    {
                        Department = departmentTB.Text,
                        FirstName = firstNameTB.Text,
                        LastName = lastNameTB.Text,
                        Acronym = acronymTB.Text
                    };
                    toSave.Save();
                    ResetDatagrid();
                    ResetTextBoxes();
                }
                else MessageBox.Show("Please establish a database connection!");
            }
            else MessageBox.Show("Please fill out the complete form!");
        }

        private void UpdateEmployeeBT_Click(object sender, RoutedEventArgs e)
        {
            if (employeeTable.SelectedItem != null)
            {
                var employee = (Employee)employeeTable.SelectedItem;
                employee.FirstName = firstNameTB.Text;
                employee.LastName = lastNameTB.Text;
                employee.Acronym = acronymTB.Text;
                employee.Department = departmentTB.Text;
                employee.UpdatedAt = DateTime.Now;
                employee.Save();
                ResetDatagrid();
                ResetTextBoxes();
            }
            else MessageBox.Show("To update a set of data, you have to select one item!");
        }

        private void DeleteEmployeeButtonCT_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            var selectedItems = employeeTable.SelectedItems;
            List<Employee> toDeleteList = new List<Employee>();
            Employee toDeleteTemp;
            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    if (!item.Equals(CollectionView.NewItemPlaceholder))
                    {
                        toDeleteTemp = (Employee)item;
                        toDeleteList.Add(toDeleteTemp);
                        msg += $"{toDeleteTemp.ID}\n";
                    }

                }
                if (!msg.Equals(string.Empty))
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to delete the following employees?\nIDs:\n" + msg,
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

        private void employeeTable_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            if (employeeTable.SelectedItem != null)
            {
                var employee = (Employee)employeeTable.SelectedItem;
                idTB.Text = $"{employee.ID}";
                firstNameTB.Text = $"{employee.FirstName}";
                lastNameTB.Text = $"{employee.LastName}";
                acronymTB.Text = $"{employee.Acronym}";
                departmentTB.Text = $"{employee.Department}";

            }
        }
    }
}
