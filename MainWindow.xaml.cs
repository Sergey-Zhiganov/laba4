using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using laba1.up01DataSetTableAdapters;

namespace laba1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StudentsTableAdapter students = new StudentsTableAdapter();
        GroupsTableAdapter groups = new GroupsTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
            up01DataGrid.ItemsSource = students.Select1();
            Group_IDText.ItemsSource = groups.GetData();
            Window1 window1 = new Window1();
            window1.Show();
        }
        private void up01DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (up01DataGrid.SelectedItem != null)
            {
                DataRowView selected = up01DataGrid.SelectedItem as DataRowView;
                SurnameText.Text = selected["Surname"].ToString();
                FirstnameText.Text = selected["Fistname"].ToString();
                MiddleNameText.Text = selected["MiddleName"].ToString();
                Group_IDText.Text = selected["Title"].ToString();
            }
            else
            {
                SurnameText.Text = null;
                FirstnameText.Text = null;
                MiddleNameText.Text = null;
                Group_IDText.Text = null;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                students.Insert1(SurnameText.Text, FirstnameText.Text, MiddleNameText.Text, Convert.ToInt32((Group_IDText.SelectedItem as DataRowView)["ID_Group"]));
                up01DataGrid.ItemsSource = students.GetData();
            }
            catch (Exception ex) {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Students student = up01DataGrid.SelectedItem as Students;
                students.Update1(SurnameText.Text, FirstnameText.Text, MiddleNameText.Text, Convert.ToInt32((Group_IDText.SelectedItem as DataRowView)["ID_Group"]), student.ID_Student);
                up01DataGrid.ItemsSource = students.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                students.Delete1(Convert.ToInt32(students.GetData()[up01DataGrid.SelectedIndex][0]));
                up01DataGrid.ItemsSource = students.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int group = 0;
            DataRowView row = Group_IDText.SelectedItem as DataRowView;
            if (row != null)
            {
                group = Convert.ToInt32((Group_IDText.SelectedItem as DataRowView)["ID_Group"]);
            }
            up01DataGrid.ItemsSource = students.FindBy(SurnameText.Text, FirstnameText.Text, MiddleNameText.Text, group);
        }
    }
}
