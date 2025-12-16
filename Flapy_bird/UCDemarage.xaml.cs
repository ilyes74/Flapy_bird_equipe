using Flapy_bird;
using System.Windows;

using System.Windows.Controls;

namespace SpeedyWings

{

    public partial class UCDemarrage : UserControl

    {

        private MainWindow mainWindow;

  

        public UCDemarrage(MainWindow window)

        {

            InitializeComponent();

            this.mainWindow = window;

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
         
            mainWindow.AfficherSkin();
        }
    }

}