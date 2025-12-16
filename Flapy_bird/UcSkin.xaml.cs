using System.Windows;
using System.Windows.Controls;

namespace Flapy_bird
{
    public partial class UcSkin : UserControl
    {
        // On crée une variable pour stocker la référence à la fenêtre principale
        private MainWindow fenetre;

        // Le constructeur reçoit la fenêtre principale (w)
        public UcSkin(MainWindow w)
        {
            InitializeComponent();
            this.fenetre = w;
        }

        // --- 1. CLIC SUR L'OISEAU CLASSIQUE ---
        private void BtnSkin1_Click(object sender, RoutedEventArgs e)
        {
            // On définit le skin
            MainWindow.SkinActuel = "/Images/FlappyBird.png";
            // On lance le jeu
            fenetre.AfficherJeu();
        }

        // --- 2. CLIC SUR L'OISEAU ROUGE ---
        private void BtnSkin2_Click(object sender, RoutedEventArgs e)
        {
            // On définit le skin (vérifie bien le nom de ton image !)
            MainWindow.SkinActuel = "/Images/oiseauRouge2.png";
            // On lance le jeu
            fenetre.AfficherJeu();
        }

        // --- 3. CLIC SUR LE BOUTON RETOUR ---
        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            // On revient au menu de démarrage
            fenetre.AfficherDemarrage();
        }
    }
}