using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KazouInventaris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void uitleenButton_Click(object sender, RoutedEventArgs e)
        {
            uitleenWindow uitleenWindow = new uitleenWindow();
            uitleenWindow.ShowDialog();
        }

        private void bekijkInventoryButton_Click(object sender, RoutedEventArgs e)
        {
            IventoryWindow iventoruWindow = new IventoryWindow();
            iventoruWindow.ShowDialog();
        }

        private void pasUitleningenAan_Click(object sender, RoutedEventArgs e)
        {
            uitgeleendeItems uitgeleendeItems = new uitgeleendeItems();
            uitgeleendeItems.ShowDialog();

        }
    }
}