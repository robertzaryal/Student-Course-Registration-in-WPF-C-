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
    /// Interaction logic for UnassignedCoursesForm.xaml
    /// </summary>
    public partial class UnassignedCoursesForm : Window
    {
        Student_DBEntities std = new Student_DBEntities();
        public UnassignedCoursesForm()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from c in std.Courses
                        where
                            !(from sc in std.StudentCourses select sc.CourseID).Contains(c.Id)
                        select new
                        {
                            CourseCode = c.Code,
                            CourseName = c.Name
                        };
            this.datagrid.ItemsSource = query.Distinct().ToList();
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow windowobject = new MainWindow();
            this.Visibility = Visibility.Hidden;
            windowobject.Show();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
