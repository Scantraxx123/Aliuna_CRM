using Aliuna.Model;
using Aliuna.Model.Subclasses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;

namespace Aliuna.View
{
    /// <summary>
    /// Interaktionslogik für OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private Customer customer;
        public OrderWindow(Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            SetTextBoxes();
            SetEmployeesCB();
            ResetDatagrid();
        }

        private void ResetDatagrid()
        {
            this.customer = Customer.GetOne(x => x.Id.Equals(this.customer.Id));
            notesTable.ItemsSource = customer.Notes;
            orderTable.ItemsSource = customer.Orders;
        }

        private void SetEmployeesCB()
        {
            var employees = BaseModel<Employee>.GetAll();
            foreach (var item in employees)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{item.LastName}, {item.FirstName}";
                temp.Tag = item;
                employeeCB.Items.Add(temp);
            }
        }
        private void SetTextBoxes()
        {
            idTB.Text = $"{this.customer.Id}";
            companyTB.Text = $"{this.customer.CompanyName}";
            fnTB.Text = $"{this.customer.FirstName}";
            lnTB.Text = $"{this.customer.LastName}";
            emailTB.Text = $"{this.customer.Email}";
            streetTB.Text = $"{this.customer.Street}";
            hnTB.Text = $"{this.customer.Housenumber}";
            pcTB.Text = $"{this.customer.Postcode}";
            cityTB.Text = $"{this.customer.City}";
            countryTB.Text = $"{this.customer.Country}";
            phoneTB.Text = $"{this.customer.PhoneNumber}";
            faxTB.Text = $"{this.customer.FaxNumber}";
        }

        private void Clear()
        {
            orderTable.SelectedIndex = -1;
            productTable.SelectedIndex = -1;
            notesTable.SelectedIndex = -1;
            employeeCB.SelectedIndex = -1;
            orderNumberLabel.Text = string.Empty;
        }

        private void UpdateCustomerBT_Click(object sender, RoutedEventArgs e)
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
                }
            }
        }

        private void NewOrderBT_Click(object sender, RoutedEventArgs e)
        {
            if (employeeCB.SelectedItem != null)
            {
                var employee = (Employee)((ComboBoxItem)employeeCB.SelectedItem).Tag;
                var order = new Order
                {
                    Employee = employee,
                    Customer = this.customer
                };
                this.customer.Notes.Add(new Note($"New Order was created with ID: {order.Id}"));
                this.customer.Orders.Add(order);
                order.Save();
                this.customer.Save();
                ResetDatagrid();
                Clear();
            }
        }

        private void ClearBT_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void UpdateOrderBT_Click(object sender, RoutedEventArgs e)
        {
            if (orderTable.SelectedItem != null)
            {
                var order = (Order)orderTable.SelectedItem;
                order.UpdatedAt = DateTime.Now;
                order.Employee = (Employee)((ComboBoxItem)employeeCB.SelectedItem).Tag;
                this.customer.Notes.Add(new Note($"Order was updated with ID: {order.Id}"));
                this.customer.Save();
                order.Save();
                Clear();
            } 
        }
    }
}
