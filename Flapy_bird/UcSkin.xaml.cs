using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Logique d'interaction pour UcSkin.xaml
    /// </summary>
    public partial class UcSkin : UserControl
    {
        MainWindow fenetrePrincipale;

        public UcSkin(MainWindow window)
        {
            InitializeComponent();
            this.fenetrePrincipale = window;
        }

        private void BtnSkin1_Click(object sender, RoutedEventArgs e)
        {
           
            MainWindow.SkinActuel = "/Images/FlappyBird.png";
            MessageBox.Show("Skin Classique sélectionné !");
        }

        private void BtnSkin2_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow.SkinActuel = "/Images/oiseauRouge.png"; 
            MessageBox.Show("Skin Rouge sélectionné !");
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
           
            fenetrePrincipale.AfficherDemarrage();
        }
    }
}
