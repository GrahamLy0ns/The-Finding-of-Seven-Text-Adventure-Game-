using Newtonsoft.Json;
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
            //making info box invisible
            displayBox.Visibility = Visibility.Hidden;
            border.Visibility = Visibility.Hidden;

            //setting scene
            background.Opacity = 0;
            
            
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 15 sans..mp3";
            window.player.Source = new Uri(path, UriKind.Relative);
            while (background.Opacity < 1)
            {
                window.player.Volume += 0.01;
                background.Opacity += 0.01;
                await Task.Delay(40);
            }
            
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
                    shopItemsList.Items.Remove(shopItemsList.SelectedItem);
                    inventoryItems.Items.Add(item);
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
                inventoryItems.Items.Remove(inventoryItems.SelectedItem);
                shopItemsList.Items.Add(item);
            }
            
        }

        private void steelSword_MouseEnter(object sender, MouseEventArgs e)
        {
            string text = "Equiping this steel sword adds +3 to your attack in combat.";
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
            string text = "Impetum Potion adds +3 to your attack temporarily.";
            displayBox.Visibility = Visibility.Visible;
            border.Visibility = Visibility.Visible;
            display.Text = text;
            
        }

        private void woodenShield_MouseEnter(object sender, MouseEventArgs e)
        {
            string text = "Wooden Shield adds +3 to your defence when equiped.";
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
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }

            window.MainFrame.Navigate(new Uri("Page4.xaml", UriKind.Relative));
            window.player.Volume = 0;
            background.Opacity = 1;

            if (inventoryItems.Items == null)
            {

            }
            else
            {
                foreach (var item in inventoryItems.Items)
                {
                    if (item.ToString().Contains("Red Gem"))
                    {
                        window.inventoryItems.Add("Red Gem");
                        window.inventoryListBox.Items.Add("Red Gem");
                    }
                    else if (item.ToString().Contains("Steel Sword"))
                    {
                        window.inventoryItems.Add("Steel Sword");
                        window.inventoryListBox.Items.Add("Steel Sword");
                    }
                    else if (item.ToString().Contains("Healing Gem"))
                    {
                        window.inventoryItems.Add("Healing Gem");
                        window.inventoryListBox.Items.Add("Healing Gem");
                    }
                    else if (item.ToString().Contains("Impetum Potion"))
                    {
                        window.inventoryItems.Add("Impetum Potion");
                        window.inventoryListBox.Items.Add("Impetum Potion");
                    }
                    else if (item.ToString().Contains("Wooden Shield"))
                    {
                        window.inventoryItems.Add("Wooden Shield");
                        window.inventoryListBox.Items.Add("Wooden Shield");
                    }
                    else
                    {
                        window.inventoryItems.Add(item.ToString());
                        window.inventoryListBox.Items.Add(item);
                    }
                    
                }
            }
            
            
        }
    }
}
