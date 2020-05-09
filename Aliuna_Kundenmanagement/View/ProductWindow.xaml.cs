using Aliuna.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Aliuna.View
{
    /// <summary>
    /// Interaktionslogik für ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            if (App.isDBConOpen) ResetDatagrid();
        }
        private void ResetDatagrid()
        {
            productsTable.ItemsSource = BaseModel<Product>.GetAll();
        }

        private void ResetTextBoxes()
        {
            idTB.Text = string.Empty;
            nameTB.Text = string.Empty;
            priceTB.Text = string.Empty;
            inStockTB.Text = string.Empty;
            reservedTB.Text = string.Empty;
            soldTB.Text = string.Empty;
        }

        private void createProductBT_Click(object sender, RoutedEventArgs e)
        {
            if (!nameTB.Text.Equals(string.Empty) && !priceTB.Text.Equals(string.Empty) && !inStockTB.Text.Equals(string.Empty))
            {
                if (App.isDBConOpen)
                {
                    var price = Convert.ToDouble(priceTB.Text);
                    var inStock = Convert.ToInt32(inStockTB.Text);

                    if (inStock.Equals(0))
                    {
                        MessageBoxResult result = MessageBox.Show("You have no Products in stock, are you sure? This can have a consequences later on, but you can change it.",
                                              "Confirmation",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
                        if (result == MessageBoxResult.No) return;
                    }
                    new Product
                    {
                        Name = nameTB.Text,
                        Price = price,
                        InStock = inStock,
                        Reserved = 0,
                        Sold = 0
                    }.Save();

                    ResetDatagrid();
                    ResetTextBoxes();
                }
                else MessageBox.Show("Please establish a database connection!");
            }
            else MessageBox.Show("Please fill out the complete form!");
        }

        private void updateProductBT_Click(object sender, RoutedEventArgs e)
        {
            if (productsTable.SelectedItem != null)
            {
                var product = (Product)productsTable.SelectedItem;
                product.Name = nameTB.Text;
                product.Price = Convert.ToDouble(priceTB.Text);
                product.InStock = Convert.ToInt32(inStockTB.Text);
                product.Reserved = Convert.ToInt32(reservedTB.Text);
                product.Sold = Convert.ToInt32(soldTB.Text);
                product.UpdatedAt = DateTime.Now;
                product.Save();
                ResetDatagrid();
                ResetTextBoxes();
            }
            else MessageBox.Show("To update a set of data, you have to select one item!");
        }

        private void clearProductFieldsBT_Click(object sender, RoutedEventArgs e)
        {
            productsTable.SelectedIndex = -1;
            ResetTextBoxes();
        }
        private void deleteProductsBT_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            var selectedItems = productsTable.SelectedItems;
            List<Product> toDeleteList = new List<Product>();
            Product toDeleteTemp;
            if (selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    if (!item.Equals(CollectionView.NewItemPlaceholder))
                    {
                        toDeleteTemp = (Product)item;
                        toDeleteList.Add(toDeleteTemp);
                        msg += $"{toDeleteTemp.ID}\n";
                    }

                }
                if (!msg.Equals(string.Empty))
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to delete the following products?\nIDs:\n" + msg,
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

        private void productsTable_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            if (productsTable.SelectedItem != null)
            {
                var product = (Product)productsTable.SelectedItem;
                idTB.Text = $"{product.ID}";
                nameTB.Text = $"{product.Name}";
                priceTB.Text = $"{product.Price}";
                inStockTB.Text = $"{product.InStock}";
                reservedTB.Text = $"{product.Reserved}";
                soldTB.Text = $"{product.Sold}";

            }
        }


        #region Need to be worked on
        private void DoubleValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            double val;
            // If parsing is successful, set Handled to false
            e.Handled = !double.TryParse(fullText, out val);
        }
        private void IntegerValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            int val;
            // If parsing is successful, set Handled to false
            e.Handled = !int.TryParse(fullText, out val);
        }
        #endregion
    }
}
