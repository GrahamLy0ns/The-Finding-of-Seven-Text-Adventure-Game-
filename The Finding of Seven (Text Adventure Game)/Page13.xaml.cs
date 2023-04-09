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
using System.Xml.Linq;
using System.Reflection;
using System.Windows.Media.Animation;

namespace The_Finding_of_Seven__Text_Adventure_Game_
{
    /// <summary>
    /// Interaction logic for Page13.xaml
    /// </summary>
    public partial class Page13 : Page
    {
        public Page13()
        {
            InitializeComponent();
        }
        private Storyboard storyboard = new Storyboard();
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sans.Visibility = Visibility.Collapsed;
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //getting assets from the db
            var imageQuery = from a in window.db.ImageTBLs
                             where a.ImageName == "villageEnding"
                             select a.ImageSrc;
            var soundQuery = from a in window.db.SoundTBLs
                             where a.SoundName == "megalovania"
                             select a.SoundSrc;
            string path = soundQuery.ToList()[0];
            string imageSrc = imageQuery.ToList()[0];
            background.Background = new ImageBrush(new BitmapImage(new Uri(imageSrc, UriKind.Relative)));
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
            //displaying text and moving sans
            await displayText("title");
            await displayText("credits");
            await Task.Delay(4000);
            movingSans();
            sans.Visibility = Visibility.Visible;
            await Task.Delay(20000);
            endGame();
        }
        private async Task displayText(string GameText)
        {
            var element = display;
            string text = "";
            string titleText = "THE END!";
            string creditText = "\n\n\nCreated by Graham Lyons\n\nThanks for playing!";
            int delay = 40;
            switch (GameText)
            {
                case "title": text = titleText; element = title; delay = 80; break;
                case "credits": text = creditText; break;
            }
            //displaying text
            foreach (char c in text)
            {
                element.Text += c;
                await Task.Delay(delay);
            }
        }
        private void movingSans()
        {
            // Create a DoubleAnimationUsingKeyFrames to move the rectangle left and right
            DoubleAnimationUsingKeyFrames moveAnimation = new DoubleAnimationUsingKeyFrames();
            moveAnimation.Duration = TimeSpan.FromSeconds(5);
            moveAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Create the key-frames for the animation
            EasingDoubleKeyFrame keyFrame1 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            EasingDoubleKeyFrame keyFrame2 = new EasingDoubleKeyFrame(1100, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.5)));
            EasingDoubleKeyFrame keyFrame3 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5)));

            // Add the key-frames to the animation
            moveAnimation.KeyFrames.Add(keyFrame1);
            moveAnimation.KeyFrames.Add(keyFrame2);
            moveAnimation.KeyFrames.Add(keyFrame3);

            // Set the animation targets
            Storyboard.SetTarget(moveAnimation, sans);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(Canvas.LeftProperty));

            // Create a Storyboard and add the DoubleAnimationUsingKeyFrames to it

            storyboard.Children.Add(moveAnimation);

            // Start the Storyboard
            storyboard.Begin();
        }
        private async void endGame()
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            //fading out
            while (background.Opacity > 0)
            {
                window.player.Volume -= 0.01;
                background.Opacity -= 0.01;
                await Task.Delay(40);
            }
            //restarting app
            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0]);
        }
        private void displayBox_LayoutUpdated(object sender, EventArgs e)
        {
            scroll.ScrollToBottom();
        }
    }
}
