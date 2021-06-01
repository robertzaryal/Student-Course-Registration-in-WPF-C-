using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StudentCourseRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BitmapImage> Images = new List<BitmapImage>();
        private int ImageNumber = 0;
        private DispatcherTimer imageTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Tick(object sender, System.EventArgs e)
        {
            ImageNumber = (ImageNumber + 1) % Images.Count;
            ShowNextImage(imgSlideshow);
        }

        private void ShowNextImage(Image img)
        {
            const double tansTime = 0.9;
            Storyboard sb = new Storyboard();

            DoubleAnimation fadeOut = new DoubleAnimation(1.0, 0.0,
                TimeSpan.FromSeconds(tansTime));
            fadeOut.BeginTime = TimeSpan.FromSeconds(0);

            Storyboard.SetTarget(fadeOut, img);
            Storyboard.SetTargetProperty(fadeOut,
                new PropertyPath(Image.OpacityProperty));

            sb.Children.Add(fadeOut);

            ObjectAnimationUsingKeyFrames newImageAnimation =
                new ObjectAnimationUsingKeyFrames();

            newImageAnimation.BeginTime = TimeSpan.FromSeconds(tansTime);

            DiscreteObjectKeyFrame newImageFrame =
                new DiscreteObjectKeyFrame(Images[ImageNumber], TimeSpan.Zero);
            newImageAnimation.KeyFrames.Add(newImageFrame);

            Storyboard.SetTarget(newImageAnimation, img);
            Storyboard.SetTargetProperty(newImageAnimation,
                new PropertyPath(Image.SourceProperty));

            sb.Children.Add(newImageAnimation);

            DoubleAnimation fadeIn = new DoubleAnimation(0.0, 1.0,
                TimeSpan.FromSeconds(tansTime));
            fadeIn.BeginTime = TimeSpan.FromSeconds(tansTime);

            Storyboard.SetTarget(fadeIn, img);
            Storyboard.SetTargetProperty(fadeIn,
                new PropertyPath(Image.OpacityProperty));

            sb.Children.Add(fadeIn);

            sb.Begin(img);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\rober\source\repos\robertzaryal\robertzaryal\StudentCourseRegistration\Images\");

            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                if ((fileInfo.Extension.ToLower() == ".jpg" ||
                    fileInfo.Extension.ToLower() == ".png"))
                {
                    Images.Add(new BitmapImage(new Uri(fileInfo.FullName)));
                }
            }

            imgSlideshow.Source = Images[0];

            imageTimer.Interval = TimeSpan.FromSeconds(3.0);
            imageTimer.Tick += Tick;
            imageTimer.Start();

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void addcourseBtn_Click(object sender, RoutedEventArgs e)
        {
            addCourseRecordForm windowobject = new addCourseRecordForm();
            this.Visibility = Visibility.Hidden;
            windowobject.Show();
        }

        private void addstdBtn_Click(object sender, RoutedEventArgs e)
        {
            addStudentRecordForm windowobject = new addStudentRecordForm();
            this.Visibility = Visibility.Hidden;
            windowobject.Show();
        }

        private void viewstdBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewStudentsForm windowobject = new ViewStudentsForm();
            this.Visibility = Visibility.Hidden;
            windowobject.Show();
        }

        private void notassigncourseBtn_Click(object sender, RoutedEventArgs e)
        {
            UnassignedCoursesForm windowobject = new UnassignedCoursesForm();
            this.Visibility = Visibility.Hidden;
            windowobject.Show();
        }

        private void courseBtn_Click(object sender, RoutedEventArgs e)
        {
            UnassignedStudentsForm windowobject = new UnassignedStudentsForm();
            this.Visibility = Visibility.Hidden;
            windowobject.Show();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
