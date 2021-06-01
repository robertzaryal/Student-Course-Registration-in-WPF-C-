using System.Linq;
using System.Windows;


namespace StudentCourseRegistration
{
    /// <summary>
    /// Interaction logic for addStudentRecordForm.xaml
    /// </summary>
    public partial class addStudentRecordForm : Window
    {
        Student_DBEntities std = new Student_DBEntities();
        public addStudentRecordForm()
        {
            InitializeComponent();
        }

        public void fillgrid()
        {
            var query = from s in std.Students
                        select new
                        {
                            StudentRegNo = s.RegNo,
                            StudentName = s.Name
                        };
            this.stdgrid.ItemsSource = query.ToList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(stdcodetxtBox.Text) && !string.IsNullOrEmpty(stdnametxtBox.Text))
            {
                Student s = new Student();
                s.RegNo = stdcodetxtBox.Text;
                s.Name = stdnametxtBox.Text;
                std.Students.Add(s);
                std.SaveChanges();
                fillgrid();
                stdcodetxtBox.Text = "";
                stdnametxtBox.Text = "";
            }
            else
                MessageBox.Show("Fill all the Fields!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fillgrid();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
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
