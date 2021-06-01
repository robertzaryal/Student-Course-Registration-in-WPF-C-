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
    /// Interaction logic for ViewStudentsForm.xaml
    /// </summary>
    public partial class ViewStudentsForm : Window
    {
        Student_DBEntities std = new Student_DBEntities();
        public ViewStudentsForm()
        {
            InitializeComponent();
        }

        public void fillgrid()
        {
            var query = from sc in std.StudentCourses
                        from s in std.Students
                        where (s.Id == sc.StudentID)
                        from c in std.Courses
                        where (c.Id == sc.CourseID) && (c.Name.Contains(searchtxtBox.Text) || s.Name.Contains(searchtxtBox.Text))
                        select new
                        {
                            Student_Name = sc.Student.Name,
                            Course_Name = sc.Course.Name
                        };
            this.datagrid.ItemsSource = query.ToList();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fillgrid();
        }

        private void searchtxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                fillgrid();
        }

        private void exitbtn_Click(object sender, RoutedEventArgs e)
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
