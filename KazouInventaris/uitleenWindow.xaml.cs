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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace KazouInventaris
{
    /// <summary>
    /// Interaction logic for uitleenWindow.xaml
    /// </summary>
    public partial class uitleenWindow : Window
    {
        private PersistenceCode pc;
        public uitleenWindow()
        {
            InitializeComponent();
            PersistenceCode pc = new PersistenceCode();
            this.pc = pc;
            itemTextBox.Focus();
        }

        private void loadItem(string item)
        {
            itemTextBox.Text = "";
            int itemID = Convert.ToInt32(item);
            inventoryItem inventoryItem = pc.LoadItem(itemID);
            itemUitlenenWindow itemUitlenenWindow = new itemUitlenenWindow(inventoryItem);
            itemUitlenenWindow.ShowDialog();
        }

        private void itemTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string item = itemTextBox.Text;
                loadItem(item);
            }
        }
    }
}
