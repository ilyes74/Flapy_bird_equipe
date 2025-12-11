using SpeedyWings;
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

                // Au démarrage, on affiche le menu

                AfficherDemarrage();

            }

            // --- Méthodes de Navigation ---

            public void AfficherDemarrage()

            {

                // On charge l'UserControl UCDemarrage

                ContenuPrincipal.Content = new UCDemarrage(this);

            }

            public void AfficherJeu()

            {

                ContenuPrincipal.Content = new UCjeu(this);

            }

            public void AfficherRegles()

            {

                ContenuPrincipal.Content = new UCReglesJeu(this);

            }

            public void AfficherSkins()

            {

                // ContenuPrincipal.Content = new UCSkins(this); (A faire plus tard)

            }
        public void 
        }
    }
