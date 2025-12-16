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
        private MainWindow fenetrePrincipale;
        private DispatcherTimer minuterie;
        private Random aleatoire = new Random();

        private int pasSol = 5;
        private int pasFond = 2;
        private int score = 0;

        private double vitesseVerticale = 0;
        private double gravite = 0.5;
        private double forceSaut = -8;

        public UCjeu(MainWindow window)
        {
            InitializeComponent();
            this.fenetrePrincipale = window;
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

            DeplaceDecor(ciel, pasFond);
            DeplaceDecor(ciel2, pasFond);

            DeplaceDecor(imgsol, pasSol);
            DeplaceDecor(imgsol2, pasSol);

            DeplaceObstacle(imgtuyau1, pasSol);
            DeplaceObstacle(imgtuyau2, pasSol);
            DeplaceObstacle(imgtuyau3, pasSol);
            DeplaceObstacle(imgtuyau4, pasSol);
            DeplaceObstacle(imgtuyau5, pasSol);
            DeplaceObstacle(imgtuyau6, pasSol);

            DeplacePiece(imgpiece1, pasSol);

            VerifCollision();
        }

        private void DeplaceDecor(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            if (double.IsNaN(x)) x = 0;

            x -= pas;

            if (x <= -image.ActualWidth)
            {
                x = image.ActualWidth - pas;
            }
            Canvas.SetLeft(image, x);
        }

        private void DeplaceObstacle(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            if (double.IsNaN(x)) x = 800;

            x -= pas;

            if (x < -image.ActualWidth)
            {
                x = 800;
            }
            Canvas.SetLeft(image, x);
        }

        private void DeplacePiece(Image image, int pas)
        {
            double x = Canvas.GetLeft(image);
            if (double.IsNaN(x)) x = 800;

            x -= pas;

            if (x < -image.ActualWidth)
            {
                x = 800;
                double nouvelleHauteur = aleatoire.Next(50, 300);
                Canvas.SetTop(image, nouvelleHauteur);
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

            if (y > 380 || y < -20)
            {
                FinDuJeu();
                return;
            }

            Rect rOiseau = new Rect(Canvas.GetLeft(imgflapy) + 10, y + 10, imgflapy.ActualWidth - 20, imgflapy.ActualHeight - 20);

            if (Touche(rOiseau, imgtuyau1) || Touche(rOiseau, imgtuyau2) ||
                Touche(rOiseau, imgtuyau3) || Touche(rOiseau, imgtuyau4))
            {
                FinDuJeu();
            }

            if (imgpiece1.Visibility == Visibility.Visible && Touche(rOiseau, imgpiece1))
            {
                imgpiece1.Visibility = Visibility.Hidden;
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

                if (resultat == MessageBoxResult.Yes)
                {
                    fenetrePrincipale.AfficherJeu();
                }
                else
                {
                    fenetrePrincipale.AfficherDemarrage();
                }
            }
        }


    }
}