using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace StudentCourseRegistration
{
    /// <summary>
    /// Interaction logic for UnassignedStudentsForm.xaml
    /// </summary>
    public partial class UnassignedStudentsForm : Window
    {
        Student_DBEntities std = new Student_DBEntities();
        public UnassignedStudentsForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var result = from s in std.Students
                        where
                        !(from sc in std.StudentCourses select sc.StudentID).Contains(s.Id)
                        select new
                        {
                            Student_RegNo = s.RegNo,
                            Student_Name = s.Name
                        };
            this.datagrid.ItemsSource = result.Distinct().ToList();
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow windowobject = new MainWindow();
            this.Visibility = Visibility.Hidden;
            windowobject.Show();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
