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
            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;

            //setting scene
            background.Opacity = 0;
            displayBox.Opacity = 0;
            border.Opacity = 0;
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 81 An Ending.mp3";
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
            string titleText = "Flee";

            //displaying title
            foreach (char c in titleText)
            {
                title.Text += c;
                await Task.Delay(80);

            }
            
            //displaying p1 and q1
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p1Text = "\n\n\n" + json.main[0].P1;
            string p2Text = "\n\n\n" + json.main[1].P2;

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
            btn1Display.Text = json.main[2].Q1[1].A1[0];
            btn2Display.Text = json.main[2].Q1[2].A2[0];
        }
        private async void a1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a1Text = json.main[2].Q1[1].A1[1];

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
            string a2Text = json.main[2].Q1[2].A2[1];

            foreach (char c in a2Text)
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
        private async void q2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[4].Q2[0].QT2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[4].Q2[1].A3[0];
            btn2Display.Text = json.main[4].Q2[2].A4[0].T1;
        }
        private async void a3()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a3Text = json.main[4].Q2[1].A3[1];

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
            string a4Text = json.main[4].Q2[2].A4[1].T2;
            
            foreach (char c in a4Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

        }
        private async void sq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[4].Q2[2].A4[2].SQ1[0].SQT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[4].Q2[2].A4[2].SQ1[1].SA1[0];
            btn2Display.Text = json.main[4].Q2[2].A4[2].SQ1[2].SA2[0];
        }
        private async void sa1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string sa1Text = json.main[4].Q2[2].A4[2].SQ1[1].SA1[1];

            foreach (char c in sa1Text)
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
            string sa2Text = json.main[4].Q2[2].A4[2].SQ1[2].SA2[1];

            foreach (char c in sa2Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

        }
        private async void SceneChange()
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
            Task t = Task.Run(() => {
                Task.Delay(50 * 40);
                
            });
            t.Wait();

            
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
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a1Text = json.main[2].Q1[1].A1[1];
            string a2Text = json.main[2].Q1[2].A2[1];
            string p3Text = json.main[3].P3;
            string a3Text = json.main[4].Q2[1].A3[1];
            string a4Text = json.main[4].Q2[2].A4[1].T2;
            string sa1Text = json.main[4].Q2[2].A4[2].SQ1[1].SA1[1];
            string sa2Text = json.main[4].Q2[2].A4[2].SQ1[2].SA2[1];

            if (count == 0)
            {
                //options for q1
                if (btn1Click == true)
                {
                    a1();
                    for (int i = 0; i < a1Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    p3();
                    for (int i = 0; i < p3Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    q2();
                    count++;
                }
                else if (btn2Click == true)
                {
                    a2();
                    for (int i = 0; i < a2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    p3();
                    for (int i = 0; i < p3Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    q2();
                    count++;
                }
            }
            //answering q2
            else if (count == 1)
            {
                if (btn1Click == true)
                {
                    a3();
                    for (int i = 0; i < a3Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    endOfChapter();
                }
                else if (btn2Click == true)
                {
                    a4();
                    SceneChange();
                    for (int i = 0; i < a4Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    sq1();
                    count++;
                }

            }
            else if (count == 2)
            {
                if (btn1Click == true)
                {
                    sa1();
                    for (int i = 0; i < sa1Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    youDied();
                }
                else if(btn2Click == true)
                {
                    sa2();
                    for (int i = 0; i < sa2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    youDied();
                }
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
            string endChapterText = "\n\nEND OF CHAPTER";

            //displaying endOfChapter text
            foreach (char c in endChapterText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
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
