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
    /// Interaction logic for uitgeleendItem.xaml
    /// </summary>
    public partial class uitgeleendItem : Window
    {
        private BorrowedItems item;
        private uitgeleendeItems window;
        public uitgeleendItem(BorrowedItems item, uitgeleendeItems window)
        {
            this.window = window;
            InitializeComponent();
            this.item = item;
            aantalTextBox.Text = item.amount.ToString();
            datumTextBox.Text = item.Date;
            nameTextBox.Text = item.Name;
            vakantieCodeTextbox.Text = item.vacantieCode;
            PersistenceCode persistenceCode = new PersistenceCode();
            
            uitgeleendItemTextBox.Text = persistenceCode.LoadItem(item.uitgeleendItemID).name;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            PersistenceCode persistenceCode = new PersistenceCode();
            persistenceCode.returnItem(item.ID);
            if (MessageBox.Show("Item is teruggebracht, alles is ok", "Item is teruggebracht", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                window.Load();
                this.Close();
            }
        }

        private void notOkButton_Click(object sender, RoutedEventArgs e)
        {
           if( MessageBox.Show("Er is een probleem met dit item, contacteer een beroepskracht", "Item is niet teruggebracht", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
            {
                window.Load();
                this.Close();
            }
        }
    }
}
