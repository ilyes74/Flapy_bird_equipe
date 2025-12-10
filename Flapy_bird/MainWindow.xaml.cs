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
            AfficherJeu();
            //AfficherReglesJeux();
        }

        private void AfficherJeu()
        {
            UCDemarage uc = new UCDemarage(); // crée et charge l'écran de démarrage
            ZoneDemarrage.Content = uc; // associe l'écran au conteneur 
            uc.btnJouer.Click += AfficherUCJeu; //
        }

        private void AfficherUCJeu(object sender, RoutedEventArgs e)
        {
            UCjeu uc = new UCjeu();
            ZoneDemarrage.Content = uc;
        }
    }
}