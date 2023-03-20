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
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn1Click = true;
        }

        private void displayBox_LayoutUpdated(object sender, EventArgs e)
        {
            scroll.ScrollToBottom();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Resources\music\toby fox - UNDERTALE Soundtrack - 69 For the Fans.mp3
            MainWindow window = (MainWindow)Window.GetWindow(this);

            //collapsing btns
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;

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
                await Task.Delay(40);
            }
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
            public string P1, P2, P3, QT1, T1, W, L, QT2, AT3, T2, SQT1, SAT1, T3, SSQT1;
            public string[] A2;
            public string[] SSA1;
            public string[] SA2;
            public string[] A4;


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
            btn1Display.Text = "";
            btn2Display.Text = "";
        }
    }
}
