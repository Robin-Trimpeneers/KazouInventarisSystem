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
    /// Interaction logic for uitgeleendeItems.xaml
    /// </summary>
    public partial class uitgeleendeItems : Window
    {
        private PersistenceCode pc;

        public uitgeleendeItems()
        {
            InitializeComponent();
            pc = new PersistenceCode();
            Load();
            
        }
        public void Load()
        {
            geleendeitemsListBox.Items.Clear();
            List<BorrowedItems> borrowedItems = pc.LoadBorrowedItems();
            foreach (BorrowedItems borrowedItem in borrowedItems)
            {
                inventoryItem inventoryItem = pc.LoadItem(borrowedItem.uitgeleendItemID);
                ListViewItem item = new ListViewItem();
                string status = "";
                if (borrowedItem.returned == 0)
                {
                    status = "Niet teruggebracht";
                }
                else
                {
                    status = "Teruggebracht";
                }
                item.Content += $"{borrowedItem.Name} heeft {borrowedItem.amount} {inventoryItem.name} uitgeleend op {borrowedItem.Date} Status: {status}";
                geleendeitemsListBox.Items.Add(item);
            }
        }

   

        private void geleendeitemsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BorrowedItems item= pc.LoadBorrowedItem(geleendeitemsListBox.SelectedIndex +1 );
            uitgeleendItem uitgeleendItem = new uitgeleendItem(item, this);
            uitgeleendItem.ShowDialog();
        }
    }
}
