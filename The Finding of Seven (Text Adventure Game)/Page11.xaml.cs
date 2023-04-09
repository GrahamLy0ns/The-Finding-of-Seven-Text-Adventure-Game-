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
using System.Data.SqlTypes;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page11.xaml
    /// </summary>
    public partial class Page11 : Page
    {
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }
        public int count = 0;
        public int subCount = 0;
        public int subSubCount = 0;
        //adding json text to text box
        public static string fileName = @"Resources\text\theLittles.json";
        public static string jsonString = File.ReadAllText(fileName);
        public Page11()
        {
            InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //getting assets from the db
            var imageQuery = from a in window.db.ImageTBLs
                             where a.ImageName == "village2"
                             select a.ImageSrc;
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "snowdinTown"
                             select a.SoundSrc;
            string path = soundQuery.ToList()[0];
            string imageSrc = imageQuery.ToList()[0];
            background.Background = new ImageBrush(new BitmapImage(new Uri(imageSrc, UriKind.Relative)));
            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            //collapsing riddle ui
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
            await displayText("p2");
            await displayText("q1");
        }
        public class GameText
        {
            public string P1, P2, QT1, AT1, SQT1;
            public string[] SA, SA2, A2;
            public List<GameText> main = new List<GameText>();
            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> A1 = new List<GameText>();
            public List<GameText> SQ1 = new List<GameText>();
            public List<GameText> SA1 = new List<GameText>();
            public List<GameText> SSQ1 = new List<GameText>();
            public string SAT1, T1, T2, SSQT1, W, L;
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
                case "sq1": btn1Text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[0].SAT1; btn2Text = json.main[2].Q1[1].A1[2].SQ1[2].SA2[0]; break;
                case "ssq1": riddle.Visibility = Visibility.Visible; submitBtn.Visibility = Visibility.Visible; break;
            }
            if (GameText == "ssq1")
            {
                //do nothing
            }
            else
            {
                //showing elements and adding content to them
                btn1.Visibility = Visibility.Visible;
                btn2.Visibility = Visibility.Visible;
                btn1Display.Text = btn1Text;
                btn2Display.Text = btn2Text;
            }
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
            string titleText = "Chapter 7\nThe Littles";
            string p1Text = "\n\n\n" + json.main[0].P1;
            string p2Text = json.main[1].P2;
            string q1Text = json.main[2].Q1[0].QT1;
            string a1Text = json.main[2].Q1[1].A1[1].T1;
            string a2Text = json.main[2].Q1[2].A2[1];
            string sq1Text = json.main[2].Q1[1].A1[2].SQ1[0].SQT1;
            string sa1Text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[1].T2;
            string sa2Text = json.main[2].Q1[1].A1[2].SQ1[2].SA2[1];
            string ssq1Text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[2].SSQ1[0].SSQT1;
            string wText = json.main[2].Q1[1].A1[2].SQ1[1].SA1[2].SSQ1[1].W;
            string lText = json.main[2].Q1[1].A1[2].SQ1[1].SA1[2].SSQ1[2].L;
            //logic
            switch (GameText)
            {
                case "title": text = titleText;element = title; delay = 80; break;
                case "p1": text = p1Text; break;
                case "p2": text = p2Text; break;
                case "q1": text = q1Text; displayElements("q1"); break;
                case "a1": text = a1Text; hideElements("a1"); break;
                case "a2": text = a2Text; hideElements("a2"); break;
                case "sq1": text = sq1Text; displayElements("sq1"); break;
                case "sa1": text = sa1Text; hideElements("sa1"); break;
                case "sa2": text = sa2Text; hideElements("sa2"); break;
                case "ssq1": text = ssq1Text; break;
                case "w": text = wText; break;
                case "l": text = lText; break;
            }
            //displaying text
            foreach (char c in text)
            {
                element.Text += c;
                await Task.Delay(delay);
            }
            //displaying riddle ui elements after text is displayed
            if (GameText == "ssq1")
            {
                displayElements("ssq1");
            }
        }
        private async void gameTextDisplay()
        {
            if (count == 0)
            {
                if(btn1Click == true)
                {
                    if (subCount == 1)
                    {
                        //no need to check subSubCount. Display code continues in submitBtnclick
                        await displayText("sa1");
                        await displayText("ssq1");
                        subSubCount++;
                    }
                    else
                    {
                        await displayText("a1");
                        await displayText("sq1");
                        subCount++;
                    }
                }
                else if(btn2Click == true)
                {
                    if (subCount == 1)
                    {
                        await displayText("sa2");
                    }
                    else
                    {
                        await displayText("a2");
                        endOfChapter();
                    }
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
        private async void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int answer = int.Parse(riddle.Text);
                if (answer == 40)
                {
                    MainWindow window = (MainWindow)Window.GetWindow(this);
                    window.inventoryListBox.Items.Add("Violet Gem");
                    window.inventoryItems.Add("Violet Gem");
                    riddle.Visibility = Visibility.Collapsed;
                    submitBtn.Visibility = Visibility.Collapsed;
                    await displayText("w");
                    endOfChapter();
                }
                else
                {
                    await displayText("l");
                    endOfChapter();
                }
            }
            catch 
            {
                MessageBox.Show("Value entered must be a number!");
            } 
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
            window.MainFrame.Navigate(new Uri("Page12.xaml", UriKind.Relative));
            background.Opacity = 1;
        }

        private void riddle_GotFocus(object sender, RoutedEventArgs e)
        {
            riddle.Clear();
        }
    }
}
