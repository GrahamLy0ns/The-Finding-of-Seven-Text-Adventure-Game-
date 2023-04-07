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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page8.xaml
    /// </summary>
    public partial class Page8 : Page
    {
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }
        public int count = 0;
        public int subCount = 0;
        //adding json text to text box
        public static string fileName = @"Resources\text\banditsOnTheRoad.json";
        public static string jsonString = File.ReadAllText(fileName);
        public Page8()
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
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 45 NGAHHH!!.mp3";
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
            if (window.blackLionFightEncounterd == true)
            {
                await continueWithGameTextDisplay();
            }
            else
            {
                await displayText("title");
                await displayText("p1");
                await displayText("p2");
                await displayText("q1");
            }
        }
        public class GameText
        {
            public List<GameText> main = new List<GameText>();
            public string P1, P2, P3, P4;
            public string T1, W;
            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> SQ1 = new List<GameText>();
            public List<GameText> SQ2 = new List<GameText>();
            public string QT1, QT2, QT3;
            public List<GameText> A1 = new List<GameText>();
            public string AT1, T2, SQT1, SQT2;
            public string[] SA1;
            public string[] SA2;
            public string[] SA3;
            public string[] SA4;
            public string[] A2;
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
                case "q1": btn1Text = json.main[2].Q1[1].A1[0].AT1; btn2Text = json.main[2].Q1[2].A2[0]; break;
                case "sq1": btn1Text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[0]; btn2Text = json.main[2].Q1[1].A1[2].SQ1[2].SA2[0]; break;
                case "sq2": btn1Text = json.main[2].Q1[1].A1[3].SQ2[1].SA3[0]; btn2Text = json.main[2].Q1[1].A1[3].SQ2[2].SA4[0]; break;
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
            string p1Text = "\n\n\n" + json.main[0].P1;
            string p2Text = json.main[1].P2;
            string q1Text = json.main[2].Q1[0].QT1;
            string a1Text = json.main[2].Q1[1].A1[1].T2;
            string sa1Text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[1];
            string sa3Text = json.main[2].Q1[1].A1[3].SQ2[1].SA3[1];
            string p3Text = json.main[3].P3;
            string sq2Text = json.main[2].Q1[1].A1[3].SQ2[0].SQT2;
            string sq1Text = json.main[2].Q1[1].A1[2].SQ1[0].SQT1;
            string sa2Text = json.main[2].Q1[1].A1[2].SQ1[2].SA2[1];
            string a2Text = json.main[2].Q1[2].A2[1];
            string p4Text = json.main[6].P4;
            string wText = json.main[4].W;
            string sa4Text = json.main[2].Q1[1].A1[3].SQ2[2].SA4[1];
            string titleText = "Chapter 4\nBandits on the Road";

            //determining what needs to be displayed
            switch (GameText)
            {
                case "title": text = titleText; delay = 80; element = title; break;
                case "p1": text = p1Text; break;
                case "p2": text = p2Text; break;
                case "q1": text = q1Text; displayElements("q1"); break;
                case "a1": text = a1Text; hideElements("a1"); break;
                case "sa1": text = sa1Text; hideElements("sa1"); break;
                case "sa3": text = sa3Text; hideElements("sa3"); break;
                case "p3": text = p3Text; break;
                case "sq2": text = sq2Text; displayElements("sq2"); break;
                case "sq1": text = sq1Text; displayElements("sq1"); break;
                case "sa2": text = sa2Text; hideElements("sa2"); break;
                case "a2": text = a2Text; hideElements("a2"); break;
                case "p4": text = p4Text ; break;
                case "w": text = wText; break;
                case "sa4": text = sa4Text; hideElements("sa4"); break;
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
                        //adding yellow gem
                        window.inventoryListBox.Items.Add("Yellow Gem");
                        window.inventoryItems.Add("Yellow Gem");
                        await displayText("sq2");
                        subCount++;
                    }
                    else if (subCount == 2)
                    {
                        await displayText("sa3");
                        count++;
                    }
                    else
                    {
                        await displayText("a1");
                        await displayText("sq1");
                        subCount++;
                    }
                }
                else if (btn2Click == true)
                {
                    if (subCount == 1)
                    {
                        await displayText("sa2");
                        //adding yellow gem
                        window.inventoryListBox.Items.Add("Yellow Gem");
                        window.inventoryItems.Add("Yellow Gem");
                        await displayText("sq2");
                        subCount++;
                    }
                    else if (subCount == 2)
                    {
                        await displayText("sa4");
                        count++;
                    }
                    else
                    {
                        await displayText("a2");
                        count++;
                    }
                }
            }

            if (count == 1)
            {
                await displayText("p3");
                await fight();
            }
        }
        private async Task fight()
        {
            //execute fight page
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            window.MainFrame.Navigate(new Uri("Page7.xaml", UriKind.Relative));
            background.Opacity = 1;
            //changing fight path so player can return to this page if won
            window.returnFromFightPath = "Page8.xaml";
            window.blackLionFightEncounterd = true;
        }
        private async Task continueWithGameTextDisplay()
        {
            await displayText("w");
            await displayText("p4");
            await endOfChapter();
        }
        private async Task endOfChapter()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            window.MainFrame.Navigate(new Uri("Page9.xaml", UriKind.RelativeOrAbsolute));
            background.Opacity = 1;
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
        private void displayBox_LayoutUpdated(object sender, EventArgs e)
        {
            scroll.ScrollToBottom();
        }
    }
}
