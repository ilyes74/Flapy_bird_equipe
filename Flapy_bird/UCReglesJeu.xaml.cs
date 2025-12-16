using Flapy_bird;
using System.Windows;

using System.Windows.Controls;

namespace SpeedyWings

{

    public partial class UCReglesJeu : UserControl

    {

        private MainWindow mainWindow;

        public UCReglesJeu(MainWindow window)

        {

            InitializeComponent();

            this.mainWindow = window;

        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)

        {

            mainWindow.AfficherDemarrage();

        }
    }
}