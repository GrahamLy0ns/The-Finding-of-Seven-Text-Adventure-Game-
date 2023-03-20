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
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 24 Bonetrousle.mp3";
            window.player.Source = new Uri(path, UriKind.Relative);
            while (background.Opacity < 1)
            {
                window.player.Volume += 0.01;
                background.Opacity += 0.01;
                await Task.Delay(40);
            }
            endOfChapter();
            //fading in displayBox
            while (displayBox.Opacity < 1)
            {
                border.Opacity += 0.01;
                displayBox.Opacity += 0.01;
                await Task.Delay(40);
            }
            //adding json text to text box
            string titleText = "Chapter 2\nOn the Road";

            //displaying title
            foreach (char c in titleText)
            {
                title.Text += c;
                await Task.Delay(80);

            }
            await Task.Delay(1000);
            
            //displaying p1 and q1
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p1Text = "\n\n\n" + json.main[0].P1;
            
            //calling the methods for text and pausing it
            p1();
            for (int i = 0; i < p1Text.Length; i++)
            {
                await Task.Delay(40);
            }
            q1();
        }

        public class GameText
        {
            public List<GameText> main = new List<GameText>();
            public string P1, P2, P3, P4;
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
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
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

            if (count == 0)
            {
                if (btn1Click == true)
                {
                    a1();
                    for (int i = 0; i < a1Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    //display troll
                    displayTroll();
                    await Task.Delay(12000);
                    p2();
                    for (int i = 0; i < p2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    q2();
                    count++;
                }
                else if (btn2Click == true)
                {
                    MainWindow window = (MainWindow)Window.GetWindow(this);
                    string text = "The Troll's rock like arm seems to have wounded you.";
                    window.HeartDeath(text);
                    a2();
                    for (int i = 0; i < a2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    //display troll
                    displayTroll();
                    await Task.Delay(12000);
                    p2();
                    for (int i = 0; i < p2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    q2();
                    count++;
                }
            }
            else if (count == 1)
            {
                if (btn1Click == true)
                {
                    a3();
                    for (int i = 0; i < a3Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    youDied();
                }
                else if (btn2Click == true || submitBtnClicked == true)
                {
                    if (submitBtnClicked == false)
                    {
                        a4();
                        for (int i = 0; i < a4Text.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        riddleReveal();
                    }
                    if (riddleCorrect == true)
                    {
                        w();
                        for (int i = 0; i < wText.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        count++;
                    }
                    else if(riddleCorrect == false && submitBtnClicked == true)
                    {
                        l();
                        for (int i = 0; i < lText.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        youDied();
                    }
                }
                
            }

            if (count == 2)
            {
                p3();
                for (int i = 0; i < p3Text.Length; i++)
                {
                    await Task.Delay(40);
                }
                q3();
                count++;
            }
            else if (count == 3)
            {
                if (btn1Click == true)
                {
                    a5();
                    for (int i = 0; i < a5Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    endOfChapter();
                }
                else if (btn2Click == true)
                {
                    a6();
                    for (int i = 0; i < a6Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    endOfChapter();
                }
            }
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
        private async void q1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].Q1[0].QT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[1].Q1[1].A1[0];
            btn2Display.Text = json.main[1].Q1[2].A2[0];
        }
        private async void a1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a1Text = json.main[1].Q1[1].A1[1];

            foreach (char c in a1Text)
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
            string a2Text = json.main[1].Q1[2].A2[1];

            foreach (char c in a2Text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            
        }
        private async void p2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p2Text = json.main[2].P2;

            foreach (char c in p2Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void q2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string q2Text = json.main[3].Q2[0].QT2;

            foreach (char c in q2Text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[3].Q2[1].A3[0];
            btn2Display.Text = json.main[3].Q2[2].A4[0].AT4;
        }
        private async void a3()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a3Text = json.main[3].Q2[1].A3[1];

            foreach (char c in a3Text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
        }
        private async void a4()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a4Text = json.main[3].Q2[2].A4[1].T1;

            foreach (char c in a4Text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
        }
        private async void w()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string wText = json.main[3].Q2[2].A4[2].W;

            foreach (char c in wText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void l()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string lText = json.main[3].Q2[2].A4[3].L;

            foreach (char c in lText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p3Text = json.main[4].P3;

            foreach (char c in p3Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void q3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string q3Text = json.main[5].Q3[0].QT3;

            foreach (char c in q3Text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[5].Q3[1].A5[0];
            btn2Display.Text = json.main[5].Q3[2].A6[0];
        }
        private async void a5()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a5Text = json.main[5].Q3[1].A5[1];

            foreach (char c in a5Text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
        }
        private async void a6()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a6Text = json.main[5].Q3[2].A6[1];
            

            foreach (char c in a6Text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
        }
        private async void displayTroll()
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
            await Task.Delay(8000);

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
        private async void endOfChapter()
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

            window.MainFrame.Navigate(new Uri("Page3.xaml", UriKind.Relative));

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
            if (riddle.Text.Contains("candle"))
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
