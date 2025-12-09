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
        public MainWindow()
        {
            InitializeComponent();
            AfficherDemarrage();
            //AfficherReglesJeux();
        }

        private void AfficherDemarrage()
        {
            UCDemarage uc = new UCDemarage(); // crée et charge l'écran de démarrage
            ZoneDemarrage.Content = uc; // associe l'écran au conteneur 
            uc.btnJouer.Click += AfficherReglesJeux; //
        }

        private void AfficherReglesJeux(object sender, RoutedEventArgs e)
        {
            UCReglesJeu uc = new UCReglesJeu();
            ZoneDemarrage.Content = uc;
        }
    }
}