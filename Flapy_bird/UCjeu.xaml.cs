using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Flapy_bird
{
    public partial class UCjeu : UserControl
    {
        private MainWindow fenetrePrincipale;
        private DispatcherTimer minuterie;
        private Random aleatoire = new Random();

        private int pasSol = 5;
        private int pasFond = 2;
        private int score = 0;

        private double vitesseVerticale = 0;
        private double gravite = 0.5;
        private double forceSaut = -8;

 
        private const double LargeurFenetre = 800;

        public UCjeu(MainWindow window)
        {
            InitializeComponent();
            this.fenetrePrincipale = window;

            // Chargement du Skin
            if (!string.IsNullOrEmpty(MainWindow.SkinActuel))
            {
                var uri = new Uri(MainWindow.SkinActuel, UriKind.Relative);
                imgflapy.Source = new BitmapImage(uri);
            }

            InitializeTimer();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
            if (fenetrePrincipale != null)
            {
                fenetrePrincipale.KeyDown += Window_KeyDown;
            }
        }

        private void InitializeTimer()
        {
            minuterie = new DispatcherTimer();
            minuterie.Interval = TimeSpan.FromMilliseconds(16);
            minuterie.Tick += BoucleJeu;
            minuterie.Start();
        }

        private void BoucleJeu(object? sender, EventArgs e)
        {
         
            vitesseVerticale += gravite;
            double y = Canvas.GetTop(imgflapy);
            if (double.IsNaN(y)) y = 200;
            Canvas.SetTop(imgflapy, y + vitesseVerticale);

            imgflapy.RenderTransform = new RotateTransform(vitesseVerticale * 3);
            imgflapy.RenderTransformOrigin = new Point(0.5, 0.5);

      
            DeplaceDecor(ciel, pasFond, LargeurFenetre);
            DeplaceDecor(ciel2, pasFond, LargeurFenetre);

            DeplaceDecor(imgbatiment, pasFond, LargeurFenetre);
            DeplaceDecor(imgbatiment2, pasFond, LargeurFenetre);

            DeplaceDecor(imgsol, pasSol, LargeurFenetre);
            DeplaceDecor(imgsol2, pasSol, LargeurFenetre);

            // --- Obstacles ---
            DeplaceObstacle(imgtuyau1, pasSol);
            DeplaceObstacle(imgtuyau2, pasSol);
            DeplaceObstacle(imgtuyau3, pasSol);
            DeplaceObstacle(imgtuyau4, pasSol);
            DeplaceObstacle(imgtuyau5, pasSol);
            DeplaceObstacle(imgtuyau6, pasSol);

            // --- Pièces ---
            DeplacePiece(imgpiece1, pasSol);
            DeplacePiece(imgpiece2, pasSol);
            DeplacePiece(imgpiece3, pasSol);

            // --- Collisions ---
            VerifCollision();
        }

        
        private void DeplaceDecor(Image image, int pas, double largeurImage)
        {
            double x = Canvas.GetLeft(image);
            if (double.IsNaN(x)) x = 0;

            x -= pas;

            // Si l'image est entièrement sortie à gauche (ex: x < -800)
            if (x < -largeurImage)
            {
               
                x += (largeurImage * 2);
            }
            Canvas.SetLeft(image, x);
        }



        private void DeplaceObstacle(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            if (double.IsNaN(x)) x = 800;
            x -= pas;
            if (x < -70) { x = 800; }
            Canvas.SetLeft(image, x);
        }

     
        private void DeplacePiece(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            if (double.IsNaN(x)) x = 800;

            x -= pas;

            // Si la pièce sort à gauche
            if (x < -image.ActualWidth)
            {
                x = 800 + aleatoire.Next(0, 300);

                // 2. Hauteur aléatoire
                double nouvelleHauteur = aleatoire.Next(50, 300);
                Canvas.SetTop(image, nouvelleHauteur);

                // 3. On la rend visible à nouveau
                image.Visibility = Visibility.Visible;
            }
            Canvas.SetLeft(image, x);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Up)
            {
                vitesseVerticale = forceSaut;
            }
        }

        private void VerifCollision()
        {
            double y = Canvas.GetTop(imgflapy);
            if (y > 380 || y < -10) { FinDuJeu(); return; }

            Rect rOiseau = new Rect(Canvas.GetLeft(imgflapy) + 10, y + 10, imgflapy.ActualWidth - 20, imgflapy.ActualHeight - 20);

            // Tuyaux
            if (Touche(rOiseau, imgtuyau1) || Touche(rOiseau, imgtuyau2) ||
                Touche(rOiseau, imgtuyau3) || Touche(rOiseau, imgtuyau4) ||
                Touche(rOiseau, imgtuyau5) || Touche(rOiseau, imgtuyau6))
            {
                FinDuJeu();
            }

            // --- VÉRIFICATION DES 3 PIÈCES ---
            CheckPieceCollision(rOiseau, imgpiece1);
            CheckPieceCollision(rOiseau, imgpiece2);
            CheckPieceCollision(rOiseau, imgpiece3);
        }

      
        private void CheckPieceCollision(Rect rOiseau, Image piece)
        {
            if (piece.Visibility == Visibility.Visible && Touche(rOiseau, piece))
            {
                piece.Visibility = Visibility.Hidden;
                score++;
                txtScore.Text = "Score: " + score;
            }
        }

        private bool Touche(Rect r1, Image obstacle)
        {
            Rect r2 = new Rect(Canvas.GetLeft(obstacle) + 5, Canvas.GetTop(obstacle), obstacle.ActualWidth - 10, obstacle.ActualHeight);
            return r1.IntersectsWith(r2);
        }

        private void FinDuJeu()
        {
            minuterie.Stop();
            if (fenetrePrincipale != null)
            {
                fenetrePrincipale.KeyDown -= Window_KeyDown;
                var resultat = MessageBox.Show("Game Over ! Score : " + score + "\nRejouer ?", "Flappy Bird", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (resultat == MessageBoxResult.Yes) fenetrePrincipale.AfficherJeu();
                else fenetrePrincipale.AfficherDemarrage();
            }
        }
    }
}