using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media; // Pour RotateTransform
using System.Windows.Threading;

namespace Flapy_bird
{
    public partial class UCjeu : UserControl
    {
        // --- 1. VARIABLES GLOBALES ---
        private DispatcherTimer minuterie;

        // Vitesses de défilement (comme dans ton Infinite Runner)
        private int pasSol = 5;      // Vitesse rapide (Premier plan)
        private int pasFond = 2;     // Vitesse lente (Arrière plan)

        // Physique de l'oiseau
        private double vitesseVerticale = 0;
        private double gravite = 0.5;
        private double forceSaut = -8;

        public UCjeu()
        {
            InitializeComponent();
            InitializeTimer();
        }

        // --- 2. INITIALISATION ---
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // On active la réception des touches du clavier
            this.Focus();
            Window fenetre = Window.GetWindow(this);
            if (fenetre != null) fenetre.KeyDown += Window_KeyDown;
        }

        private void InitializeTimer()
        {
            minuterie = new DispatcherTimer();
            minuterie.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            minuterie.Tick += Jeu;
            minuterie.Start(); // Le jeu démarre tout de suite
        }

        // --- 3. BOUCLE DE JEU (Exécutée 60 fois par seconde) ---
        private void Jeu(object? sender, EventArgs e)
        {
            // A. PHYSIQUE DE L'OISEAU
            vitesseVerticale += gravite; // La gravité augmente la vitesse de chute

            double y = Canvas.GetTop(imgflapy);
            // On applique le mouvement vertical
            Canvas.SetTop(imgflapy, y + vitesseVerticale);

            // Rotation de l'oiseau (nez vers le bas quand il tombe)
            imgflapy.RenderTransform = new RotateTransform(vitesseVerticale * 3);
            imgflapy.RenderTransformOrigin = new Point(0.5, 0.5);

            // B. DÉPLACEMENT DU DÉCOR (Noms de tes images XAML)
            DeplaceDecor(ciel, pasFond);
            DeplaceDecor(ciel2, pasFond);

            DeplaceDecor(imgbatiment, pasFond);
            DeplaceDecor(imgbatiment2, pasFond);

            DeplaceDecor(imgsol, pasSol);
            DeplaceDecor(imgsol2, pasSol);

            // C. DÉPLACEMENT DES OBSTACLES (TUYAUX)
            // On utilise ta méthode Deplacecaillou (renommée ou telle quelle)
            DeplaceObstacle(imgtuyau1, pasSol);
            DeplaceObstacle(imgtuyau2, pasSol);
            DeplaceObstacle(imgtuyau3, pasSol);
            DeplaceObstacle(imgtuyau4, pasSol);

            // On bouge aussi la pièce
            DeplaceObstacle(imgpiece1, pasSol);

            // D. COLLISIONS
            VerifCollision();
        }

        // --- 4. TES MÉTHODES DE DÉPLACEMENT ---

        // Pour le décor infini (Ciel, Sol, Batiments)
        public void DeplaceDecor(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            Canvas.SetLeft(image, x - pas); // Déplace vers la gauche

            // Si l'image est entièrement sortie à gauche
            if (x <= -image.ActualWidth)
            {
                // On la replace pile à la suite de l'autre (800px)
                Canvas.SetLeft(image, image.ActualWidth - pas);
            }
        }

        // Pour les objets qui reviennent en boucle (Tuyaux, Pièces)
        // C'est l'équivalent de ta méthode "Deplacecaillou"
        public void DeplaceObstacle(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            Canvas.SetLeft(image, x - pas);

            // Si l'objet sort de l'écran
            if (x < -image.ActualWidth)
            {
                Canvas.SetLeft(image, 800); // On le remet au début (droite)

                // Ici, tu pourrais ajouter du code pour changer la hauteur (Top) aléatoirement
            }
        }

        // --- 5. GESTION DU CLAVIER ---
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Up)
            {
                // Le saut modifie la vitesse verticale instantanément
                vitesseVerticale = forceSaut;
            }
        }

        // --- 6. COLLISIONS ---
        private void VerifCollision()
        {
            // Vérif Sol et Plafond
            double y = Canvas.GetTop(imgflapy);
            if (y > 380 || y < -20) FinDuJeu();

            // Vérif Tuyaux
            // On crée une Hitbox légèrement plus petite (-10) pour être gentil avec le joueur
            Rect rOiseau = new Rect(Canvas.GetLeft(imgflapy) + 5, y + 5, imgflapy.ActualWidth - 10, imgflapy.ActualHeight - 10);

            if (Touche(rOiseau, imgtuyau1) || Touche(rOiseau, imgtuyau2) ||
                Touche(rOiseau, imgtuyau3) || Touche(rOiseau, imgtuyau4))
            {
                FinDuJeu();
            }
        }

        private bool Touche(Rect r1, Image obstacle)
        {
            Rect r2 = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.ActualWidth, obstacle.ActualHeight);
            return r1.IntersectsWith(r2);
        }

        private void FinDuJeu()
        {
            minuterie.Stop();
            // Ici tu peux afficher un MessageBox ou changer d'écran
        }
    }
}