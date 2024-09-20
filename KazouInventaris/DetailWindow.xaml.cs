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
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private inventoryItem _selectedItem;
        private PersistenceCode PC = new PersistenceCode();
        private IventoryWindow _previousWindow;
        public DetailWindow(inventoryItem selectedItem, IventoryWindow previousWindow)
        {
            InitializeComponent();
            _previousWindow = previousWindow;
            _selectedItem = selectedItem;
            inventoryCategory category = new inventoryCategory();
            category = PC.LoadCategory(_selectedItem.category);
            this.Title = _selectedItem.name;
            categoryTextBox.Text = category.name;
            stockTextBox.Text = _selectedItem.amount.ToString();
            descriptionTextBox.Text = _selectedItem.description;
            itemnameTextBox.Text = _selectedItem.name;
            purchaseAmountTextBox.Text = _selectedItem.purchaseAmount.ToString();
            locationTextBox.Text = _selectedItem.location;
            


        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            categoryTextBox.IsEnabled = true;
            stockTextBox.IsEnabled = true;
            descriptionTextBox.IsEnabled = true;
            itemnameTextBox.IsEnabled = true;
            purchaseAmountTextBox.IsEnabled = true;
            locationTextBox.IsEnabled = true;
            categoryTextBox.IsEnabled = true;

            



        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

            _selectedItem.name = itemnameTextBox.Text;
            _selectedItem.amount = Convert.ToInt32(stockTextBox.Text);
            _selectedItem.description = descriptionTextBox.Text;
            _selectedItem.purchaseAmount = Convert.ToInt32(purchaseAmountTextBox.Text);
            _selectedItem.location = locationTextBox.Text;
            _selectedItem.category = PC.GetCategoryId(categoryTextBox.Text);
            try
            {
                PC.EditItem(_selectedItem);
                this.Close();
            }
            catch
            {
                if (MessageBox.Show("Er is iets mis gegaan, contacteer de beroepskracht", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    this.Close();
                }

            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Ben je zeker dat je dit item wilt verwijderen?", "Verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                PC.deleteItem(_selectedItem.id);
                _previousWindow.refreshListbox();
                this.Close();
            }
         
        }
    }
}
