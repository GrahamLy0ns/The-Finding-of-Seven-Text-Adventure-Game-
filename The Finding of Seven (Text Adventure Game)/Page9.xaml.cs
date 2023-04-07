using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page9.xaml
    /// </summary>
    public partial class Page9 : Page
    {
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }
        public int count = 0;
        public int subCount = 0;
        //adding json text to text box
        public static string fileName = @"Resources\text\theFall.json";
        public static string jsonString = File.ReadAllText(fileName);
        public Page9()
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
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 31 Waterfall.mp3";
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
            //displaying text
            await displayText("title");
            await displayText("p1");
            await displayText("p2");
            await displayText("q1");
        }
        public class GameText
        {
            public List<GameText> main = new List<GameText>();
            public string P1, P2;
            public string T2, AT1, T3, T1, AT2;

            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> SQ1 = new List<GameText>();
            public string QT1;
            public List<GameText> SA1 = new List<GameText>();
            public List<GameText> A2 = new List<GameText>();
            public string SQT1;
            public string[] SA2;
            public string[] A1;

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
                case "q1": btn1Text = json.main[2].Q1[1].A1[0]; btn2Text = json.main[2].Q1[2].A2[0].AT2; break;
                case "sq1": btn1Text = json.main[2].Q1[2].A2[2].SQ1[1].SA1[0].AT1; btn2Text = json.main[2].Q1[2].A2[2].SQ1[2].SA2[0]; break;
            }
            //showing elements and adding content to them
            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = btn1Text;
            btn2Display.Text = btn2Text;
        }
        private void hideElements(string GameText)
        {
            //declaring variables
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            //hiding elements
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
        }
        private async Task displayText(string GameText)
        {
            //declaring variables
            MainWindow window = (MainWindow)Window.GetWindow(this);
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            var element = display;
            int delay = 40;
            string text = "";
            string p1Text = "\n\n\n" + json.main[0].P1;
            string titleText = "Chapter 5\nThe Fall";
            string p2Text = json.main[1].P2;
            string q1Text = json.main[2].Q1[0].QT1;
            string sa1Text = json.main[2].Q1[2].A2[2].SQ1[1].SA1[1].T2;
            string a1Text = json.main[2].Q1[1].A1[1];
            string sa2Text = json.main[2].Q1[2].A2[2].SQ1[2].SA2[1];
            string a2Text = json.main[2].Q1[2].A2[1].T1;
            string sq1Text = json.main[2].Q1[2].A2[2].SQ1[0].SQT1;
            //logic
            switch (GameText)
            {
                case "title": text = titleText; element = title; delay = 80; break;
                case "p1": text = p1Text; break;
                case "p2": text = p2Text; break;
                case "q1": text = q1Text; displayElements("q1"); break;
                case "sa1": text = sa1Text; hideElements("sa1"); break;
                case "a1": text = a1Text; hideElements("a1"); break;
                case "sa2": text = sa2Text; hideElements("sa2"); break;
                case "a2": text = a2Text; hideElements("a2"); break;
                case "sq1": text = sq1Text; displayElements("sq1"); break;
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
                if (btn1Click == true)
                {
                    if (subCount == 1)
                    {
                        await displayText("sa1");
                        //add green gem
                        window.inventoryListBox.Items.Add("Green Gem");
                        window.inventoryItems.Add("Green Gem");
                        endOfChapter();
                        
                    }
                    else
                    {
                        await displayText("a1");
                        youDied();
                    }
                }
                else if (btn2Click == true)
                {
                    if (subCount == 1)
                    {
                        await displayText("sa2");
                        //repeat
                        count = 0;
                        subCount = 0;
                        await displayText("q1");
                    }
                    else
                    {
                        await displayText("a2");
                        await displayText("sq1");
                        subCount++;
                    }
                }
            }
        }
        private async void youDied()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
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
            window.GetGold(20, "You found some Gold lying outside the cavern");
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            window.MainFrame.Navigate(new Uri("Page10.xaml", UriKind.Relative));
            background.Opacity = 1;
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
    }
}
