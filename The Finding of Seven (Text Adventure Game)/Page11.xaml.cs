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
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 22 Snowdin Town.mp3";
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
            //adding json text to text box
            string titleText = "Chapter 7\nThe Littles";
            //displaying title
            foreach (char c in titleText)
            {
                title.Text += c;
                await Task.Delay(80);

            }

            //calling the methods for text and pausing it
            await p1();
            await p2();
            q1();
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
        private async void gameTextDisplay()
        {
            if (count == 0)
            {
                if(btn1Click == true)
                {
                    if (subCount == 1)
                    {
                        //no need to check subSubCount. Display code continues in submitBtnclick
                        await sa1();
                        ssq1();
                        subSubCount++;
                    }
                    else
                    {
                        await a1();
                        sq1();
                        subCount++;
                    }
                    
                }
                else if(btn2Click == true)
                {
                    if (subCount == 1)
                    {
                        await sa2();
                    }
                    else
                    {
                        await a2();
                        endOfChapter();
                    }
                }
            }
        }
        private async Task p1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = "\n\n\n" + json.main[0].P1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async Task p2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].P2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void q1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[0].QT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[2].Q1[1].A1[0].AT1;
            btn2Display.Text = json.main[2].Q1[2].A2[0];
        }
        private async Task a1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[1].T1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async Task a2()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[2].A2[1];

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void sq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[2].SQ1[0].SQT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[0].SAT1;
            btn2Display.Text = json.main[2].Q1[1].A1[2].SQ1[2].SA2[0];
        }
        private async Task sa1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[1].T2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async Task sa2()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[2].SQ1[2].SA2[1];

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void ssq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[2].SSQ1[0].SSQT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
            }
            //displaying riddle ui
            riddle.Visibility = Visibility.Visible;
            submitBtn.Visibility = Visibility.Visible;
        }
        private async Task w()
        {
            //hiding riddle ui
            riddle.Visibility = Visibility.Collapsed;
            submitBtn.Visibility = Visibility.Collapsed;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[2].SSQ1[1].W;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
            }
            
        }
        private async Task l()
        {
            //hiding riddle ui
            riddle.Visibility = Visibility.Collapsed;
            submitBtn.Visibility = Visibility.Collapsed;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[2].SSQ1[2].L;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
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
                    await w();
                    endOfChapter();
                }
                else
                {
                    await l();
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
