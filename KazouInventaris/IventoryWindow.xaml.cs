using KazouInventaris.Classes;
using KazouInventaris.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for IventoryWindow.xaml
    /// </summary>
    public partial class IventoryWindow : Window
    {
        private PersistenceCode PC = new PersistenceCode();
        private inventoryCategory selectedCategory;
        public IventoryWindow()
        {
            InitializeComponent();
            //prepares the combobox
            List<inventoryCategory> categories = PC.LoadCategories();
            foreach (inventoryCategory category in categories)
            {
                categoriesComboBox.Items.Add(category.name);

            }
            categoriesComboBox.SelectedItem = categoriesComboBox.Items[0];
            selectedCategory = categories[0];
            //inital load of items
            loadEverything();
        }
        private void loadEverything()
        {
            inventoryListBox.Items.Clear();
            List<inventoryItem> items = PC.LoadItems();
            foreach (inventoryItem item in items)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Content = item.name;
                lvi.Tag = item.id;
                inventoryListBox.Items.Add(lvi);
            }
        }
        private void loadBasedOnCategory(int id) {
            inventoryListBox.Items.Clear();
            List<inventoryItem> items = PC.LoadCategoryItems(id);
            foreach (inventoryItem item in items)
            {
                if (item.category == id)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Content = item.name;
                    lvi.Tag = item.id;
                    inventoryListBox.Items.Add(lvi);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = PC.LoadCategory(categoriesComboBox.SelectedIndex);
            if (selectedCategory.id == 0)
            {
                loadEverything();
            }
            else
            {
                loadBasedOnCategory(categoriesComboBox.SelectedIndex);
            }
        }

 

        private void inventoryListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            int ID = Convert.ToInt32(((ListViewItem)inventoryListBox.SelectedItem).Tag);
            inventoryItem selectedItem = PC.LoadItem(ID);
            if (selectedItem != null)
            {
                DetailWindow detailWindow = new DetailWindow(selectedItem, this);
                detailWindow.ShowDialog();
            }
        }

        public void refreshListbox()
        {
           inventoryListBox.Items.Clear();
           loadEverything();

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addItemWindow addItemWindow = new addItemWindow(this);
            addItemWindow.ShowDialog();
        }
    }
}
