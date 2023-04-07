using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page7.xaml
    /// </summary>
    public partial class Page7 : Page
    {
        public Page7()
        {
            InitializeComponent();
        }
        public bool btnClick = false;
        private Storyboard storyboard = new Storyboard();
        public bool enemyDead = false;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.fightEncountered = true;
            window.fightEncounteredCount++;
            background.Opacity = 0;
            if (window.fightEncounteredCount == 1)
            {
                EnemyHeart8.Visibility = Visibility.Collapsed;
                EnemyHeart7.Visibility = Visibility.Collapsed;
                EnemyHeart6.Visibility = Visibility.Collapsed;
                EnemyHeart5.Visibility = Visibility.Collapsed;
            }
            else if (window.fightEncounteredCount == 2)
            {
                EnemyHeart8.Visibility = Visibility.Collapsed;
                EnemyHeart7.Visibility = Visibility.Collapsed;
            }
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 46 Spear of Justice.mp3";
            window.player.Source = new Uri(path, UriKind.Relative);
            while (background.Opacity < 1)
            {
                window.player.Volume += 0.01;
                background.Opacity += 0.01;
                await Task.Delay(40);
            }
            if (window.inventoryListBox.Items.Contains("Impetum Potion"))
            {
                await ImpetumPotionCheck();
            }
            boxAnimation();
        }
        private async Task ImpetumPotionCheck()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            string greyHeart = @"pack://application:,,,/greyHeart.png";
            var result = MessageBox.Show("Use Impetum Potion?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (EnemyHeart8.Source.ToString().Contains("heart") && EnemyHeart8.Visibility == Visibility.Visible)
                {
                    EnemyHeart8.Source = new BitmapImage(new Uri(greyHeart));
                }
                else if (EnemyHeart7.Source.ToString().Contains("heart") && EnemyHeart7.Visibility == Visibility.Visible)
                {
                    EnemyHeart7.Source = new BitmapImage(new Uri(greyHeart));
                }
                else if (EnemyHeart6.Source.ToString().Contains("heart") && EnemyHeart6.Visibility == Visibility.Visible)
                {
                    EnemyHeart6.Source = new BitmapImage(new Uri(greyHeart));
                }
                else if (EnemyHeart5.Source.ToString().Contains("heart") && EnemyHeart5.Visibility == Visibility.Visible)
                {
                    EnemyHeart5.Source = new BitmapImage(new Uri(greyHeart));
                }
                else if (EnemyHeart4.Source.ToString().Contains("heart") && EnemyHeart4.Visibility == Visibility.Visible)
                {
                    EnemyHeart4.Source = new BitmapImage(new Uri(greyHeart));
                }
                else if (EnemyHeart3.Source.ToString().Contains("heart") && EnemyHeart3.Visibility == Visibility.Visible)
                {
                    EnemyHeart3.Source = new BitmapImage(new Uri(greyHeart));
                }
            }
            window.inventoryListBox.Items.Remove("Impetum Potion");
            await Task.Delay(40);
        }

        private void boxAnimation()
        {
            
            // Create a DoubleAnimationUsingKeyFrames to move the rectangle left and right
            DoubleAnimationUsingKeyFrames moveAnimation = new DoubleAnimationUsingKeyFrames();
            moveAnimation.Duration = TimeSpan.FromSeconds(5);
            moveAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Create the key-frames for the animation
            EasingDoubleKeyFrame keyFrame1 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            EasingDoubleKeyFrame keyFrame2 = new EasingDoubleKeyFrame(1200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.5)));
            EasingDoubleKeyFrame keyFrame3 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5)));

            // Add the key-frames to the animation
            moveAnimation.KeyFrames.Add(keyFrame1);
            moveAnimation.KeyFrames.Add(keyFrame2);
            moveAnimation.KeyFrames.Add(keyFrame3);

            // Set the animation targets
            Storyboard.SetTarget(moveAnimation, bar);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(Canvas.LeftProperty));

            // Create a Storyboard and add the DoubleAnimationUsingKeyFrames to it
            
            storyboard.Children.Add(moveAnimation);

            // Start the Storyboard
            storyboard.Begin();
        }

        private void clickToStopBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);

            storyboard.Stop();
            double currentX = (double)bar.GetValue(Canvas.LeftProperty);
            //start is 591
            if (currentX >= 551 && currentX <= 640)
            {
                enemyHeartDeath();
            }
            else
            {
                Random random = new Random();
                int damage = random.Next(0, 13);
                window.HeartDeath("You failed to successfully hit your opponent and they struck you as a result!\n\nYou take " + damage + " points of damage!");
            }

            if (enemyDead == true)
            {
                //do nothing
            }
            else
            {
                boxAnimation();
            }
        }
        private void enemyHeartDeath()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);

            string sfx = @"Resources\music\sfx\success-fanfare-trumpets-6185.mp3";
            string greyHeart = @"pack://application:,,,/greyHeart.png";

            if (EnemyHeart8.Source.ToString().Contains("heart") && EnemyHeart8.Visibility == Visibility.Visible)
            {
                EnemyHeart8.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (EnemyHeart7.Source.ToString().Contains("heart") && EnemyHeart7.Visibility == Visibility.Visible)
            {
                EnemyHeart7.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (EnemyHeart6.Source.ToString().Contains("heart") && EnemyHeart6.Visibility == Visibility.Visible)
            {
                EnemyHeart6.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (EnemyHeart5.Source.ToString().Contains("heart") && EnemyHeart5.Visibility == Visibility.Visible)
            {
                EnemyHeart5.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (EnemyHeart4.Source.ToString().Contains("heart") && EnemyHeart4.Visibility == Visibility.Visible)
            {
                EnemyHeart4.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (EnemyHeart3.Source.ToString().Contains("heart") && EnemyHeart3.Visibility == Visibility.Visible)
            {
                EnemyHeart3.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (EnemyHeart2.Source.ToString().Contains("heart") && EnemyHeart2.Visibility == Visibility.Visible)
            {
                EnemyHeart2.Source = new BitmapImage(new Uri(greyHeart));
            }
            else
            {
                EnemyHeart1.Source = new BitmapImage(new Uri(greyHeart));
                MessageBox.Show("Your Enemy has pershied. You Won!");
                window.player.Source = new Uri(sfx, UriKind.Relative);
                enemyDead = true;
                endBattle();
            }

            if (enemyDead == true)
            {
                //do nothing
            }
            else
            {
                window.player.Source = new Uri(sfx, UriKind.Relative);

                Task t = Task.Run(() => {
                    MessageBox.Show("You hit your enemy!");
                });
                t.Wait();
            }
        }
        
        private async void endBattle()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);

            window.GetGold(20, "Your enemy dropped some Gold!");
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            window.MainFrame.Navigate(new Uri(window.returnFromFightPath, UriKind.Relative));
            background.Opacity = 1;
        }
    }
    
}
