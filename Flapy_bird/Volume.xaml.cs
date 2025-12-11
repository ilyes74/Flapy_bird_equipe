using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Flapy_bird
{
    public partial class VolumeSettings : UserControl
    {
        // Supposons que votre lecteur audio est statique pour un accès facile
        // REMPLACER ceci par l'accès à VOTRE objet MediaPlayer réel
        public static MediaPlayer GameAudioPlayer { get; set; }

        public VolumeSettings()
        {
            InitializeComponent();

            // Initialiser le slider avec le volume actuel au chargement
            if (GameAudioPlayer != null)
            {
                VolumeSlider.Value = GameAudioPlayer.Volume;
            }
        }

        private void Volume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Vérifie que l'objet Player est disponible
            if (GameAudioPlayer != null)
            {
                // La propriété Volume d'un MediaPlayer va de 0.0 (muet) à 1.0 (max)
                GameAudioPlayer.Volume = e.NewValue;
            }
        }
    }
}