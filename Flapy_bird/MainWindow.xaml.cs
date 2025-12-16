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
    
    public partial class MainWindow : Window

    {

        public static string SkinActuel = "/Images/FlappyBird.png";


        public MainWindow()

        {

            InitializeComponent();
            AfficherDemarrage();

        }

        

        public void AfficherDemarrage()

        {

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

        public void AfficherSkin()
        {
            ContenuPrincipal.Content = new UcSkin(this);
        }
        public string SkinActuel = "/Images/FlappyBird.png";
    }
    }
