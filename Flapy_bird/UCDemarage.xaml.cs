using Flapy_bird;
using System.Windows;

using System.Windows.Controls;

namespace SpeedyWings

{

    public partial class UCDemarrage : UserControl

    {

        private MainWindow mainWindow;

  

        public UCDemarrage(MainWindow w)

        {

            InitializeComponent();

            this.mainWindow = w;

        }

        private void BtnJouer_Click(object sender, RoutedEventArgs e)

        {

            mainWindow.AfficherJeu();

        }

        private void BtnRegles_Click(object sender, RoutedEventArgs e)

        {

            mainWindow.AfficherRegles();

        }

        private void BtnQuitter_Click(object sender, RoutedEventArgs e)

        {

            Application.Current.Shutdown();

        }

     
        private void BtnSkins_Click(object sender, RoutedEventArgs e)
        {
            // On appelle la méthode de navigation de MainWindow
            fenetrePrincipale.AfficherSkins();
        }
    }

}