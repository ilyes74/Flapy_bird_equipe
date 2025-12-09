using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
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
using System.Windows.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Flapy_bird
{

    /// <summary>
    /// Logique d'interaction pour UCjeu.xaml
    /// </summary>
    public partial class UCjeu : UserControl
    {
        // On reprend tes variables statiques et la structure exacte
        private DispatcherTimer minuterie;

        // --- Variables pour la physique (indispensables pour Flappy Bird) ---
        private double vitesseVerticale = 0;
        private double gravite = 0.6;   // Force qui tire vers le bas
        private double forceSaut = -10; // Force vers le haut
        private int vitesseDecor = 5;   // Vitesse de défilement vers la gauche

        public UCjeu()
        {
            InitializeComponent();
            InitTimer();
        }

        // Comme dans ton code Père Noël : On attache les touches quand le contrôle est chargé
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Cette méthode permet de capter les touches même si on n'a pas cliqué sur le jeu
            Application.Current.MainWindow.KeyDown += canvasJeu_KeyDown;
        }

        // Gestion des touches (copié collé adapté de ton code)
        private void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                // Si le timer est arrêté (perdu ou pause), on relance
                if (!minuterie.IsEnabled)
                {
                    // Reset simple pour recommencer si on a perdu
                    Canvas.SetTop(imgflapy, 200);
                    vitesseVerticale = 0;
                    minuterie.Start();
                }
                else
                {
                    // ACTION DE SAUT (remplace ton "gauche/droite")
                    Sauter();
                }
            }
        }

        // Nouvelle méthode "Sauter" (équivalent de tes méthodes gauche/droite)
        private void Sauter()
        {
            vitesseVerticale = forceSaut;
        }

        private void InitTimer()
        {
            minuterie = new DispatcherTimer();
            minuterie.Interval = TimeSpan.FromMilliseconds(16);
            minuterie.Tick += Jeu;
            minuterie.Start();
        }

        // Le coeur du jeu (Comme ta méthode "Jeu" du Père Noël)
        private void Jeu(object? sender, EventArgs e)
        {
            // 1. GESTION DE L'OISEAU (Gravité)
            // C'est la même logique que ton cadeau qui tombe, mais avec accélération
            vitesseVerticale += gravite;

            double yFlapy = Canvas.GetTop(imgflapy);
            double nvFlapy = yFlapy + vitesseVerticale;
            Canvas.SetTop(imgflapy, nvFlapy);

            // 2. GESTION DES DÉCORS (Tuyaux qui avancent vers la gauche)
            // J'appelle une méthode pour éviter de copier-coller 4 fois le code
            BougeTuyau(imgtuyau1);
            BougeTuyau(imgtuyau2);
            BougeTuyau(imgtuyau3);
            BougeTuyau(imgtuyau4);

            BougeTuyau(imgpiece1); // Si tu veux bouger la pièce aussi

            // On peut aussi bouger le sol
            BougeTuyau(imgsol);
            BougeTuyau(imgsol2);

            // 3. COLLISIONS (Vérifier si on touche)
            VerifCollision();
        }

        // Cette méthode remplace ta logique "if (nvpernoel >= 0)..."
        // Elle sert à déplacer n'importe quelle image vers la gauche et la remettre à droite
        private void BougeTuyau(Image image)
        {
            double xImage = Canvas.GetLeft(image);
            double nvImage = xImage - vitesseDecor; // On va vers la gauche (-)

            Canvas.SetLeft(image, nvImage);

            // Si l'image sort complètement à gauche, on la remet à droite
            if (Canvas.GetLeft(image) + image.Width <= 0)
            {
                Canvas.SetLeft(image, 800); // 800 est la largeur de ton écran
            }
        }

        private void VerifCollision()
        {
            // Création des rectangles (Comme dans ton constructeur Père Noël)
            Rect rFlapy = new Rect(Canvas.GetLeft(imgflapy), Canvas.GetTop(imgflapy), imgflapy.Width, imgflapy.Height);

            // Vérification sol/plafond
            if (Canvas.GetTop(imgflapy) > 380 || Canvas.GetTop(imgflapy) < -50)
            {
                minuterie.Stop();
            }

            // Vérification tuyaux (exemple avec 2 tuyaux, tu peux ajouter les autres)
            if (Touche(rFlapy, imgtuyau1) || Touche(rFlapy, imgtuyau2) || Touche(rFlapy, imgtuyau3) || Touche(rFlapy, imgtuyau4))
            {
                minuterie.Stop();
            }
        }

        // Petite fonction d'aide pour la collision (utilise IntersectsWith vu en cours normalement)
        private bool Touche(Rect r1, Image cible)
        {
            Rect r2 = new Rect(Canvas.GetLeft(cible), Canvas.GetTop(cible), cible.ActualWidth, cible.ActualHeight);
            return r1.IntersectsWith(r2);
        }
    }
}

