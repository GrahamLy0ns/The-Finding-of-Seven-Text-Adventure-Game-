using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.GetGold(110, "test");
            //getting asset src from database
            var backgroundImageQuery = from a in window.db.ImageTBLs
                                  where a.ImageName == "merchantBackground"
                                  select a.ImageSrc;
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "sans"
                             select a.SoundSrc;
            string path = soundQuery.ToList()[0];
            string backgroundImageSrc = backgroundImageQuery.ToList()[0];
            background.Background = new ImageBrush(new BitmapImage(new Uri(backgroundImageSrc, UriKind.Relative)));
            //adding items to the shop List
            foreach (string shopItem in window.shopItems)
            {
                shopItemsList.Items.Add(shopItem);
            }
            //adding inventory items
            foreach (string inventoryItem in window.inventoryItems)
            {
                inventoryItems.Items.Add(inventoryItem);
            }
            //making info box invisible
            displayBox.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;
            exitShopBtn.Visibility = Visibility.Hidden;
            //setting scene
            background.Opacity = 0;
            //string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 15 sans..mp3";
            window.player.Source = new Uri(path, UriKind.Relative);
            while (background.Opacity < 1)
            {
                window.player.Volume += 0.01;
                background.Opacity += 0.01;
                await Task.Delay(40);
            }
            exitShopBtn.Visibility = Visibility.Visible;
        }

        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (shopItemsList.SelectedItem == null)
            {
                //do nothing
            }
            else
            {
                MainWindow window = (MainWindow)Window.GetWindow(this);
                string[] array = shopItemsList.SelectedItem.ToString().Split('(');
                int price = int.Parse(array[1].Substring(0, 2));
                int goldBalance = int.Parse(window.balance.Text);
                if (price <= goldBalance)
                {
                    object item = shopItemsList.SelectedItem;
                    shopItemsList.Items.Remove(item);
                    window.shopItems.Remove(item.ToString());
                    inventoryItems.Items.Add(item);
                    window.inventoryItems.Add(item.ToString());
                    goldBalance -= price;
                    window.balance.Text = goldBalance.ToString();

                    //playing gold sfx
                    string path = @"Resources\music\sfx\cha-ching.mp3";
                    window.player.LoadedBehavior = MediaState.Manual;
                    window.player.Source = new Uri(path, UriKind.RelativeOrAbsolute);
                    window.player.Play();
                }
                else
                {
                    MessageBox.Show($"You do not have enough to purchase this item!\nCurrent Balance: {goldBalance}\nPrice of Item: {price}");
                }
            }
        }

        private void removeItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (inventoryItems.SelectedItem == null)
            {
                //do nothing
            }
            else
            {
                MainWindow window = (MainWindow)Window.GetWindow(this);
                string[] array = inventoryItems.SelectedItem.ToString().Split('(');
                try
                {
                    int price = int.Parse(array[1].Substring(0, 2));
                    int goldBalance = int.Parse(window.balance.Text);

                    //updating balance
                    goldBalance += price;
                    window.balance.Text = goldBalance.ToString();

                    //playing gold sfx
                    string path = @"Resources\music\sfx\cha-ching.mp3";
                    window.player.LoadedBehavior = MediaState.Manual;
                    window.player.Source = new Uri(path, UriKind.RelativeOrAbsolute);
                    window.player.Play();

                    object item = inventoryItems.SelectedItem;
                    inventoryItems.Items.Remove(item);
                    window.inventoryItems.Remove(item.ToString());
                    shopItemsList.Items.Add(item);
                    window.shopItems.Add(item.ToString());
                }
                catch
                {
                    MessageBox.Show("Cannot remove this item!");
                }
            }
            
        }

        private void steelSword_MouseEnter(object sender, MouseEventArgs e)
        {
            string text = "Allows you to take out two health points instead of one in combat.";
            displayBox.Visibility = Visibility.Visible;
            display.Text = text;
        }

        private void healingPotion_MouseEnter(object sender, MouseEventArgs e)
        {
            string text = "Basic healing potion that heals you by +3 Health Points.";
            displayBox.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            display.Text = text;
            
        }

        private void impetumPotion_MouseEnter(object sender, MouseEventArgs e)
        {
            string text = "Impetum Potion reduces your enemy's health by 1 Health Point.";
            displayBox.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            display.Text = text;
            
        }

        private void woodenShield_MouseEnter(object sender, MouseEventArgs e)
        {
            string text = "Wooden Shield adds +3 to your defence.";
            displayBox.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            display.Text = text;
            
        }

        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            string text = "It calls to you......";
            displayBox.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            display.Text = text;
        }

        private async void exitShopBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //revealing merchant btn so player can now return to merchant shop at any time
            window.merchantShopBtn.Visibility = Visibility.Visible;
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            foreach (var item in window.inventoryItems)
            {
                if (item.ToString().Contains("Wooden Shield") && window.inventoryListBox.Items.Contains("Wooden Shield") == false)
                {
                    window.DefenseTextBlock.Visibility = Visibility.Visible;
                    window.shield1.Visibility = Visibility.Visible;
                    window.shield2.Visibility = Visibility.Visible;
                    window.shield3.Visibility = Visibility.Visible;
                }
            }
            //navigation 
            window.MainFrame.Navigate(new Uri(window.returnPageFromMerchantShop, UriKind.Relative));
            window.player.Volume = 0;
            background.Opacity = 1;
        }
    }
}
