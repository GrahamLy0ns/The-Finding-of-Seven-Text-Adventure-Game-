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
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Interop;


namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool fightEncountered = false;
        public bool blackLionFightEncounterd = false;
        public List<string> inventoryItems = new List<string>();
        public string returnFromFightPath = "Page4.xaml";
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
        }
        
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //hiding inventory box
            inventoryListBox.Visibility = Visibility.Collapsed;
            inventoryLabel.Visibility = Visibility.Collapsed;
            //hiding reveal nav btn
            revealNavigation.Visibility = Visibility.Hidden;
            soundControlBtns.Visibility = Visibility.Hidden;
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 02 Start Menu.mp3";
            player.LoadedBehavior = MediaState.Manual;
            player.Source = new Uri(path, UriKind.RelativeOrAbsolute);
            player.Play();

            string titleText = "The Finding of Seven";
            string text = "\n\nHello and welcome to my text adventure game!\nIn this game you will face many decisions, many of which will lead to your demise so choose wisely.\nClick the 'Start Game' button below whenever you are ready to start the game!";
            //displaying title
            foreach (char c in titleText)
            {
                title.Text += c;
                await Task.Delay(80);

            }
            await Task.Delay(1000);

            //displaying intro text
            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

        }
        public void GetGold(int goldAmount, string msg)
        {
            string path = @"Resources\music\sfx\cha-ching.mp3";
            int goldBalance = Convert.ToInt32(balance.Text);
            goldBalance += goldAmount;
            balance.Text = goldBalance.ToString();

            Task t = Task.Run(() => {
                MessageBox.Show($"You Got {goldAmount} Gold!\n{msg}");
            });
            
            player.LoadedBehavior = MediaState.Manual;
            player.Source = new Uri(path, UriKind.RelativeOrAbsolute);
            player.Play();
            t.Wait();
            
        }

        public async void HeartDeath(string text)
        {
           
            string sfx = @"Resources\music\sfx\heart-death.mp3";
            string greyHeart = @"pack://application:,,,/greyHeart.png";

            if (heart5.Source.ToString().Contains("heart"))
            {
                heart5.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (heart4.Source.ToString().Contains("heart"))
            {
                heart4.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (heart3.Source.ToString().Contains("heart"))
            {
                heart3.Source = new BitmapImage(new Uri(greyHeart));
            }
            else if (heart2.Source.ToString().Contains("heart"))
            {
                heart2.Source = new BitmapImage(new Uri(greyHeart));
            }
            else
            {
                heart1.Source = new BitmapImage(new Uri(greyHeart));
                MessageBox.Show("You died");
                //fade out scene change code below
                while (background.Opacity > 0)
                {
                    player.Volume -= 0.01;
                    background.Opacity -= 0.01;
                    await Task.Delay(40);
                }
                MainFrame.Navigate(new Uri("Page6.xaml", UriKind.Relative));
                background.Opacity = 1;
            }
            
            player.Source = new Uri(sfx, UriKind.Relative);

            Task t = Task.Run(() => {
                MessageBox.Show($"You Lost a Life!\n{text}");
            });
            t.Wait();
        }

        private async void startGameButton_Click(object sender, RoutedEventArgs e)
        {
            
            //fade out music and scene
            while (startGameButton.Opacity > 0)
            {
                
                player.Volume -= 0.01;
                startGameButton.Opacity -= 0.01;
                display.Opacity -= 0.01;
                border.Opacity -= 0.01;
                title.Opacity -= 0.01;
                await Task.Delay(40);
            }
      
            
            MainFrame.Navigate(new Uri("Page11.xaml", UriKind.Relative));

            startGameButton.Visibility = Visibility.Hidden;


        }
        private void quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

       
        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            string path = "";
            if (MainFrame.Content == null)
            {
                path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 02 Start Menu.mp3";
            }
            else
            {
                if (MainFrame.Content.ToString().Contains("Page1"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 05 Ruins.mp3";

                }
                else if (MainFrame.Content.ToString().Contains("Page2"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 24 Bonetrousle.mp3";
                }
                else if (MainFrame.Content.ToString().Contains("Page3"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 15 sans..mp3";
                }
                else if (MainFrame.Content.ToString().Contains("Page4"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 51 Another Medium.mp3";
                }
                else if (MainFrame.Content.ToString().Contains("Page5"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 81 An Ending.mp3";

                }
                else if (MainFrame.Content.ToString().Contains("Page6"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 11 Determination.mp3";
                }
                else if (MainFrame.Content.ToString().Contains("Page7"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 46 Spear of Justice.mp3";
                }
                else if (MainFrame.Content.ToString().Contains("Page8"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 45 NGAHHH!!.mp3";
                }
                else if (MainFrame.Content.ToString().Contains("Page9"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 31 Waterfall.mp3";
                }
                else if (MainFrame.Content.ToString().Contains("Page10"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 28 Premonition.mp3";

                }
                else if (MainFrame.Content.ToString().Contains("Page11"))
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 22 Snowdin Town.mp3";
                }
                else
                {
                    path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 02 Start Menu.mp3";

                }
            }
           
            player.LoadedBehavior = MediaState.Manual;
            player.Source = new Uri(path, UriKind.RelativeOrAbsolute);
            player.Play();

        }

        private void mainMenu_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0]);
            fightEncountered = false;
        }
        private void pause_Click(object sender, RoutedEventArgs e)
        {
            
            //running new thread with message box to "pause" execution
            Task t = Task.Run(() => {
                
                MessageBoxResult result = MessageBox.Show("Game paused. Click \"OK\" to resume.", "Paused", MessageBoxButton.OK);
                
            });
            t.Wait();
            

        }

        private void hideNav_Click(object sender, RoutedEventArgs e)
        {
            MainNavigation.Visibility = Visibility.Hidden;
            revealNavigation.Visibility = Visibility.Visible;
            soundControlBtns.Visibility = Visibility.Hidden;
            inventoryLabel.Visibility = Visibility.Collapsed;
            inventoryListBox.Visibility = Visibility.Collapsed;
        }

        private void revealNavigation_Click(object sender, RoutedEventArgs e)
        {
            MainNavigation.Visibility = Visibility.Visible;
            revealNavigation.Visibility = Visibility.Hidden;
        }

        private void soundControlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (soundControlBtns.Visibility == Visibility.Hidden)
            {
                soundControlBtns.Visibility = Visibility.Visible;
            }
            else
            {
                soundControlBtns.Visibility = Visibility.Hidden;
            }
            
        }

        private void mute_Click(object sender, RoutedEventArgs e)
        {
            player.Volume = 0;
        }

        private void unMute_Click(object sender, RoutedEventArgs e)
        {
            player.Volume = 1;
        }

        private void increaseVolume_Click(object sender, RoutedEventArgs e)
        {
            player.Volume += 0.1;
        }

        private void decreaseVolume_Click(object sender, RoutedEventArgs e)
        {
            player.Volume -= 0.1;
        }

        private void showInventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            //checking state of inventory box
            if (inventoryListBox.Visibility == Visibility.Visible)
            {
                inventoryLabel.Visibility = Visibility.Collapsed;
                inventoryListBox.Visibility = Visibility.Collapsed;
            }
            else 
            {
                inventoryLabel.Visibility = Visibility.Visible;
                inventoryListBox.Visibility = Visibility.Visible;
            }
            

        }
    }
}

