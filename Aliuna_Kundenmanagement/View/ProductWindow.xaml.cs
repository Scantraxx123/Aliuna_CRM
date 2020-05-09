using System.Text.RegularExpressions;
using System.Windows;
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
        }

        private void createProductBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateProductBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clearProductFieldsBT_Click(object sender, RoutedEventArgs e)
        {

        }
        private void deleteProductsBT_Click(object sender, RoutedEventArgs e)
        {

        }

        private void productsTable_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {

        }

        private void DoubleValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^-?[0-9][0-9,\.]+$");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void IntegerValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+$");
            e.Handled = regex.IsMatch(e.Text);
        }


    }
}
