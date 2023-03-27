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
            
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 69 For the Fans.mp3";
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
            string titleText = "Chapter 6\nThe Drawbridge";
            //displaying title
            foreach (char c in titleText)
            {
                title.Text += c;
                await Task.Delay(80);

            }
            //displaying p1 and q1
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p1Text = "\n\n\n" + json.main[0].P1;

            //calling the methods for text and pausing it
            p1();
            for (int i = 0; i < p1Text.Length; i++)
            {
                await Task.Delay(45);
            }
            await Task.Delay(1000);
            q1();
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
        private async void gameTextDisplay()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a1Text = json.main[1].Q1[1].A1[1].T1;
            string a2Text = json.main[1].Q1[2].A2[1];
            string pText = json.main[2].P2;
            string a3Text = json.main[3].Q2[1].A3[1].T2;
            string sa3Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[1].T3;
            string ssa1Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[2].SSQ1[1].SSA1[1];

            if (count == 0)
            {
                if(btn1Click == true)
                {
                    a1();
                    for (int i = 0; i < a1Text.Length; i++)
                    {
                        await Task.Delay(45);
                    }
                    r();
                    //rest of code in submitBtn click event
                }
                else if (btn2Click == true)
                {
                    a2();
                    for (int i = 0; i < a2Text.Length; i++)
                    {
                        await Task.Delay(45);
                    }
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
                            ssa1();
                            for (int i = 0; i < ssa1Text.Length; i++)
                            {
                                await Task.Delay(45);
                            }
                            youDied();

                        }
                        else
                        {
                            sa3();
                            for (int i = 0; i < sa3Text.Length; i++)
                            {
                                await Task.Delay(45);
                            }
                            ssq1();
                            subSubCount++;
                        }
                    }
                    else
                    {
                        a3();
                        for (int i = 0; i < a3Text.Length; i++)
                        {
                            await Task.Delay(45);
                        }
                        sq1();
                        subCount++;
                    }
                    
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
                await Task.Delay(45);

            }
        }
        private async void q1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].Q1[0].QT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[1].Q1[1].A1[0].AT1;
            btn2Display.Text = json.main[1].Q1[2].A2[0];
        }
        private async void a1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].Q1[1].A1[1].T1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void a2()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].Q1[2].A2[1];

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void r()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].Q1[1].A1[2].R;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }

            riddle.Visibility = Visibility.Visible;
            submitBtn.Visibility = Visibility.Visible;
            riddle.Opacity = 0;
            submitBtn.Opacity = 0;
            //fading in ui elements
            for (double i = 0; i < 1; i += 0.01)
            {
                await Task.Delay(45);
                riddle.Opacity += 0.01;
                submitBtn.Opacity += 0.01;
            }
        }
        private async void w()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].Q1[1].A1[3].W;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void l()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[1].Q1[1].A1[4].L;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void p2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[2].P2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void q2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[3].Q2[0].QT2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            
            btn1Display.Text = json.main[3].Q2[1].A3[0].AT3;
            dreamLabel.Content = json.main[3].Q2[2].A4[0];
        }
        private async void a3()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[3].Q2[1].A3[1].T2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void sq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[3].Q2[1].A3[2].SQ1[0].SQT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[0].SAT1;
            //displaying input box
            dreamLabel.Content = json.main[3].Q2[1].A3[2].SQ1[2].SA2[0];
            dreamLabel.Visibility = Visibility.Visible;
            dreamTextBox.Visibility = Visibility.Visible;
            submitBtnDream.Visibility = Visibility.Visible;
        }
        private async void sa3()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            //hiding text input
            dreamLabel.Visibility = Visibility.Collapsed;
            dreamTextBox.Visibility = Visibility.Collapsed;
            submitBtnDream.Visibility = Visibility.Collapsed;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[1].T3;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void ssq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[2].SSQ1[0].SSQT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[2].SSQ1[1].SSA1[0];
            //displaying input box
            dreamLabel.Content = json.main[3].Q2[1].A3[2].SQ1[2].SA2[0];
            dreamLabel.Visibility = Visibility.Visible;
            dreamTextBox.Visibility = Visibility.Visible;
            submitBtnDream.Visibility = Visibility.Visible;
        }
        private async void ssa1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            //hiding text input
            dreamLabel.Visibility = Visibility.Collapsed;
            dreamTextBox.Visibility = Visibility.Collapsed;
            submitBtnDream.Visibility = Visibility.Collapsed;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[3].Q2[1].A3[2].SQ1[1].SA1[2].SSQ1[1].SSA1[1];

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void p3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[4].P3;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(45);

            }
        }
        private async void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            riddle.Visibility = Visibility.Collapsed;
            submitBtn.Visibility = Visibility.Collapsed;
            MainWindow window = (MainWindow)Window.GetWindow(this);
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string wText = json.main[1].Q1[1].A1[3].W;
            string lText = json.main[1].Q1[1].A1[4].L;
            string p2Text = json.main[2].P2;

            if (riddle.Text.ToLower() == "sapphire")
            {
                w();
                for (int i = 0; i < wText.Length; i++)
                {
                    await Task.Delay(45);
                }
                window.inventoryListBox.Items.Add("Blue Gem");
                window.inventoryItems.Add("Blue Gem");
            }
            else
            {
                l();
                for (int i = 0; i < lText.Length; i++)
                {
                    await Task.Delay(45);
                }
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
            p2();
            for (int i = 0; i < p2Text.Length; i++)
            {
                await Task.Delay(45);
            }
            q2();
            count++;
            dreamLabel.Visibility = Visibility.Visible;
            dreamTextBox.Visibility = Visibility.Visible;
            submitBtnDream.Visibility = Visibility.Visible;
        }

        private void riddle_GotFocus(object sender, RoutedEventArgs e)
        {
            riddle.Clear();
        }

        private async void submitBtnDream_Click(object sender, RoutedEventArgs e)
        {
            submitBtnDream.Visibility = Visibility.Collapsed;
            dreamLabel.Visibility = Visibility.Collapsed;
            dreamTextBox.Visibility= Visibility.Collapsed;
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p3Text = json.main[4].P3;

            //continue display
            if (dreamTextBox.Text.ToLower().Contains("dream"))
            {
                p3();
                for (int i = 0; i < p3Text.Length; i++)
                {
                    await Task.Delay(45);
                }
                endOfChapter();
            }
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
