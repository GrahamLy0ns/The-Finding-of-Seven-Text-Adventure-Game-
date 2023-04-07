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
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page12.xaml
    /// </summary>
    public partial class Page12 : Page
    {
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }
        public int count = 0;
        //adding json text to text box
        public static string fileName = @"Resources\text\theDarkCastle.json";
        public static string jsonString = File.ReadAllText(fileName);
        public Page12()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            //setting scene
            background.Opacity = 0;
            displayBox.Opacity = 0;
            border.Opacity = 0;
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 80 Finale.mp3";
            window.player.Source = new Uri(path, UriKind.Relative);
            while (background.Opacity < 1)
            {
                window.player.Volume += 0.01;
                background.Opacity += 0.01;
                await Task.Delay(40);
            }
            //fading in displayBox
            while (displayBox.Opacity < 1)
            {
                border.Opacity += 0.01;
                displayBox.Opacity += 0.01;
                await Task.Delay(40);
            }
            //checking if final fight took place
            if (window.finalFightEncountered == true)
            {
                await displayText("p4");
                await displayText("p5");
                endOfChapter();
            }
            else
            {
                //displaying text
                await displayText("title");
                await displayText("p1");
                //checking if player has all the gems
                int gemCount = 0;
                foreach (var item in window.inventoryItems)
                {
                    switch (item)
                    {
                        case "Red Gem": gemCount++; break;
                        case "Orange Gem": gemCount++; break;
                        case "Yellow Gem": gemCount++; break;
                        case "Green Gem": gemCount++; break;
                        case "Blue Gem": gemCount++; break;
                        case "Indigo Gem": gemCount++; break;
                        case "Violet Gem": gemCount++; break;
                    }
                }
                if (gemCount == 7)
                {
                    await displayText("oa1");
                }
                else
                {
                    await displayText("oa2");
                    await youDied();
                }
                await displayText("p2");
                await displayText("q1");
            }
        }
        public class GameText
        {
            public List<GameText> main = new List<GameText>();
            public string P1, OA1, OA2, P2, P3, P4, P5, QT1, QT2, QT3, A1, A2;
            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> Q2 = new List<GameText>();
            public List<GameText> Q3 = new List<GameText>();
            public string[] A3, A4, A5, A6;
        }
        private void displayElements(string GameText)
        {
            //declaring variables
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string btn1Text = "";
            string btn2Text = "";
            //logic
            switch (GameText)
            {
                case "q1": btn1Text = json.main[4].Q1[1].A1; btn2Text = json.main[4].Q1[2].A2; break;
                case "q2": btn1Text = json.main[6].Q2[1].A3[0]; btn2Text = json.main[6].Q2[2].A4[0]; break;
                case "q3": btn1Text = json.main[7].Q3[1].A5[0]; btn2Text = json.main[7].Q3[2].A6[0]; break;
            }
            //showing elements and adding content to them
            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = btn1Text;
            btn2Display.Text = btn2Text;
        }
        private void hideElements(string GameText)
        {
            //hiding elements
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
        }
        private async Task displayText(string GameText)
        {
            //declaring variables
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            var element = display;
            int delay = 40;
            string text = "";
            string titleText = "Chapter 8\nThe Dark Castle";
            string p1Text = "\n\n\n" + json.main[0].P1;
            string p2Text = json.main[3].P2;
            string q1Text = json.main[4].Q1[0].QT1;
            string oa1Text = json.main[1].OA1;
            string oa2Text = json.main[2].OA2;
            string p3Text = json.main[5].P3;
            string q2Text = json.main[6].Q2[0].QT2;
            string a3Text = json.main[6].Q2[1].A3[1];
            string a4Text = json.main[6].Q2[2].A4[1];
            string q3Text = json.main[7].Q3[0].QT3;
            string a5Text = json.main[7].Q3[1].A5[1];
            string a6Text = json.main[7].Q3[2].A6[1];
            string p4Text = json.main[8].P4;
            string p5Text = json.main[9].P5;
            //logic
            switch (GameText)
            {
                case "title": text = titleText; element = title; delay = 80; break;
                case "p1": text = p1Text; break;
                case "p2": text = p2Text; break;
                case "q1": text = q1Text; displayElements("q1"); break;
                case "oa1": text = oa1Text; hideElements("oa1"); break;
                case "oa2": text = oa2Text; hideElements("oa2"); break;
                case "p3": text = p3Text; break;
                case "q2": text = q2Text; displayElements("q2"); break;
                case "a3": text = a3Text ; hideElements("a3"); break;
                case "a4": text = a4Text ; hideElements("a4"); break;
                case "q3": text = q3Text ; displayElements("q3"); break;
                case "a5": text = a5Text ; hideElements("a5"); break;
                case "a6": text = a6Text ; hideElements("a6"); break;
                case "p4": text = p4Text ; break;
                case "p5": text = p5Text ; break;   
            }
            //displaying text
            foreach (char c in text)
            {
                element.Text += c;
                await Task.Delay(delay);
            }
        }
        private async void gameTextDisplay()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            if (count == 0)
            {
                btn1.Visibility = Visibility.Collapsed;
                btn2.Visibility = Visibility.Collapsed;
                await displayText("p3");
                await displayText("q2");
                count++;
            }
            else if (count == 1)
            {
                if (btn1Click == true)
                {
                    await displayText("a4");
                }
                else if (btn2Click == true)
                {
                    await displayText("a3");
                }
                await displayText("q3");
                count++;
            }
            else if (count == 2)
            {
                if (btn1Click == true)
                {
                    await displayText("a5");
                    //take damage
                    window.HeartDeath("Electricity hurts more than you think.");
                    //navigate to fight page
                    window.fightEncountered = true;
                    while (background.Opacity > 0)
                    {
                        background.Opacity -= 0.01;
                        window.player.Volume -= 0.01;
                    }
                    window.MainFrame.Navigate(new Uri("Page7.xaml", UriKind.Relative));
                    background.Opacity = 1;
                    //changing fight path so player can return to this page if won
                    window.returnFromFightPath = "Page12.xaml";
                    window.finalFightEncountered = true;
                }
                else if (btn2Click == true)
                {
                    await displayText("a6");
                    //navigate to fight page
                    window.fightEncountered = true;
                    while (background.Opacity > 0)
                    {
                        background.Opacity -= 0.01;
                        window.player.Volume -= 0.01;
                    }
                    window.MainFrame.Navigate(new Uri("Page7.xaml", UriKind.Relative));
                    background.Opacity = 1;
                    //changing fight path so player can return to this page if won
                    window.returnFromFightPath = "Page12.xaml";
                    window.finalFightEncountered = true;
                }
            }
        }
        private void displayBox_LayoutUpdated(object sender, EventArgs e)
        {
            scroll.ScrollToBottom();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn1Click = true;
            gameTextDisplay();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            btn2Click = true;
            gameTextDisplay();
        }
        private async Task youDied()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);


            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }

            window.MainFrame.Navigate(new Uri("Page6.xaml", UriKind.Relative));
            background.Opacity = 1;
        }
        private async void endOfChapter()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            window.MainFrame.Navigate(new Uri("Page13.xaml", UriKind.Relative));
            background.Opacity = 1;
        }
    }
}
