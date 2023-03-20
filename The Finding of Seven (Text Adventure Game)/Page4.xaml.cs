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
using System.Security.Cryptography;
using System.Runtime.Remoting.Contexts;
using static System.Console;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{

    /// <summary>
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public bool btn1Click { get; set; }
        public bool btn2Click { get; set; }

        public int count = 0;
        public int subCount = 0;
        public int subSubCount = 0;
        public bool hideBtnForQ5 = false;

        //adding json text to text box
        public static string fileName = @"Resources\text\movingVillage.json";
        public static string jsonString = File.ReadAllText(fileName);

        public Page4()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //buttons collapsed
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;

            //setting scene
            background.Opacity = 0;
            displayBox.Opacity = 0;
            border.Opacity = 0;
            string path = @"Resources\music\toby fox - UNDERTALE Soundtrack - 51 Another Medium.mp3";
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
           
            if (window.fightEncountered == true)
            {
                continueGameTextDisplayAfterFight();
            }
            else
            {
                //adding json text to text box
                string titleText = "Chapter 3\nThe Moving Village";

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
                await Task.Delay(50 * p1Text.Length);
                q1();

            }
            
            
        }
        public class GameText
        {
            public List<GameText> main = new List<GameText>();
            public string P1, P2, P3, P4, P5, P6, P7, P8;
            public string AT1, SAT1, SSQT1, AT7, W;
            public string SQT1, SQT2;
            public string T1, T2, T3, T4, T5, T6;
            public List<GameText> OA1 = new List<GameText>();
            public List<GameText> OA2 = new List<GameText>();
            public List<GameText> SQ1 = new List<GameText>();
            public List<GameText> SQ2 = new List<GameText>();
            public List<GameText> SA1 = new List<GameText>();
            public List<GameText> SSQ1 = new List<GameText>();
            public string[] SA2;
            public string[] SA3;
            public string[] SA4;
            public string[] SSA2;
            public string[] SSA1;
            public List<GameText> Q1 = new List<GameText>();
            public List<GameText> Q2 = new List<GameText>();
            public List<GameText> Q3 = new List<GameText>();
            public List<GameText> Q4 = new List<GameText>();
            public List<GameText> Q5 = new List<GameText>();
            public string QT1, QT2, QT3, QT4, QT5;
            public string[] A1;
            public string[] A2;
            public List<GameText> A5 = new List<GameText>();
            public string[] A4;
            public string[] A3;
            public List<GameText> A6 = new List<GameText>();
            public List<GameText> A7 = new List<GameText>();
            public string[] A8;
            public string[] A9;
            public string[] A10;


        }
        private async void continueGameTextDisplayAfterFight()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string p7Text = json.main[9].P7;
            string a7Text = json.main[10].Q4[1].A7[1].T6;
            string a8Text = json.main[10].Q4[2].A8[1];
            string sa3Text = json.main[10].Q4[1].A7[2].SQ2[1].SA3[1];
            string sa4Text = json.main[10].Q4[1].A7[2].SQ2[2].SA4[1];
            string p8Text = json.main[11].P8;
            string a9Text = json.main[12].Q5[1].A9[1];
            string a10Text = json.main[12].Q5[2].A10[1];
            string winText = json.main[8].Q3[2].A6[2].W;
            count = 2;

            //display win text if successful and then continue
            w();
            for (int i = 0; i < winText.Length; i++)
            {
                await Task.Delay(40);

            }
            p7();
            for (int i = 0; i < p7Text.Length; i++)
            {
                await Task.Delay(40);

            }
            q4();
            count++;

            //continuing with display
            if (count == 3)
            {
                if (btn1Click == true)
                {
                    if (subCount == 2)
                    {
                        sa3();
                        for (int i = 0; i < sa3Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        p8();
                        for (int i = 0; i < p8Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        q5();
                        count++;
                    }
                    else
                    {
                        a7();
                        for (int i = 0; i < a7Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        sq2();
                        subCount++;
                    }

                }
                else if (btn2Click == true)
                {
                    if (subCount == 2)
                    {
                        sa4();
                        for (int i = 0; i < sa4Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        p8();
                        for (int i = 0; i < p8Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        q5();
                        count++;
                    }
                    else
                    {
                        a8();
                        for (int i = 0; i < a8Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        //hide button for q5
                        hideBtnForQ5 = true;
                        p8();
                        for (int i = 0; i < p8Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        q5();
                        count++;
                    }


                }
            }
            else if (count == 4)
            {
                if (btn1Click == true)
                {
                    a9();
                }
                else if (btn2Click == true)
                {
                    a10();
                }
            }
        }
    

        private async void gameTextDisplay()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string a1Text = json.main[1].Q1[1].A1[1];
            string a2Text = json.main[1].Q1[2].A2[1];
            string p2Text = json.main[2].P2;
            string p3Text = json.main[3].P3;
            string p4Text = json.main[4].P4;
            string p5Text = json.main[5].P5;
            string a3Text = json.main[6].Q2[1].A3[1];
            string a4Text = json.main[6].Q2[2].A4[1];
            string p6Text = json.main[7].P6;
            string a5Text = json.main[8].Q3[1].A5[1].T2;
            string a6Text = json.main[8].Q3[2].A6[1].T5;
            string sa1Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[1].T1;
            string sa2Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[2].SA2[1];
            string ssa1Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[1].SSA1[1];
            string ssa2Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[2].SSA2[1];
            string p7Text = json.main[9].P7;
            string a7Text = json.main[10].Q4[1].A7[1].T6;
            string a8Text = json.main[10].Q4[2].A8[1];
            string sa3Text = json.main[10].Q4[1].A7[2].SQ2[1].SA3[1];
            string sa4Text = json.main[10].Q4[1].A7[2].SQ2[2].SA4[1];
            string p8Text = json.main[11].P8;
            string a9Text = json.main[12].Q5[1].A9[1];
            string a10Text = json.main[12].Q5[2].A10[1];

            if (count == 0)
            {
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
                    p3();
                    for (int i = 0; i < p3Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    p4();
                    for (int i = 0; i < p4Text.Length; i++)
                    {
                        await Task.Delay(40);
                        
                    }
                    p5();
                    for (int i = 0; i < p5Text.Length; i++)
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
                    p2();
                    for (int i = 0; i < p2Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    p3();
                    for (int i = 0; i < p3Text.Length; i++)
                    {
                        await Task.Delay(40);
                    }
                    p4();
                    for (int i = 0; i < p4Text.Length; i++)
                    {
                        await Task.Delay(40);

                    }
                    p5();
                    for (int i = 0; i < p5Text.Length; i++)
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
                    p6();
                    for (int i = 0; i < p6Text.Length; i++)
                    {
                        await Task.Delay(40);

                    }
                    q3();
                    count++;
                }
                else if (btn2Click == true)
                {
                    a4();
                    for (int i = 0; i < a4Text.Length; i++)
                    {
                        await Task.Delay(40);

                    }
                    p6();
                    for (int i = 0; i < p6Text.Length; i++)
                    {
                        await Task.Delay(40);

                    }
                    q3();
                    count++;

                }
            }
            else if (count == 2)
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
                                await Task.Delay(40);

                            }
                            p7();
                            for (int i = 0; i < p7Text.Length; i++)
                            {
                                await Task.Delay(40);

                            }
                            q4();
                            count++;
                        }
                        else
                        {
                            sa1();
                            for (int i = 0; i < sa1Text.Length; i++)
                            {
                                await Task.Delay(40);

                            }
                            ssq1();
                            subSubCount++;
                        }
                        
                    }
                    else
                    {
                        a5();
                        for (int i = 0; i < a5Text.Length; i++)
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
                        if (subSubCount == 1)
                        {
                            ssa2();
                            for (int i = 0; i < ssa2Text.Length; i++)
                            {
                                await Task.Delay(40);

                            }
                            p7();
                            for (int i = 0; i < p7Text.Length; i++)
                            {
                                await Task.Delay(40);

                            }
                            q4();
                            count++;
                        }
                        else
                        {
                            sa2();
                            for (int i = 0; i < sa2Text.Length; i++)
                            {
                                await Task.Delay(40);

                            }
                            //jump to p7
                            p7();
                            for (int i = 0; i < p7Text.Length; i++)
                            {
                                await Task.Delay(40);

                            }
                            q4();
                            count++;
                        }

                    }
                    else
                    {
                        a6();
                        for (int i = 0; i < a6Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        //enter fight scene here for man that attacks you
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

                        continueGameTextDisplayAfterFight();
                    }

                }
            }
            else if (count == 3)
            {
                if (btn1Click == true)
                {
                    if (subCount == 2)
                    {
                        sa3();
                        for (int i = 0; i < sa3Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        p8();
                        for (int i = 0; i < p8Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        q5();
                        count++;
                    }
                    else
                    {
                        a7();
                        for (int i = 0; i < a7Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        sq2();
                        subCount++;
                    }
                    
                }
                else if (btn2Click == true)
                {
                    if (subCount == 2)
                    {
                        sa4();
                        for (int i = 0; i < sa4Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        p8();
                        for (int i = 0; i < p8Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        q5();
                        count++;
                    }
                    else
                    {
                        a8();
                        for (int i = 0; i < a8Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        //hide button for q5
                        hideBtnForQ5 = true;
                        p8();
                        for (int i = 0; i < p8Text.Length; i++)
                        {
                            await Task.Delay(40);

                        }
                        q5();
                        count++;
                    }
                    

                }
            }
            else if (count == 4)
            {
                if (btn1Click == true)
                {
                    a9();
                }
                else if (btn2Click == true)
                {
                    a10();
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
            string text = json.main[2].P2;

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
        private async void p4()
        {

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text =json.main[4].P4;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

        }
        private async void p5()
        {

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[5].P5;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

        }
        private async void q2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[6].Q2[0].QT2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[6].Q2[1].A3[0];
            btn2Display.Text = json.main[6].Q2[2].A4[0];


        }
        private async void a3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[6].Q2[1].A3[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void a4()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[6].Q2[2].A4[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p6()
        {

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[7].P6;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

        }
        private async void q3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[8].Q3[0].QT3;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[8].Q3[1].A5[0].T1;
            btn2Display.Text = json.main[8].Q3[2].A6[0].T4;


        }
        private async void a5()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[8].Q3[1].A5[1].T2;
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void a6()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[8].Q3[2].A6[1].T5;
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void sq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[0].SQT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[0].SAT1;
            btn2Display.Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[2].SA2[0];


        }
        private async void sa1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[1].T1;
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void sa2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[8].Q3[1].A5[2].OA1[0].SQ1[2].SA2[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void ssq1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[0].SSQT1;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[1].SSA1[0];
            btn2Display.Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[2].SSA2[0];


        }
        private async void ssa1()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[1].SSA1[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void ssa2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[2].SSA2[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p7()
        {

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[9].P7;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

        }
        private async void w()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string winText = json.main[8].Q3[2].A6[2].W;

            foreach (char c in winText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void q4()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[10].Q4[0].QT4;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[10].Q4[1].A7[0].AT7;
            btn2Display.Text = json.main[10].Q4[2].A8[0];


        }
        private async void a7()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[10].Q4[1].A7[1].T6;
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void a8()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[10].Q4[2].A8[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void sq2()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[10].Q4[1].A7[2].SQ2[0].SQT2;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[10].Q4[1].A7[2].SQ2[1].SA3[0];
            btn2Display.Text = json.main[10].Q4[1].A7[2].SQ2[2].SA4[0];


        }
        private async void sa3()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[10].Q4[1].A7[2].SQ2[1].SA3[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }

        private async void sa4()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[10].Q4[1].A7[2].SQ2[2].SA4[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void p8()
        {

            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[11].P8;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);

            }

        }
        private async void q5()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string text = json.main[12].Q5[0].QT5;

            foreach (char c in text)
            {
                display.Text += c;
                await Task.Delay(40);
            }
            //displaying buttons with content

            if (hideBtnForQ5 == true)
            {
                //do nothing
            }
            else
            {
                btn1.Visibility = Visibility.Visible;
                
            }
            btn2.Visibility = Visibility.Visible;
            btn1Display.Text = json.main[12].Q5[1].A9[0];
            btn2Display.Text = json.main[12].Q5[2].A10[0];


        }
        private async void a9()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[12].Q5[1].A9[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
        }
        private async void a10()
        {
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            MainWindow window = (MainWindow)Window.GetWindow(this);
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
            btn1Click = false;
            btn2Click = false;
            string btnText = json.main[12].Q5[2].A10[1];
            foreach (char c in btnText)
            {
                display.Text += c;
                await Task.Delay(40);

            }
            //adding orange gem to inventory as this is the only case the player can walk out with the gem
            window.inventoryItems.Add("Orange Gem");
        }
        private async void endOfChapter()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);

            window.GetGold(20, "You found some Gold on the border of the moving village.");
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
