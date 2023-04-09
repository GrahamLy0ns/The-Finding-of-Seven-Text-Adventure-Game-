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
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.CodeDom;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }
        public int count = 0;

        //adding json text to text box
        public static string fileName = @"Resources\text\flee.json";
        public static string jsonString = File.ReadAllText(fileName);
        public Page5()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            var imageQuery = from a in window.db.ImageTBLs
                             where a.ImageName == "burningVillage"
                             select a.ImageSrc;
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "anEnding"
                             select a.SoundSrc;
            string path = soundQuery.ToList()[0];
            string imageSrc = imageQuery.ToList()[0];
            background.Background = new ImageBrush(new BitmapImage(new Uri(imageSrc, UriKind.Relative)));
            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;

            //setting scene
            background.Opacity = 0;
            displayBox.Opacity = 0;
            border.Opacity = 0;
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
            public string P1, P2, P3;
            public string T1, T2, SQT1;
            public string AT4;
            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> Q2 = new List<GameText>();
            public List<GameText> A4 = new List<GameText>();
            public List<GameText> SQ1 = new List<GameText>();
            public string QT1, QT2;
            public string[] A1;
            public string[] A2;
            public string[] A3;
            public string[] SA1;
            public string[] SA2;
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
                case "q1": btn1Text = json.main[2].Q1[1].A1[0]; btn2Text = json.main[2].Q1[2].A2[0]; break;
                case "q2": btn1Text = json.main[4].Q2[1].A3[0]; btn2Text = json.main[4].Q2[2].A4[0].T1; break;
                case "sq1": btn1Text = json.main[4].Q2[2].A4[2].SQ1[1].SA1[0]; btn2Text = json.main[4].Q2[2].A4[2].SQ1[2].SA2[0]; break;
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
            string text = "";
            var element = display;
            int delay = 40;
            string p1Text = "\n\n\n" + json.main[0].P1;
            string p2Text = json.main[1].P2;
            string q1Text = json.main[2].Q1[0].QT1;
            string a1Text = json.main[2].Q1[1].A1[1];
            string a2Text = json.main[2].Q1[2].A2[1];
            string p3Text = json.main[3].P3;
            string q2Text = json.main[4].Q2[0].QT2;
            string a3Text = json.main[4].Q2[1].A3[1];
            string a4Text = json.main[4].Q2[2].A4[1].T2;
            string sq1Text = json.main[4].Q2[2].A4[2].SQ1[0].SQT1;
            string sa1Text = json.main[4].Q2[2].A4[2].SQ1[1].SA1[1];
            string sa2Text = json.main[4].Q2[2].A4[2].SQ1[2].SA2[1];
            string titleText = "Flee";

            switch (GameText)
            {
                case "title": text = titleText; element = title; delay = 80; break;
                case "p1": text = p1Text; break;
                case "p2": text = p2Text; break;
                case "q1": text = q1Text; displayElements("q1"); break;
                case "a1": text = a1Text; hideElements("a1"); break;
                case "a2": text = a2Text; hideElements("a2"); break;
                case "p3": text = p3Text; break;
                case "q2": text = q2Text; displayElements("q2"); break;
                case "a3": text = a3Text; hideElements("a3"); break;
                case "a4": text = a4Text; hideElements("a4"); break;
                case "sq1": text = sq1Text; displayElements("sq1");break;
                case "sa1": text = sa1Text; hideElements("sa1"); break;
                case "sa2": text = sa2Text; hideElements("sa2"); break;
            }
            //displaying text
            foreach (char c in text)
            {
                element.Text += c;
                await Task.Delay(delay);
            }
        }
        private async Task SceneChange()
        {
            string path = "Resources/music/toby fox - UNDERTALE Soundtrack - 53 Stronger Monsters.mp3";
            MainWindow window = (MainWindow)Window.GetWindow(this);
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.02;
                background.Opacity -= 0.02;
                await Task.Delay(40);
            }
            window.player.LoadedBehavior = MediaState.Manual;
            window.player.Source = new Uri(path, UriKind.RelativeOrAbsolute);
            window.player.Play();
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri(@"Resources\imgs\fleeRoad.png", UriKind.RelativeOrAbsolute));
            background.Background = image;
            while (background.Opacity < 1)
            {
                window.player.Volume += 0.02;
                background.Opacity += 0.02;
                await Task.Delay(40);
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
        private async void gameTextDisplay()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            if (count == 0)
            {
                //options for q1
                if (btn1Click == true)
                {
                    await displayText("a1");
                    await displayText("p3");
                    await displayText("q2");
                    count++;
                }
                else if (btn2Click == true)
                {
                    await displayText("a2");
                    await displayText("p3");
                    await displayText("q2");
                    count++;
                }
            }
            //answering q2
            else if (count == 1)
            {
                if (btn1Click == true)
                {
                    await displayText("a3");
                    //adding scroll to inventory
                    window.inventoryListBox.Items.Add("Scroll");
                    window.inventoryItems.Add("Scroll");
                    endOfChapter();
                }
                else if (btn2Click == true)
                {
                    await displayText("a4");
                    await SceneChange();
                    await displayText("sq1");
                    count++;
                }
            }
            else if (count == 2)
            {
                if (btn1Click == true)
                {
                    await displayText("sa1");
                    youDied();
                }
                else if(btn2Click == true)
                {
                    await displayText("sa2");
                    youDied();
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
            window.GetGold(20, "Who would leave Gold lying on the road like that?");
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            window.MainFrame.Navigate(new Uri("Page2.xaml", UriKind.Relative));
            background.Opacity = 1;
        }
    }
    
}
