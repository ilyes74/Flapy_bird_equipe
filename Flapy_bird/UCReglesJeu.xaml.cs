using Flapy_bird;
using System.Windows;

using System.Windows.Controls;

namespace SpeedyWings

{

    public partial class UCReglesJeu : UserControl

    {

        private MainWindow mainWindow;

        public UCReglesJeu(MainWindow w)

        {

            InitializeComponent();

            this.mainWindow = w;

        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)

        {

            mainWindow.AfficherDemarrage();

        }

    }

}