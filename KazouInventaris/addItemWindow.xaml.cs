using KazouInventaris.Classes;
using KazouInventaris.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KazouInventaris
{
    /// <summary>
    /// Interaction logic for addItemWindow.xaml
    /// </summary>
    public partial class addItemWindow : Window
    {
        private PersistenceCode pc;
        private IventoryWindow _IventoryWindow;
        public addItemWindow(IventoryWindow window)
        {
            InitializeComponent();
            pc = new PersistenceCode();
            _IventoryWindow = window;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            inventoryItem item = new inventoryItem();
            item.name = itemnameTextBox.Text;
            item.description = descriptionTextBox.Text;
            item.location = locationTextBox.Text;
            item.amount = Convert.ToInt32(stockTextBox.Text);
            item.purchaseAmount = Convert.ToInt32(purchaseAmountTextBox.Text);
            item.category = Convert.ToInt32(categoryTextBox.Text);
            pc.createItem(item);
            if (MessageBox.Show("Item is toegevoegd", $"{item.name} is toegevoegd", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                _IventoryWindow.refreshListbox();
                this.Close();
            }
        }
    }
}
