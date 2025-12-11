using Flapy_bird;
using System.Windows;

using System.Windows.Controls;

namespace SpeedyWings

{

    public partial class UCReglesJeu : UserControl

    {

        private MainWindow mainWin;

        public UCReglesJeu(MainWindow w)

        {

            InitializeComponent();

            this.mainWin = w;

        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)

        {

            mainWin.AfficherDemarrage();

        }

    }

}