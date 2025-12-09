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

namespace Flapy_bird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Perso { get; set; }
        public static int Pasflopy { get; set; } = 2;
        public static int Paspiece { get; set; } = 5;

        public MainWindow()
        {
            InitializeComponent();
            this.Content = new UCjeu();
        }
    }
}