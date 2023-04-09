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
using System.CodeDom;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page10.xaml
    /// </summary>
    public partial class Page10 : Page
    {
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }
        public int count = 0;
        public int subCount = 0;
        public int subSubCount = 0;
        //adding json text to text box
        public static string fileName = @"Resources\text\theDrawbridge.json";
        public static string jsonString = File.ReadAllText(fileName);
        public Page10()
        {
            InitializeComponent();
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            btn2Click = true;
            gameTextDisplay();
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn1Click = true;
            gameTextDisplay();
            submitBtnDream.Visibility = Visibility.Collapsed;
            dreamTextBox.Visibility = Visibility.Collapsed;
            dreamLabel.Visibility = Visibility.Collapsed;
        }
        private void displayBox_LayoutUpdated(object sender, EventArgs e)
        {
            scroll.ScrollToBottom();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //getting assets from the db
            var imageQuery = from a in window.db.ImageTBLs
                             where a.ImageName == "bridge"
                             select a.ImageSrc;
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "forTheFans"
                             select a.SoundSrc;
            string path = soundQuery.ToList()[0];
            string imageSrc = imageQuery.ToList()[0];
            background.Background = new ImageBrush(new BitmapImage(new Uri(imageSrc, UriKind.Relative)));
            //hiding dream ui
            dreamTextBox.Visibility = Visibility.Collapsed;
            submitBtnDream.Visibility = Visibility.Collapsed;
            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            //collapsing riddle
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
            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> Q2 = new List<GameText>();
            public List<GameText> A1 = new List<GameText>();
            public List<GameText> A3 = new List<GameText>();
            public List<GameText> SQ1 = new List<GameText>();
            public List<GameText> SA1 = new List<GameText>();
            public List<GameText> SSQ1 = new List<GameText>();
            public string P1, P2, P3, QT1, T1, W, L;
            public string QT2, AT3, T2, SQT1, SAT1, T3, SSQT1, AT1, R;
            public string[] A2;
            public string[] SSA1;
            public string[] SA2;
            public string[] A4;
        }
        private async void displayElements(string GameText)
        {
            //declaring variables
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string btn1Text = "";
            string btn2Text = "";
            string dreamLabelText = "";
            //logic
            switch (GameText)
            {
                case "q1": btn1Text = json.main[1].Q1[1].A1[0].AT1; btn2Text = json.main[1].Q1[2].A2[0]; break;
                case "sq1": btn1Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[0].SAT1; dreamLabelText = json.main[3].Q2[1].A3[2].SQ1[2].SA2[0]; break;
                case "ssq1":btn1Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[2].SSQ1[1].SSA1[0]; dreamLabelText = json.main[3].Q2[1].A3[2].SQ1[2].SA2[0]; break;
                case "r": riddle.Visibility = Visibility.Visible; submitBtn.Visibility = Visibility.Visible; break;
                case "q2": btn1Text = json.main[3].Q2[1].A3[0].AT3; dreamLabelText = json.main[3].Q2[1].A3[2].SQ1[2].SA2[0]; break;
            }
            if (GameText == "q1")
            {
                btn1.Visibility = Visibility.Visible;
                btn2.Visibility = Visibility.Visible;
                btn1Display.Text = btn1Text;
                btn2Display.Text = btn2Text;
            }
            else if (GameText == "q2" || GameText == "ssq1" || GameText == "sq1")
            {
                btn1.Visibility = Visibility.Visible;
                btn1Display.Text = btn1Text;
                dreamLabel.Content = dreamLabelText;
                dreamLabel.Visibility = Visibility.Visible;
                dreamTextBox.Visibility = Visibility.Visible;
                submitBtnDream.Visibility = Visibility.Visible;
            }
        }
        private void hideElements(string GameText)
        {
            //riddle condition
            if (GameText == "r")
            {
                riddle.Visibility = Visibility.Collapsed;
                submitBtn.Visibility = Visibility.Collapsed;
            }
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
            string titleText = "Chapter 6\nThe Drawbridge";
            string p1Text = "\n\n\n" + json.main[0].P1;
            string q1Text = json.main[1].Q1[0].QT1;
            string a1Text = json.main[1].Q1[1].A1[1].T1;
            string rText = json.main[1].Q1[1].A1[2].R;
            string a2Text = json.main[1].Q1[2].A2[1];
            string ssa1Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[2].SSQ1[1].SSA1[1];
            string sa1Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[1].T3;
            string p2Text = json.main[2].P2;
            string a3Text = json.main[3].Q2[1].A3[1].T2;
            string sa3Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[1].T3;
            string ssq1Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[2].SSQ1[0].SSQT1;
            string sq1Text = json.main[3].Q2[1].A3[2].SQ1[0].SQT1;
            string lText = json.main[1].Q1[1].A1[4].L;
            string wText = json.main[1].Q1[1].A1[3].W;
            string q2Text = json.main[3].Q2[0].QT2;
            string p3Text = json.main[4].P3;
            //logic
            switch (GameText)
            {
                case "title": text = titleText; element = title; delay = 80; break;
                case "p1": text = p1Text; break;
                case "q1": text = q1Text; break;
                case "a1": text = a1Text; hideElements("a1"); break;
                case "r": text = rText; break;
                case "a2": text = a2Text; hideElements("a2"); break;
                case "ssa1": text = ssa1Text; hideElements("ssa1"); break;
                case "sa1": text = sa1Text; hideElements("sa1"); break;
                case "p2": text = p2Text; break;
                case "a3": text = a3Text; hideElements("a3"); break;
                case "sa3": text = sa3Text; hideElements("sa3"); break;
                case "ssq1": text = ssq1Text; break;
                case "sq1": text = sq1Text; break;
                case "l": text = lText; break;
                case "w": text = wText; break;
                case "q2": text = q2Text; break;
                case "p3": text = p3Text; break;
            }
            //displaying text
            foreach (char c in text)
            {
                element.Text += c;
                await Task.Delay(delay);
            }
            //displaying objects for appropriate text
            switch (GameText)
            {
                case "q1": displayElements("q1"); break;
                case "r": displayElements("r"); break;
                case "ssq1": displayElements("ssq1"); break;
                case "sq1": displayElements("sq1"); break;
                case "q2": displayElements("q2"); break;
            }
        }
        private async void gameTextDisplay()
        {
            if (count == 0)
            {
                if(btn1Click == true)
                {
                    await displayText("a1");
                    await displayText("r");
                    //rest of code in submitBtn click event
                }
                else if (btn2Click == true)
                {
                    await displayText("a2");
                    youDied();
                }
            }
            else if(count == 1)
            {
                if (btn1Click == true)
                {
                    if (subCount == 1)
                    {
                        if (subSubCount == 1)
                        {
                            await displayText("ssa1");
                            youDied();
                        }
                        else
                        {
                            await displayText("sa3");
                            await displayText("ssq1");
                            subSubCount++;
                        }
                    }
                    else
                    {
                        await displayText("a3");
                        await displayText("sq1");
                        subCount++;
                    }
                }
            }
        }
        private async void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            riddle.Visibility = Visibility.Collapsed;
            submitBtn.Visibility = Visibility.Collapsed;
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //logic
            if (riddle.Text.ToLower() == "sapphire")
            {
                await displayText("w");
                window.inventoryListBox.Items.Add("Blue Gem");
                window.inventoryItems.Add("Blue Gem");
            }
            else
            {
                await displayText("l");
            }
            //scene change
            while (background.Opacity > 0)
            {
                background.Opacity -= 0.1;
                await Task.Delay(60);
            }
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 28 Premonition.mp3";
            window.player.Source = new Uri(path, UriKind.Relative);
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri("Resources/imgs/haze.png", UriKind.RelativeOrAbsolute));
            background.Background = image;
            //fade back in
            while (background.Opacity < 1)
            {
                background.Opacity += 0.1;
                await Task.Delay(60);
            }
            await displayText("p2");
            await displayText("q2");
            count++;
        }
        private void riddle_GotFocus(object sender, RoutedEventArgs e)
        {
            riddle.Clear();
        }
        private async void submitBtnDream_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            btn1.Visibility = Visibility.Collapsed;
            submitBtnDream.Visibility = Visibility.Collapsed;
            dreamLabel.Visibility = Visibility.Collapsed;
            dreamTextBox.Visibility= Visibility.Collapsed;
            //continue display
            if (dreamTextBox.Text.ToLower().Contains("dream"))
            {
                await displayText("p3");
                //add indigo gem
                window.inventoryListBox.Items.Add("Indigo Gem");
                window.inventoryItems.Add("Indigo Gem");
                endOfChapter();
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
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            window.MainFrame.Navigate(new Uri("Page11.xaml", UriKind.Relative));
            background.Opacity = 1;
        }
    }
}
