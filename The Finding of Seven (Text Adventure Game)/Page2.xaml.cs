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
using Newtonsoft.Json;
using System.IO;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }
        public int count = 0;
        public bool riddleCorrect = false;
        public bool submitBtnClicked = false;
        public int subCount = 0;
        //adding json text to text box
        public static string fileName = @"Resources\text\onTheRoad.json";
        public static string jsonString = File.ReadAllText(fileName);
        public Page2()
        {
            InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //querying database for assets
            var backgroundImageQuery = from a in window.db.ImageTBLs
                                  where a.ImageName == "road3"
                                  select a.ImageSrc;
            var trollImageQuery = from a in window.db.ImageTBLs
                                  where a.ImageName == "troll3"
                                  select a.ImageSrc;
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "bonetrousle"
                             select a.SoundSrc;
            string path = soundQuery.ToList()[0];
            string backgroundImage = backgroundImageQuery.ToList()[0];
            string trollImageSrc = trollImageQuery.ToList()[0];
            background.Background = new ImageBrush(new BitmapImage(new Uri(backgroundImage, UriKind.Relative)));
            trollImage.Source = new BitmapImage(new Uri(trollImageSrc));
            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            trollImage.Opacity = 0;
            riddle.Opacity = 0;
            submitBtn.Opacity = 0;
            riddle.Visibility = Visibility.Collapsed;
            submitBtn.Visibility = Visibility.Collapsed;
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
            await displayText("q1");
        }

        public class GameText
        {
            public List<GameText> main = new List<GameText>();
            public string P1, P2, P3;
            public string T1, W, L;
            public string AT4;
            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> Q2 = new List<GameText>();
            public List<GameText> Q3 = new List<GameText>();
            public string QT1, QT2, QT3;
            public string[] A1;
            public string[] A2;
            public string[] A3;
            public string[] A5;
            public string[] A6;
            public List<GameText> A4 = new List<GameText>();
            
        }
        private void displayBox_LayoutUpdated(object sender, EventArgs e)
        {
            scroll.ScrollToBottom();
        }

        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 24 Bonetrousle.mp3";
            window.player.LoadedBehavior = MediaState.Manual;
            window.player.Source = new Uri(path, UriKind.RelativeOrAbsolute);
            window.player.Play();
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            if (count == 0)
            {
                if (btn1Click == true)
                {
                    await displayText("a1");
                    //display troll
                    await displayTroll();
                    await displayText("p2");
                    await displayText("q2");
                    count++;
                }
                else if (btn2Click == true)
                {
                    MainWindow window = (MainWindow)Window.GetWindow(this);
                    string text = "The Troll's rock like arm seems to have wounded you.";
                    window.HeartDeath(text);
                    await displayText("a2");
                    //display troll
                    await displayTroll();
                    await displayText("p2");
                    await displayText("q2");
                    count++;
                }
            }
            else if (count == 1)
            {
                if (btn1Click == true)
                {
                    await displayText("a3");
                    youDied();
                }
                else if (btn2Click == true || submitBtnClicked == true)
                {
                    if (submitBtnClicked == false)
                    {
                        await displayText("a4");
                        riddleReveal();
                    }
                    if (riddleCorrect == true)
                    {
                        await displayText("w");
                        count++;
                    }
                    else if(riddleCorrect == false && submitBtnClicked == true)
                    {
                        await displayText("l");
                        youDied();
                    }
                }
                
            }

            if (count == 2)
            {
                await displayText("p3");
                await displayText("q3");
                count++;
            }
            else if (count == 3)
            {
                if (btn1Click == true)
                {
                    await displayText("a5");
                    endOfChapter("merchant");
                }
                else if (btn2Click == true)
                {
                    await displayText("a6");
                    endOfChapter("cave");
                }
            }
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
                case "q2": btn1Text = json.main[3].Q2[1].A3[0]; btn2Text = json.main[3].Q2[2].A4[0].AT4; break;
                case "q3": btn1Text = json.main[5].Q3[1].A5[0]; btn2Text = json.main[5].Q3[2].A6[0]; break;
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
            string titleText = "Chapter 2\nOn the Road";
            string p1Text = "\n\n\n" + json.main[0].P1;
            string q1Text = json.main[1].Q1[0].QT1;
            string a1Text = json.main[1].Q1[1].A1[1];
            string a2Text = json.main[1].Q1[2].A2[1];
            string p2Text = json.main[2].P2;
            string a3Text = json.main[3].Q2[1].A3[1];
            string a4Text = json.main[3].Q2[2].A4[1].T1;
            string wText = json.main[3].Q2[2].A4[2].W;
            string lText = json.main[3].Q2[2].A4[3].L;
            string p3Text = json.main[4].P3;
            string a6Text = json.main[5].Q3[2].A6[1];
            string a5Text = json.main[5].Q3[1].A5[1];
            string q2Text = json.main[3].Q2[0].QT2;
            string q3Text = json.main[5].Q3[0].QT3;
            string text = "";
            var element = display;
            int delay = 40;
            //determining what needs to be displayed
            switch (GameText)
            {
                case "title": text = titleText; element = title; delay = 80; break;
                case "p1": text = p1Text; element = display; delay = 40; break;
                case "q1": text = q1Text; element = display; delay = 40; displayElements("q1"); break;
                case "a1": text = a1Text; element = display; delay = 40; hideElements("a1"); break;
                case "a2": text = a2Text; element = display; delay = 40; hideElements("a2"); break;
                case "p2": text = p2Text; element = display; delay = 40; break;
                case "p3": text = p3Text; element = display; delay = 40; break;
                case "a5": text = a5Text; element = display; delay = 40; hideElements("a5"); break;
                case "a6": text = a6Text; element = display; delay = 40; hideElements("a6"); break;
                case "a3": text = a3Text; element = display; delay = 40; hideElements("a3"); break;
                case "q2": text = q2Text; element = display; delay = 40; displayElements("q2"); break;
                case "q3": text = q3Text; element = display; delay = 40; displayElements("q3"); break;
                case "a4": text = a4Text; element = display; delay = 40; hideElements("a4"); break;
                case "w": text = wText; element = display; delay = 40; hideElements("w"); break;
                case "l": text = lText; element = display; delay = 40; hideElements("l"); break;
            }
            //displaying text
            foreach (char c in text)
            {
                element.Text += c;
                await Task.Delay(delay);
            }
        }
        private async Task displayTroll()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 78 You Idiot.mp3";
            trollSound.Source = new Uri(path, UriKind.Relative);
            trollSound.LoadedBehavior = MediaState.Manual;
            window.player.LoadedBehavior = MediaState.Manual;
            trollSound.Play();
            window.player.Pause();
            //fade out display box
            while (displayBox.Opacity > 0)
            {
                border.Opacity -= 0.04;
                displayBox.Opacity -= 0.04;
                await Task.Delay(40);
            }
            //fade in troll
            while (trollImage.Opacity < 1)
            {
                trollImage.Opacity += 0.04;
                await Task.Delay(40);
            }
            //pausing
            await Task.Delay(2000);
            //fade out troll and fade in display box
            while (displayBox.Opacity < 1)
            {
                trollImage.Opacity -= 0.01;
                border.Opacity += 0.01;
                displayBox.Opacity += 0.01;
                trollSound.Volume -= 0.01;
                await Task.Delay(40);
            }
            window.player.Play();
        }
        private async void youDied()
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
        private async void endOfChapter(string chapter)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            
            window.GetGold(20, "It's the least you should be paid after dealing with that Troll!");
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            if (chapter == "merchant")
            {
                window.MainFrame.Navigate(new Uri("Page3.xaml", UriKind.Relative));
            }
            else if (chapter == "cave")
            {
                //go to cave chapter which then leads to fall chapter
                //skips talking to merchant and moving village
                MessageBox.Show("The Merchant approaches you and insists you see their wares.");
                window.MainFrame.Navigate(new Uri("Page3.xaml", UriKind.Relative));
            }
            background.Opacity = 1;
        }
        private async void riddleReveal()
        {
            riddle.Visibility = Visibility.Visible;
            submitBtn.Visibility = Visibility.Visible;

            while (riddle.Opacity < 1)
            {
                riddle.Opacity += 0.04;
                submitBtn.Opacity += 0.04;
                await Task.Delay(40);
            }
        }
        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (riddle.Text.ToLower().Contains("candle") || riddle.Text.ToLower().Contains("pencil"))
            {
                riddleCorrect = true;
            }
            else
            {
                riddleCorrect = false;
            }
            submitBtnClicked = true;
            gameTextDisplay();
            riddle.Visibility = Visibility.Collapsed;
            submitBtn.Visibility = Visibility.Collapsed;
        }

        private void riddle_GotFocus(object sender, RoutedEventArgs e)
        {
            riddle.Clear();
        }
        
    }
}
