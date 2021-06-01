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
using System.Windows.Shapes;

namespace StudentCourseRegistration
{
    /// <summary>
    /// Interaction logic for addCourseRecordForm.xaml
    /// </summary>
    public partial class addCourseRecordForm : Window
    {
        Student_DBEntities std = new Student_DBEntities();
        public addCourseRecordForm()
        {
            InitializeComponent();
        }
        public void fillgrid()
        {
            var query = from s in std.Courses
                        select new
                        {
                            Course_Code = s.Code,
                            Course_Name = s.Name
                        };
            this.coursegrid.ItemsSource = query.ToList();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow windowobject = new MainWindow();
            this.Visibility = Visibility.Hidden;
            windowobject.Show();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(coursecodetxtBox.Text) && !string.IsNullOrEmpty(coursenametxtBox.Text))
            {
                Course c = new Course();
                c.Code = coursecodetxtBox.Text;
                c.Name = coursenametxtBox.Text;
                std.Courses.Add(c);
                std.SaveChanges();
                fillgrid();
                coursecodetxtBox.Text = "";
                coursenametxtBox.Text = "";
            }
            else
                MessageBox.Show("Fill all the Fields!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillgrid();
        }
    }
}
