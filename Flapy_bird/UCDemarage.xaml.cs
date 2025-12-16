using Flapy_bird;
using System.Media;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer(@"C:\Users\paolo\Documents\BUT INFO\DEVELOPPEMENT\SAE\Flapy_bird\Sons\The Swiss - Bubble Bath.wav");
            player.Load();
            player.Play();
        }
    }

}