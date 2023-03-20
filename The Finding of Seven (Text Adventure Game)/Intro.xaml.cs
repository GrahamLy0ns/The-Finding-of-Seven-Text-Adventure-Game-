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
    /// Interaction logic for Intro.xaml
    /// </summary>
    public partial class Intro : Page
    {
        public Intro()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
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

        


    }
}
