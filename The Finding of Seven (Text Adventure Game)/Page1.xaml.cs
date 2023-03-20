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
            
            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            
            //setting scene
            background.Opacity = 0;
            displayBox.Opacity = 0;
            border.Opacity = 0;
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 05 Ruins.mp3";
            window.player.Source = new Uri(path, UriKind.Relative);
            while (background.Opacity < 1)
            {
                window.player.Volume += 0.01;
                background.Opacity += 0.01;
                await Task.Delay(40);
            }
            endOfChapter("onTheRoad");
            //fading in displayBox
            while (displayBox.Opacity < 1)
            {
                border.Opacity += 0.01;
                displayBox.Opacity += 0.01;
                await Task.Delay(40);
            }
            //seting MainFrame in MainWindow background to black
            
            window.MainFrame.Background = Brushes.Black;

            //adding json text to text box
            string titleText = "Chapter 1\nThe Beginning";
            
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
            //wait
            for (int i = 0; i < p1Text.Length; i++)
            {
                await Task.Delay(40);
            }
            q1();

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
            var json = JsonConvert.DeserializeObject<GameText>(jsonString); 

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[1].Q1[1].A1[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.GetGold(10, "It was just lying there on your table");
            
        }
        private async void a2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[1].Q1[2].A2[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p2Text = json.main[2].P2;
            
            
            string p3 = json.main[5].P3;
            //displaying p2
            foreach (char c in p2Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void oa1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string oa1Text = json.main[3].OA1;

            foreach (char c in oa1Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void oa2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string oa2 = json.main[4].OA2;

            foreach (char c in oa2)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p3 = json.main[5].P3;

            //displaying p3
            foreach (char c in p3)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void q2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string q2Text = json.main[6].Q2[0].QT2;

            //displaying q2
            foreach (char c in q2Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

            //displaying button options
            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[6].Q2[1].A3[0].AT1;
            btn2Display.Text = json.main[6].Q2[2].A4[0];
        }
        private async void a3()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a3Text = json.main[6].Q2[1].A3[1].T1;

            foreach (char c in a3Text)
            {
                display.Text += c;
                await Task.Delay(40);
            }

        }
        private void a4()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
        }
        private async void sq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a3Text = json.main[6].Q2[1].A3[2].SQ1[0].SQT1;

            foreach (char c in a3Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

            //displaying button options
            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[6].Q2[1].A3[2].SQ1[1].SA1[0];
            btn2Display.Text = json.main[6].Q2[1].A3[2].SQ1[2].SA2[0];
        }
        private void sa1()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
        }
        private void sa2()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
        }

        private async void p4()
        {
            
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p4Text = json.main[7].P4;

            //displaying p3
            foreach (char c in p4Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

            //take one damage from defeating the bandits
            MainWindow window = (MainWindow)Window.GetWindow(this);
            string text = "You don't appear to have come out unscathed from your encounter with the bandits";
            window.HeartDeath(text);
        }
        private async void q3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string q3Text = json.main[8].Q3[0].QT3;

            //displaying p3
            foreach (char c in q3Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

            //displaying button options
            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[8].Q3[1].A5[0];
            btn2Display.Text = json.main[8].Q3[2].A6[0];
        }
        private async void a5()
        {
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a5Text = json.main[8].Q3[1].A5[1];

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
            string a6Text = json.main[8].Q3[2].A6[1];

            foreach (char c in a6Text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

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
            public string P1,P2,P3,P4;
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
        private async void gameTextDisplay()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a2Text = json.main[1].Q1[2].A2[1];
            string p2Text = json.main[2].P2;
            string oa2Text = json.main[4].OA2;
            string p3Text = json.main[5].P3;
            string p4Text = json.main[7].P4;
            string a5Text = json.main[8].Q3[1].A5[1];
            string a6Text = json.main[8].Q3[2].A6[1];
            string a1Text = json.main[1].Q1[1].A1[1];
            string oa1Text = json.main[3].OA1;
            string a3Text = json.main[6].Q2[1].A3[1].T1;

            //answer for q1
            if (count == 0)
            {
                //btn1 - yes
                if (btn1Click == true)
                {
                    a1();
                    for (int i = 0; i < a1Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    p2();
                    for (int i = 0; i < p2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    oa1();
                    for (int i = 0; i < oa1Text.Length; i++)
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
                //btn2 - no
                else if (btn2Click == true)
                {
                    a2();
                    for (int i = 0; i < a2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    p2();
                    for (int i = 0; i < p2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    oa2();
                    for (int i = 0; i < oa2Text.Length; i++)
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
                //btn1 - threaten with horse
                if (btn1Click == true)
                {
                    //sub question
                    if (subCount == 0)
                    {
                        a3();
                        for (int i = 0; i < a3Text.Length; i++)
                        {
                            await Task.Delay(40);
                        }
                        sq1();
                        subCount++;
                    }

                    if (btn1Click == true)
                    {
                        sa1();
                        endOfChapter("flee");
                        count = -1;
                    }
                }
                //btn2 - attack
                else if (btn2Click == true)
                {
                    a4();
                    p4();
                    for (int i = 0; i < p4Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    q3();
                    count++;

                }
            }
            //answering q3
            else if (count == 2)
            {
                //btn1
                if (btn1Click == true)
                {
                    a5();
                    for (int i = 0; i < a5Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    endOfChapter("onTheRoad");
                }
                //btn2
                else if (btn2Click == true)
                {
                    a6();
                    for (int i = 0; i < a6Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
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
