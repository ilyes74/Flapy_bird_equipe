using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Flapy_bird
{
    
    public partial class UcSkin : UserControl
    {
        private MainWindow fenetrePrincipale;

        public UcSkin(MainWindow w)
        {
            InitializeComponent();
            this.fenetrePrincipale = window;
        }

        private void BtnSkin1_Click(object sender, RoutedEventArgs e)
        {
            // CORRECTION : On utilise l'instance 'fenetrePrincipale', pas la classe 'MainWindow'
            fenetrePrincipale.SkinActuel = "/Images/FlappyBird.png";
            MessageBox.Show("Skin Classique sélectionné !");
        }

        private void BtnSkin2_Click(object sender, RoutedEventArgs e)
        {
            // CORRECTION ICI AUSSI
            fenetrePrincipale.SkinActuel = "/Images/oiseauRouge.png"; 
            MessageBox.Show("Skin Rouge sélectionné !");
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
           
              fenetrePrincipale.AfficherDemarrage();
        }
    }
}