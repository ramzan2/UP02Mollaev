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

namespace UP02Mollaev.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        SqlConnection sqlConnection =
    new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02Mollaev;
            Integrated Security=True");

        SqlCommand sqlCommand;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass= PasswordPsb.Password;
            string zagl= "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal= "qwertyyuiopasdfghjklzxcvbnm";
            string cif= "1234567890";
            string znak= "@#$%&*";

            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Вы не ввели логин");
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MBClass.ErrorMB("Вы не ввели пароль");
                PasswordPsb.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать прописную букву");
                PasswordPsb.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать строчную букву");
                PasswordPsb.Focus();
            }
            else if(cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать прописную цифру");
                PasswordPsb.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать " +
                   "один из следующих знаков: " + znak);
                PasswordPsb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MBClass.ErrorMB("Вы не ввели повторно пароль");
                PasswordPsb.Focus();
            }
            else if (PasswordDoublePsb.Password != PasswordPsb.Password)
            {
                MBClass.ErrorMB("пароли не совпадают");
                PasswordPsb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand=new SqlCommand("Insert Into dbo.[User] " +
                        "(Email,Password,RoleID) Values " +
                        $"('{LoginTb.Text}','{PasswordPsb.Password}',21)",sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Вы успешно зарегистрировались");
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

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
