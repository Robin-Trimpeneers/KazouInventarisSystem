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
    /// Interaction logic for itemUitlenenWindow.xaml
    /// </summary>
    public partial class itemUitlenenWindow : Window
    {
        private inventoryItem _geleendItem;
        private PersistenceCode PersistenceCode;
        public itemUitlenenWindow(inventoryItem geleendItem)
        {
            InitializeComponent();
            _geleendItem = geleendItem;
            this.Title = _geleendItem.name;
            uitgeleendItemTextBox.Text = _geleendItem.name;
            PersistenceCode = new PersistenceCode();
            stockTextBox.Text = _geleendItem.amount.ToString();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            PersistenceCode.BorrowItem(_geleendItem, naamTextBox.Text,vakantieCodeTextBox.Text,Convert.ToInt32(aantalTextBox.Text));
            if (MessageBox.Show("Item is uitgeleend",$"{_geleendItem.name} is uitgeleend",MessageBoxButton.OK,MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                this.Close();
            }
        }
    }
}
