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
                continueWithGameTextDisplay();
            }
            else
            {
                //adding json text to text box
                string titleText = "Chapter 4\nBandits on the Road";
                //displaying title
                foreach (char c in titleText)
                {
                    title.Text += c;
                    await Task.Delay(80);

                }
                //displaying p1 and q1
                var json = JsonConvert.DeserializeObject<GameText>(jsonString);
                string p1Text = "\n\n\n" + json.main[0].P1;
                string p2Text = json.main[1].P2;

                //calling the methods for text and pausing it
                p1();
                for (int i = 0; i < p1Text.Length; i++)
                {
                    await Task.Delay(40);
                }
                p2();
                for (int i = 0; i < p2Text.Length; i++)
                {
                    await Task.Delay(40);
                }
                q1();
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
        private async void gameTextDisplay()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a1Text = json.main[2].Q1[1].A1[1].T2;
            string sa1Text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[1];
            string sa3Text = json.main[2].Q1[1].A1[3].SQ2[1].SA3[1];
            string p3Text = json.main[3].P3;
            


            if (count == 0)
            {
                if (btn1Click == true)
                {
                    if (subCount == 1)
                    {
                        sa1();
                        for (int i = 0; i < sa1Text.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        sq2();
                        subCount++;
                    }
                    else if (subCount == 2)
                    {
                        sa3();
                        for (int i = 0; i < sa3Text.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        count++;
                    }
                    else
                    {
                        a1();
                        for (int i = 0; i < a1Text.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        sq1();
                        subCount++;
                    }

                }
                else if (btn2Click == true)
                {
                    if (subCount == 1)
                    {
                        sa2();
                        for (int i = 0; i < sa1Text.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        sq2();
                        subCount++;
                    }
                    else if (subCount == 2)
                    {
                        sa4();
                        for (int i = 0; i < sa3Text.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        count++;
                    }
                    else
                    {
                        a2();
                        for (int i = 0; i < a1Text.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        count++;
                    }
                }
            }

            if (count == 1)
            {
                p3();
                for (int i = 0; i < p3Text.Length; i++)
                {
                    await Task.Delay(40);
                }
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
                continueWithGameTextDisplay();


            }
        }
        private async void continueWithGameTextDisplay()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string wText = json.main[4].W;
            string p4Text = json.main[6].P4;
            w();
            for (int i = 0; i < wText.Length; i++)
            {
                await Task.Delay(40);
            }
            p4();
            for (int i = 0; i < p4Text.Length; i++)
            {
                await Task.Delay(40);
            }
            endOfChapter();
        }
        private async void endOfChapter()
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

            window.MainFrame.Navigate(new Uri("Page9.xaml", UriKind.Relative));

            background.Opacity = 1;


        }
        private async void p1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = "\n\n\n" + json.main[0].P1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].P2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void q1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[0].QT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[2].Q1[1].A1[0].AT1;
            btn2Display.Text = json.main[2].Q1[2].A2[0];
        }
        private async void a1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[1].T2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void a2()
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
                await Task.Delay(40);

            }
        }
        private async void sq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[2].SQ1[0].SQT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[0];
            btn2Display.Text = json.main[2].Q1[1].A1[2].SQ1[2].SA2[0];
        }
        private async void sa1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[2].SQ1[1].SA1[1];

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void sa2()
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
                await Task.Delay(40);

            }
        }
        private async void sq2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[3].SQ2[0].SQT2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[2].Q1[1].A1[3].SQ2[1].SA3[0];
            btn2Display.Text = json.main[2].Q1[1].A1[3].SQ2[2].SA4[0];
        }

        private async void sa3()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[3].SQ2[1].SA3[1];

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void sa4()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].Q1[1].A1[3].SQ2[2].SA4[1];

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[3].P3;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void w()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[4].W;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p4()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[6].P4;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
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
