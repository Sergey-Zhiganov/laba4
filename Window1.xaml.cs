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

namespace laba1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        up01Entities up01 = new up01Entities();
        public Window1()
        {
            InitializeComponent();
            up01DataGrid.Items.Clear();
            up01DataGrid.ItemsSource = up01.Students.ToList();
            Group_IDText.ItemsSource = up01.Groups.ToList();
        }
        private void up01DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (up01DataGrid.SelectedItem != null)
            {
                Students selected = up01DataGrid.SelectedItem as Students;
                SurnameText.Text = selected.Surname;
                FirstnameText.Text = selected.Fistname;
                MiddleNameText.Text = selected.MiddleName;
                Group_IDText.Text = selected.Groups.Title;
            }
            else
            {
                SurnameText.Text = null;
                FirstnameText.Text = null;
                MiddleNameText.Text = null;
                Group_IDText.Text = null;
            }
        }
        private void Save()
        {
            up01.SaveChanges();
            up01DataGrid.ItemsSource = up01.Students.ToList();
        }
        //Добавление
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Students student = new Students();
            student.Surname = SurnameText.Text;
            student.Fistname = FirstnameText.Text;
            student.MiddleName = MiddleNameText.Text;
            student.Group_ID = (Group_IDText.SelectedItem as Groups).ID_Group;
            up01.Students.Add(student);
            Save();
        }
        //Изменение
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (up01DataGrid.SelectedItem != null)
            {
                Students student = up01DataGrid.SelectedItem as Students;
                student.Surname = SurnameText.Text;
                student.Fistname = FirstnameText.Text;
                student.MiddleName = MiddleNameText.Text;
                student.Group_ID = (Group_IDText.SelectedItem as Groups).ID_Group;
                Save();
            }
        }
        //Удаление
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (up01DataGrid.SelectedItem != null)
            {
                up01.Students.Remove(up01DataGrid.SelectedItem as Students);
                Save();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int group = 0;
            var row = Group_IDText.SelectedItem as Groups;
            if (row != null)
            {
                group = row.ID_Group;
            }
            up01DataGrid.ItemsSource = up01.Students.Where(up01 =>
                (up01.Surname.Contains(SurnameText.Text) || SurnameText.Text == "" || SurnameText.Text == null) &&
                (up01.Fistname.Contains(FirstnameText.Text) || FirstnameText.Text == "" || FirstnameText.Text == null) &&
                (up01.MiddleName.Contains(MiddleNameText.Text) || MiddleNameText.Text == "" || MiddleNameText.Text == null) &&
                (up01.Group_ID == group || group == 0)).ToList();
        }
    }
}
