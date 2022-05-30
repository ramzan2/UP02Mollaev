using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using UP02Mollaev.СlassFolder;

namespace UP02Mollaev.WindowFolder.AdminFolder1
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        CBClass cBClass;

        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02Mollaev;
            Integrated Security=True");
        SqlCommand sqlCommand;
        public AddUserWindow()
        {
            InitializeComponent();
            cBClass=new CBClass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.RoleCBLoad(RoleCb);
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTb.Text))
            {
                MBClass.ErrorMB("Вы не ввели логин");
                EmailTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MBClass.ErrorMB("Вы не ввели пароль");
                PasswordPsb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MBClass.ErrorMB("Вы не ввели имя");
                NameTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(SurNameTb.Text))
            {
                MBClass.ErrorMB("Вы не ввели фамилию");
                SurNameTb.Focus();
            }
            else if (RoleCb.SelectedIndex == 1)
            {
                MBClass.ErrorMB("Вы не выбрали роль");
                RoleCb.Focus();
            }
            try
            {
                sqlConnection.Open();
                sqlCommand=new SqlCommand("Insert Into dbo.[User] " +
                    "(Email, Password, FirstName, LastName, RoleID) " +
                    $"Values ('{EmailTb.Text}', " +
                    $"'{PasswordPsb.Password}', " +
                    $"'{NameTb.Text}', " +
                    $"'{SurNameTb.Text}', " +
                    $"'{RoleCb.SelectedValue.ToString()}')",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Пользователь {SurNameTb.Text} " +
                    $"{NameTb.Text} успешно добавлен");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

       
    }
}
