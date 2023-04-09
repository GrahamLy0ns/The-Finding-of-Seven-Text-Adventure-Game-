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
using System.Timers;
using Newtonsoft.Json;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Xml.Linq;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

        }
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }
        public int count = 0;
        public int subCount = 0;
        //adding json text to text box
        public static string fileName = @"Resources\text\theBeginning.json";
        public static string jsonString = File.ReadAllText(fileName);
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //querying database for assets
            var imageQuery = from a in window.db.ImageTBLs
                             where a.ImageName == "workshop2"
                             select a.ImageSrc;
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "ruins"
                             select a.SoundSrc;

            string path = soundQuery.ToList()[0];
            string imagePath = imageQuery.ToList()[0];
            background.Background = new ImageBrush(new BitmapImage(new Uri(imagePath, UriKind.Relative)));
            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            //setting scene
            background.Opacity = 0;
            displayBox.Opacity = 0;
            border.Opacity = 0;
            //string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 05 Ruins.mp3";
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
            //seting MainFrame in MainWindow background to black
            window.MainFrame.Background = Brushes.Black;
            //displaying title, p1 and q1
            await displayText("title");
            await displayText("p1");
            await displayText("q1");
        }
        private async void endOfChapter(string chapter)
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string endChapterText = "\n\nEND OF CHAPTER";
            //displaying p3
            foreach (char c in endChapterText)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.GetGold(20, "One of the bandits must have dropped it. Lucky you!");
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            if (chapter == "onTheRoad")
            {
                window.MainFrame.Navigate(new Uri("Page2.xaml", UriKind.Relative));
            }
            else if (chapter == "flee")
            {
                window.MainFrame.Navigate(new Uri("Page5.xaml", UriKind.Relative));
            }
            background.Opacity = 1;
        }
        public class GameText
        {
            public List<GameText> main = new List<GameText>();
            public string P1,P2,P3,P4,P5;
            public string OA1, OA2;
            public string AT1;
            public string SQT1;
            public string[] SA1;
            public string[] SA2;
            public string T1;
            public List<GameText> SQ1 = new List<GameText>();
            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> Q2 = new List<GameText>();
            public List<GameText> Q3 = new List<GameText>();
            public string QT1,QT2,QT3;
            public string[] A1;
            public string[] A2;
            public List<GameText> A3 = new List<GameText>();
            public string[] A4;
            public string[] A5;
            public string[] A6;
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
        private void displayElements(string GameText)
        {
            //declaring variables
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string btn1Text = "";
            string btn2Text = "";
            //logic
            switch (GameText)
            {
                case "q1": btn1Text = json.main[1].Q1[1].A1[0]; btn2Text = json.main[1].Q1[2].A2[0]; break;
                case "q2": btn1Text = json.main[6].Q2[1].A3[0].AT1; btn2Text = json.main[6].Q2[2].A4[0]; break;
                case "q3": btn1Text = json.main[8].Q3[1].A5[0]; btn2Text = json.main[8].Q3[2].A6[0]; break;
                case "sq1": btn1Text = json.main[6].Q2[1].A3[2].SQ1[1].SA1[0]; btn2Text = json.main[6].Q2[1].A3[2].SQ1[2].SA2[0]; break;
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
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string titleText = "Chapter 1\nThe Beginning";
            string p1Text = "\n\n\n" + json.main[0].P1;
            string q1Text = json.main[1].Q1[0].QT1;
            string q2Text = json.main[6].Q2[0].QT2;
            string a1Text = json.main[1].Q1[1].A1[1];
            string a2Text = json.main[1].Q1[2].A2[1];
            string p2Text = json.main[2].P2;
            string oa2Text = json.main[4].OA2;
            string p3Text = json.main[5].P3;
            string p4Text = json.main[7].P4;
            string a5Text = json.main[8].Q3[1].A5[1];
            string a6Text = json.main[8].Q3[2].A6[1];
            string oa1Text = json.main[3].OA1;
            string a3Text = json.main[6].Q2[1].A3[1].T1;
            string sq1Text = json.main[6].Q2[1].A3[2].SQ1[0].SQT1;
            string q3Text = json.main[8].Q3[0].QT3;
            string p5Text = json.main[9].P5;
            string text = "";
            var element = display;
            int delay = 40;
            //determining what needs to be displayed
            switch (GameText)
            {
                case "title": text = titleText; element = title; delay = 80; break;
                case "p1": text = p1Text; break;
                case "q1": text = q1Text; displayElements("q1"); break;
                case "a1": text = a1Text; hideElements("a1"); break;
                case "a2": text = a2Text; hideElements("a2"); break;
                case "p2": text = p2Text; break;
                case "oa2": text = oa2Text; break;
                case "p3": text = p3Text; break;
                case "p4": text = p4Text; break;
                case "a5": text = a5Text; hideElements("a5"); break;
                case "a6": text = a6Text; hideElements("a6"); break;
                case "oa1": text = oa1Text; break;
                case "a3": text = a3Text; hideElements("a3"); break;
                case "q2": text = q2Text; displayElements("q2"); break;
                case "sq1": text = sq1Text; displayElements("sq1"); break;
                case "q3": text = q3Text; displayElements("q3"); break;
                case "p5": text = p5Text; break;
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
            //answer for q1
            if (count == 0)
            {
                //btn1 - yes
                if (btn1Click == true)
                {
                    await displayText("a1");
                    window.GetGold(10, "It was just lying there on your table");
                    await displayText("p2");
                    await displayText("oa1");
                    await displayText("p3");
                    await displayText("q2");
                    count++;
                }
                //btn2 - no
                else if (btn2Click == true)
                {
                    await displayText("a2");
                    await displayText("p2");
                    await displayText("oa2");
                    await displayText("p3");
                    await displayText("q2");
                    count++;
                }
            }
            //answering q2
            else if (count == 1)
            {
                //btn1 - threaten with horse
                if (btn1Click == true)
                {
                    //sub question
                    if (subCount == 0)
                    {
                        await displayText("a3");
                        await displayText("sq1");
                        subCount++;
                    }

                    if (btn1Click == true)
                    {
                        await displayText("sa1");
                        endOfChapter("flee");
                        count = -1;
                    }
                }
                //btn2 - attack
                else if (btn2Click == true)
                {
                    hideElements("a4");
                    await displayText("p4");
                    string text = "You don't appear to have come out unscathed from your encounter with the bandits";
                    window.HeartDeath(text);
                    await displayText("q3");
                    count++;
                }
            }
            //answering q3
            else if (count == 2)
            {
                window.inventoryListBox.Items.Add("Scroll");
                window.inventoryItems.Add("Scroll");
                //btn1
                if (btn1Click == true)
                {
                    await displayText("a5");
                    await displayText("p5");
                    endOfChapter("onTheRoad");
                }
                //btn2
                else if (btn2Click == true)
                {
                    await displayText("a6");
                    await displayText("p5");
                    endOfChapter("onTheRoad");
                }
            }
            else
            {
                endOfChapter("onTheRoad");
            }
        }
        private void display_LayoutUpdated(object sender, EventArgs e)
        {
            scroll.ScrollToBottom();
        }
    }
}
