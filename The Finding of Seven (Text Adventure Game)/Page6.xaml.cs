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
using System.Drawing;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Interop;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        public Page6()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //getting assets from database
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "determination"
                             select a.SoundSrc;
            string path = soundQuery.ToList()[0];
            window.player.Source = new Uri(path, UriKind.RelativeOrAbsolute);
            background.Opacity = 0;
            text1.Opacity = 0;
            text2.Opacity = 0;
            restartBtn.Opacity = 0;
            
            while (background.Opacity < 1)
            {
                window.player.Volume += 0.01;
                background.Opacity += 0.01;
                text1.Opacity += 0.01;
                text2.Opacity += 0.01;
                restartBtn.Opacity += 0.01;
                Task.Delay(40);
            }
        }

        private void restartBtn_Click(object sender, RoutedEventArgs e)
        {
            //reseting game data
            MainWindow window = (MainWindow)Window.GetWindow(this);
            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0]);
            window.fightEncountered = false;
        }
    }
}
