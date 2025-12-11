using Flapy_bird;
using System.Windows;

using System.Windows.Controls;

namespace SpeedyWings

{

    public partial class UCDemarrage : UserControl

    {

        private MainWindow mainWin;

        // Constructeur qui reçoit la fenêtre principale

        public UCDemarrage(MainWindow w)

        {

            InitializeComponent();

            this.mainWin = w;

        }

        private void BtnJouer_Click(object sender, RoutedEventArgs e)

        {

            mainWin.AfficherJeu();

        }

        private void BtnRegles_Click(object sender, RoutedEventArgs e)

        {

            mainWin.AfficherRegles();

        }

        private void BtnQuitter_Click(object sender, RoutedEventArgs e)

        {

            Application.Current.Shutdown();

        }

    }

}