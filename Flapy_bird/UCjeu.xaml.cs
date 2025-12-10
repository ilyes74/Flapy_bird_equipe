using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Flapy_bird
{
    public partial class UCjeu : UserControl
    {
        private DispatcherTimer minuterie;

        // --- Variables Physique ---
        private double vitesseVerticale = 0;
        private double gravite = 0.5;   // Force qui tire vers le bas
        private double forceSaut = -8;  // Force du saut (négatif car on va vers le haut du Canvas)
        private int vitesseDecor = 5;   // Vitesse de défilement des tuyaux

        public UCjeu()
        {
            InitializeComponent();
            InitTimer();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Important : On met le focus pour que le clavier marche tout de suite
            this.Focus();

            // On s'abonne aux touches de la fenêtre principale
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.KeyDown += canvasJeu_KeyDown;
            }
        }

        // --- 1. GESTION DES TOUCHES (CORRIGÉE) ---
        private void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            // Dans Flappy Bird, Espace ou Haut = SAUTER
            if (e.Key == Key.Space || e.Key == Key.Up)
            {
                // Si le timer est arrêté (pause ou début), on le lance
                if (!minuterie.IsEnabled)
                {
                    minuterie.Start();
                }

                Sauter();
            }
        }

        private void Sauter()
        {
            // On inverse la gravité pour monter d'un coup
            vitesseVerticale = forceSaut;

            // Si tu as une image pour le saut, tu peux la mettre ici
            // imgflapy.Source = flapyhaut; 
        }

        private void InitTimer()
        {
            minuterie = new DispatcherTimer();
            minuterie.Interval = TimeSpan.FromMilliseconds(20); // ~50 images/seconde
            minuterie.Tick += Jeu;
            // On ne lance pas le timer tout de suite, on attend le premier saut
        }

        // --- 2. BOUCLE PRINCIPALE DU JEU ---
        private void Jeu(object? sender, EventArgs e)
        {
            // A. Gravité de l'oiseau (Ta logique)
            vitesseVerticale += gravite;
            double yFlapy = Canvas.GetTop(imgflapy);

            // Sécurité si position inconnue
            if (double.IsNaN(yFlapy)) yFlapy = 200;

            Canvas.SetTop(imgflapy, yFlapy + vitesseVerticale);

            // B. Faire avancer les tuyaux (NOUVEAU - Indispensable)
            BougeDecor(imgtuyau1);
            BougeDecor(imgtuyau2);
            BougeDecor(imgtuyau3);
            BougeDecor(imgtuyau4);

            // Si tu as un sol qui bouge :
            // BougeDecor(imgsol); 

            // C. Vérifier si on a perdu
            VerifCollision();
        }

        // --- 3. DÉPLACEMENT DU DÉCOR (NOUVEAU) ---
        private void BougeDecor(Image image)
        {
            double x = Canvas.GetLeft(image);

            // Sécurité anti-bug (si pas défini dans le XAML)
            if (double.IsNaN(x)) x = 800;

            // On déplace vers la GAUCHE
            x -= vitesseDecor;

            // Si le tuyau sort à gauche de l'écran
            if (x < -image.ActualWidth)
            {
                // On le remet tout à droite
                x = 800;
            }

            Canvas.SetLeft(image, x);
        }

        // --- 4. COLLISIONS (Ton code gardé tel quel) ---
        private void VerifCollision()
        {
            // Création du rectangle oiseau
            Rect rFlapy = new Rect(Canvas.GetLeft(imgflapy), Canvas.GetTop(imgflapy), imgflapy.Width - 10, imgflapy.Height - 10);

            // Vérification sol/plafond
            if (Canvas.GetTop(imgflapy) > 380 || Canvas.GetTop(imgflapy) < -50)
            {
                minuterie.Stop(); // Game Over
            }

            // Vérification tuyaux
            if (Touche(rFlapy, imgtuyau1) || Touche(rFlapy, imgtuyau2) || Touche(rFlapy, imgtuyau3) || Touche(rFlapy, imgtuyau4))
            {
                minuterie.Stop(); // Game Over
            }
        }

        private bool Touche(Rect r1, Image cible)
        {
            // Sécurité pour récupérer les positions
            double x = Canvas.GetLeft(cible);
            double y = Canvas.GetTop(cible);
            if (double.IsNaN(x)) x = 0;
            if (double.IsNaN(y)) y = 0;

            Rect r2 = new Rect(x, y, cible.ActualWidth, cible.ActualHeight);
            return r1.IntersectsWith(r2);
        }
    }
}