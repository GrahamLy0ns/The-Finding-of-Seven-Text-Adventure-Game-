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
        //boolean values to determine on and off states in the game
        public bool merchantShopEncountered = false;
        public bool fightEncountered = false;
        public int fightEncounteredCount = 0;
        public bool blackLionFightEncounterd = false;
        public bool finalFightEncountered = false;
        public string returnFromFightPath = "Page4.xaml";
        public string returnPageFromMerchantShop = "Page4.xaml";
        //lists
        public List<string> shopItems = new List<string>() { "Steel Sword(10 Gold)", "Red Gem (40 Gold)", "Healing Potion (20 Gold)", "Impetum Potion (20 Gold)", "Wooden Shield (20 Gold)" };
        public List<string> inventoryItems = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //displaying date
            DateTime dateTime = DateTime.Today;
            string currentDate = dateTime.ToString();
            date.Text = currentDate.Split(' ')[0];
            AttackTextBlock.Visibility = Visibility.Collapsed;
            DefenseTextBlock.Visibility = Visibility.Collapsed;
            shield1.Visibility = Visibility.Collapsed;
            shield2.Visibility = Visibility.Collapsed;
            shield3.Visibility = Visibility.Collapsed;
            //hiding merchant nav btn
            merchantShopBtn.Visibility = Visibility.Hidden;
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
            string redHeart = @"pack://application:,,,/heart.png";
            //checking if player has shield
            if (inventoryListBox.Items.Contains("Wooden Shield"))
            {
                if (shield3.Opacity == 1)
                {
                    shield3.Opacity = 0.4;
                }
                else if (shield2.Opacity == 1)
                {
                    shield2.Opacity = 0.4;
                }
                else if (shield1.Opacity == 1)
                {
                    shield1.Opacity = 0.4;
                    inventoryListBox.Items.Remove("Wooden Shield");
                    
                }
            }
            else
            {
                //if player does not have shield do the following
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
                    if (inventoryListBox.Items.Contains("Healing Potion"))
                    {
                        MessageBox.Show("As you lay there about to die you remember the Healing Potion you have.");
                        heart1.Source = new BitmapImage(new Uri(redHeart));
                        heart2.Source = new BitmapImage(new Uri(redHeart));
                        heart3.Source = new BitmapImage(new Uri(redHeart));
                        inventoryListBox.Items.Remove("Healing Potion");
                    }
                    else
                    {
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
                }
            }
            player.Source = new Uri(sfx, UriKind.Relative);
            Task t = Task.Run(() => {
                MessageBox.Show($"You've been hit!\n{text}");
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
            MainFrame.Navigate(new Uri("Page12.xaml", UriKind.Relative));
            startGameButton.Visibility = Visibility.Collapsed;
        }
        private void quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            string path = "";
            switch (MainFrame.Content.ToString())
            {
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page1": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 05 Ruins.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page2": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 24 Bonetrousle.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page3": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 15 sans..mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page4": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 51 Another Medium.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page5": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 81 An Ending.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page6": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 11 Determination.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page7": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 46 Spear of Justice.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page8": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 45 NGAHHH!!.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page9": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 31 Waterfall.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page10": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 28 Premonition.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page11": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 22 Snowdin Town.mp3"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page12": path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 80 Finale.mp3"; break;
                default: path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 02 Start Menu.mp3"; break;
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

        private void merchantShopBtn_Click(object sender, RoutedEventArgs e)
        {
            //check to figure out which page to navigate back to
            switch (MainFrame.Content.ToString())
            {
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page1": break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page2": break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page3": break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page4": returnPageFromMerchantShop = "Page4.xaml"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page5": returnPageFromMerchantShop = "Page5.xaml"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page6": returnPageFromMerchantShop = "Page6.xaml"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page7": returnPageFromMerchantShop = "Page7.xaml"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page8": returnPageFromMerchantShop = "Page8.xaml"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page9": returnPageFromMerchantShop = "Page9.xaml"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page10": returnPageFromMerchantShop = "Page10.xaml"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page11": returnPageFromMerchantShop = "Page11.xaml"; break;
                case "The_Finding_of_Seven__Text_Adventure_Game_.Page12": returnPageFromMerchantShop = "Page12.xaml"; break;
                default: returnPageFromMerchantShop = "Page4.xaml"; break;
            }
            //navigating
            MainFrame.Navigate(new Uri("Page3.xaml", UriKind.Relative));
        }
    }
}

