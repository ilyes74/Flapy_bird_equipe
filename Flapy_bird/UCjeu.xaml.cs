using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Flapy_bird
{
    public partial class UCjeu : UserControl
    {
        // 1. RÉFÉRENCE À LA FENÊTRE PRINCIPALE
        // C'est ça qui permet de revenir au menu ou de relancer le jeu
        private MainWindow fenetrePrincipale;

        // 2. VARIABLES DU JEU
        private DispatcherTimer minuterie;

        private int pasSol = 5;      // Vitesse des tuyaux/sol
        private int pasFond = 2;     // Vitesse du ciel/bâtiments

        private double vitesseVerticale = 0;
        private double gravite = 0.5;
        private double forceSaut = -8;

        // 3. CONSTRUCTEUR MODIFIÉ
        // On demande "Qui est la fenêtre principale ?" (MainWindow w)
        public UCjeu(MainWindow w)
        {
            InitializeComponent();

            this.fenetrePrincipale = w; // On sauvegarde la référence

            InitializeTimer();
        }

        // --- INITIALISATION ---
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus(); // Capture le clavier

            // On s'abonne aux touches de la fenêtre parente
            if (fenetrePrincipale != null)
            {
                fenetrePrincipale.KeyDown += Window_KeyDown;
            }
        }

        private void InitializeTimer()
        {
            minuterie = new DispatcherTimer();
            minuterie.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            minuterie.Tick += BoucleJeu;
            minuterie.Start();
        }

        // --- BOUCLE PRINCIPALE (MOTEUR DU JEU) ---
        private void BoucleJeu(object? sender, EventArgs e)
        {
            // A. PHYSIQUE (GRAVITÉ)
            vitesseVerticale += gravite;

            double y = Canvas.GetTop(imgflapy);
            if (double.IsNaN(y)) y = 200; // Sécurité

            Canvas.SetTop(imgflapy, y + vitesseVerticale);

            // Rotation de l'oiseau
            imgflapy.RenderTransform = new RotateTransform(vitesseVerticale * 3);
            imgflapy.RenderTransformOrigin = new Point(0.5, 0.5);

            // B. DÉPLACEMENT DU DÉCOR (Boucle infinie)
            DeplaceDecor(ciel, pasFond);
            DeplaceDecor(ciel2, pasFond);
            DeplaceDecor(imgbatiment, pasFond);
            DeplaceDecor(imgbatiment2, pasFond);
            DeplaceDecor(imgsol, pasSol);
            DeplaceDecor(imgsol2, pasSol);

            // C. DÉPLACEMENT DES OBSTACLES (Tuyaux & Pièces)
            DeplaceObstacle(imgtuyau1, pasSol);
            DeplaceObstacle(imgtuyau2, pasSol);
            DeplaceObstacle(imgtuyau3, pasSol);
            DeplaceObstacle(imgtuyau4, pasSol);
            DeplaceObstacle(imgpiece1, pasSol);

            // D. COLLISIONS
            VerifCollision();
        }

        // --- MÉTHODES DE DÉPLACEMENT ---

        // Pour les images qui se collent (Ciel, Sol, Batiments)
        private void DeplaceDecor(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            if (double.IsNaN(x)) x = 0;

            x -= pas;

            if (x <= -image.ActualWidth)
            {
                // On recolle l'image pile à la suite (Pixel perfect)
                x = image.ActualWidth - pas;
            }
            Canvas.SetLeft(image, x);
        }

        // Pour les objets isolés (Tuyaux)
        private void DeplaceObstacle(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            if (double.IsNaN(x)) x = 800;

            x -= pas;

            if (x < -image.ActualWidth)
            {
                x = 800; // Retour au départ
                // Ici tu pourrais ajouter un random sur le Canvas.Top pour varier la hauteur
            }
            Canvas.SetLeft(image, x);
        }

        // --- CLAVIER ---
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Up)
            {
                vitesseVerticale = forceSaut; // Hop !
            }
        }

        // --- COLLISIONS ET FIN DU JEU ---
        private void VerifCollision()
        {
            double y = Canvas.GetTop(imgflapy);

            // 1. Mort par le sol ou le plafond
            if (y > 380 || y < -20)
            {
                FinDuJeu();
                return;
            }

            // 2. Mort par les tuyaux (Hitbox réduite pour être sympa)
            Rect rOiseau = new Rect(Canvas.GetLeft(imgflapy) + 10, y + 10, imgflapy.ActualWidth - 20, imgflapy.ActualHeight - 20);

            if (Touche(rOiseau, imgtuyau1) || Touche(rOiseau, imgtuyau2) ||
                Touche(rOiseau, imgtuyau3) || Touche(rOiseau, imgtuyau4))
            {
                FinDuJeu();
            }
        }

        private bool Touche(Rect r1, Image obstacle)
        {
            // On réduit aussi un peu la hitbox des tuyaux
            Rect r2 = new Rect(Canvas.GetLeft(obstacle) + 5, Canvas.GetTop(obstacle), obstacle.ActualWidth - 10, obstacle.ActualHeight);
            return r1.IntersectsWith(r2);
        }

        private void FinDuJeu()
        {
            minuterie.Stop();

            // On retire l'écouteur du clavier pour ne pas faire buguer le menu
            if (fenetrePrincipale != null)
            {
                fenetrePrincipale.KeyDown -= Window_KeyDown;

                // --- NAVIGATION ---
                // On demande au joueur s'il veut rejouer
                var resultat = MessageBox.Show("Game Over ! Rejouer ?", "Flappy Bird", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (resultat == MessageBoxResult.Yes)
                {
                    // On relance le jeu (Appelle la méthode dans MainWindow)
                    fenetrePrincipale.AfficherJeu();
                }
                else
                {
                    // On retourne au menu (Appelle la méthode dans MainWindow)
                    fenetrePrincipale.AfficherDemarrage();
                }
            }
        }
    }
}