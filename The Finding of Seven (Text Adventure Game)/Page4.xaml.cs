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
            var imageQuery = from a in window.db.ImageTBLs
                             where a.ImageName == "movingVillage"
                             select a.ImageSrc;
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "anotherMedium"
                             select a.SoundSrc;
            string path = soundQuery.ToList()[0];
            string imageSrc = imageQuery.ToList()[0];
            background.Background = new ImageBrush(new BitmapImage(new Uri(imageSrc, UriKind.Relative)));
            //buttons collapsed
            btn1.Visibility = Visibility.Collapsed;
            btn2.Visibility = Visibility.Collapsed;
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
            if (window.fightEncountered == true)
            {
                continueGameTextDisplayAfterFight();
            }
            else
            {
                //adding json text to text box
                await displayText("title");
                await displayText("p1");
                await displayText("q1");
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
            count = 2;
            subCount = 1;
            //display win text if successful and then continue
            await displayText("w");
            await displayText("p7");
            await displayText("q4");
            count++;
            //continuing with display
            if (count == 3)
            {
                if (btn1Click == true)
                {
                    if (subCount == 2)
                    {
                        await displayText("sa3");
                        await displayText("p8");
                        await displayText("q5");
                        count++;
                    }
                    else
                    {
                        await displayText("a7");
                        await displayText("sq2");
                        subCount++;
                    }
                }
                else if (btn2Click == true)
                {
                    if (subCount == 2)
                    {
                        await displayText("sa4");
                        await displayText("p8");
                        await displayText("q5");
                        count++;
                    }
                    else
                    {
                        await displayText("a8");
                        //hide button for q5
                        hideBtnForQ5 = true;
                        await displayText("p8");
                        await displayText("q5");
                        count++;
                    }
                }
            }
            else if (count == 4)
            {
                if (btn1Click == true)
                {
                    await displayText("a9");
                }
                else if (btn2Click == true)
                {
                    await displayText("a10");
                }
                endOfChapter();
            }
        }
        private async void gameTextDisplay()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            if (count == 0)
            {
                if (btn1Click == true)
                {
                    await displayText("a1");
                    await displayText("p2");
                    await displayText("p3");
                    await displayText("p4");
                    await displayText("p5");
                    await displayText("q2");
                    count++;
                }
                else if (btn2Click == true)
                {
                    await displayText("a2");
                    await displayText("p2");
                    await displayText("p3");
                    await displayText("p4");
                    await displayText("p5");
                    await displayText("q2");
                    count++;
                }
            }
            else if (count == 1)
            {
                if (btn1Click == true)
                {
                    await displayText("a3");
                    await displayText("p6");
                    await displayText("q3");
                    count++;
                }
                else if (btn2Click == true)
                {
                    await displayText("a4");
                    await displayText("p6");
                    await displayText("q3");
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
                            await displayText("ssa1");
                            await displayText("p7");
                            await displayText("q4");
                            count++;
                        }
                        else
                        {
                            await displayText("sa1");
                            await displayText("ssq1");
                            subSubCount++;
                        }
                    }
                    else
                    {
                        await displayText("a5");
                        await displayText("sq1");
                        subCount++;
                    }
                }
                else if (btn2Click == true)
                {
                    if (subCount == 1)
                    {
                        if (subSubCount == 1)
                        {
                            await displayText("ssa2");
                            await displayText("p7");
                            await displayText("q4");
                            count++;
                        }
                        else
                        {
                            await displayText("sa2");
                            await displayText("p7");
                            await displayText("q4");
                            count++;
                        }
                    }
                    else
                    {
                        await displayText("a6");
                        //enter fight scene here for man that attacks you
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
                        await displayText("sa3");
                        //add orange gem
                        window.inventoryListBox.Items.Add("Orange Gem");
                        window.inventoryItems.Add("Orange Gem");
                        await displayText("p8");
                        await displayText("q5");
                        count++;
                    }
                    else
                    {
                        await displayText("a7");
                        await displayText("sq2");
                        subCount++;
                    }
                }
                else if (btn2Click == true)
                {
                    if (subCount == 2)
                    {
                        await displayText("sa4");
                        await displayText("p8");
                        await displayText("q5");
                        count++;
                    }
                    else
                    {
                        await displayText("a8");
                        //hide button for q5
                        hideBtnForQ5 = true;
                        await displayText("p8");
                        await displayText("q5");
                        count++;
                    }
                }
            }
            else if (count == 4)
            {
                if (btn1Click == true)
                {
                    await displayText("a9");
                }
                else if (btn2Click == true)
                {
                    await displayText("a10");
                }
                endOfChapter();
            }
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
                case "q1": btn1Text = json.main[1].Q1[1].A1[0]; btn2Text = json.main[1].Q1[2].A2[0]; break;
                case "q2": btn1Text = json.main[6].Q2[1].A3[0]; btn2Text = json.main[6].Q2[2].A4[0]; break;
                case "q3": btn1Text = json.main[8].Q3[1].A5[0].T1; btn2Text = json.main[8].Q3[2].A6[0].T4; break;
                case "q4": btn1Text = json.main[10].Q4[1].A7[0].AT7; btn2Text = json.main[10].Q4[2].A8[0]; break;
                case "q5": btn1Text = json.main[12].Q5[1].A9[0]; btn2Text = json.main[12].Q5[2].A10[0]; break;
                case "sq1": btn1Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[0].SAT1; btn2Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[2].SA2[0]; break;
                case "sq2": btn1Text = json.main[10].Q4[1].A7[2].SQ2[1].SA3[0]; btn2Text = json.main[10].Q4[1].A7[2].SQ2[2].SA4[0]; break;
                case "ssq1": btn1Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[1].SSA1[0]; btn2Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[2].SSA2[0]; break;
            }
            if (hideBtnForQ5 == true)
            {
                btn2.Visibility = Visibility.Visible;
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
            MainWindow window = (MainWindow)Window.GetWindow(this);
            var json = JsonConvert.DeserializeObject<GameText>(jsonString);
            string titleText = "Chapter 3\nThe Moving Village";
            string p1Text = "\n\n\n" + json.main[0].P1;
            string q5Text = json.main[12].Q5[0].QT5;
            string sq2Text = json.main[10].Q4[1].A7[2].SQ2[0].SQT2;
            string q4Text = json.main[10].Q4[0].QT4;
            string ssq1Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[1].SA1[2].SSQ1[0].SSQT1;
            string sq1Text = json.main[8].Q3[1].A5[2].OA1[0].SQ1[0].SQT1;
            string q1Text = json.main[1].Q1[0].QT1;
            string q2Text = json.main[6].Q2[0].QT2;
            string q3Text = json.main[8].Q3[0].QT3;
            string wText = json.main[8].Q3[2].A6[2].W;
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
            string text = "";
            var element = display;
            int delay = 40;
            //determining what needs to be displayed
            switch (GameText)
            {
                case "title": text = titleText; element = title; delay = 80; break;
                case "p1": text = p1Text; element = display; delay = 40; break;
                case "q1": text = q1Text; element = display; delay = 40; displayElements("q1"); break;
                case "a1": text = a1Text; element = display; delay = 40; hideElements("a1"); break;
                case "a2": text = a2Text; element = display; delay = 40; hideElements("a2"); break;
                case "p2": text = p2Text; element = display; delay = 40; break;
                case "p3": text = p3Text; element = display; delay = 40; break;
                case "a5": text = a5Text; element = display; delay = 40; hideElements("a5"); break;
                case "a6": text = a6Text; element = display; delay = 40; hideElements("a6"); break;
                case "a3": text = a3Text; element = display; delay = 40; hideElements("a3"); break;
                case "q2": text = q2Text; element = display; delay = 40; displayElements("q2"); break;
                case "q3": text = q3Text; element = display; delay = 40; displayElements("q3"); break;
                case "a4": text = a4Text; element = display; delay = 40; hideElements("a4"); break;
                case "w": text = wText; element = display; delay = 40; hideElements("w"); break;
                case "sq1": text = sq1Text; element = display; delay = 40; displayElements("sq1"); break;
                case "ssq1": text = ssq1Text; element = display; delay = 40; displayElements("ssq1"); break;
                case "q4": text = q4Text; element = display; delay = 40; displayElements("q4"); break;
                case "sq2": text = sq2Text; element = display; delay = 40; displayElements("sq2"); break;
                case "q5": text = q5Text; element = display; delay = 40; displayElements("q5"); break;
                case "p4": text = p4Text; element = display; delay = 40; break;
                case "p5": text = p5Text; element = display; delay = 40; break;
                case "p6": text = p6Text; element = display; delay = 40; break;
                case "sa1": text = sa1Text; element = display; delay = 40; hideElements("sa1"); break;
                case "sa2": text = sa2Text; element = display; delay = 40; hideElements("sa2"); break;
                case "ssa1": text = ssa1Text; element = display; delay = 40; hideElements("ssa1"); break;
                case "ssa2": text = ssa2Text; element = display; delay = 40; hideElements("ssa2"); break;
                case "p7": text = p7Text; element = display; delay = 40; break;
                case "a7": text = a7Text; element = display; delay = 40; hideElements("a7"); break;
                case "a8": text = a8Text; element = display; delay = 40; hideElements("a8"); break;
                case "sa3": text = sa3Text; element = display; delay = 40; hideElements("sa3"); break;
                case "sa4": text = sa4Text; element = display; delay = 40; hideElements("sa4"); break;
                case "p8": text = p8Text; element = display; delay = 40; break;
                case "a9": text = a9Text; element = display; delay = 40; window.inventoryListBox.Items.Remove("Orange Gem"); window.inventoryItems.Remove("Orange Gem"); hideElements("a9"); break;
                case "a10": text = a10Text; element = display; delay = 40; hideElements("a10"); break;
            }
            //displaying text
            foreach (char c in text)
            {
                element.Text += c;
                await Task.Delay(delay);
            }
        }
        private async void endOfChapter()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.GetGold(20, "You found some Gold on the border of the moving village.");
            //fade out scene change code below
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            window.MainFrame.Navigate(new Uri("Page8.xaml", UriKind.Relative));
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
