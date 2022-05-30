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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        SqlConnection sqlConnection=
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02Mollaev;
            Integrated Security=True");

        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
           

            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Вы не ввели логин");
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MBClass.ErrorMB("Вы не ввели пароль");
                PasswordPsb.Focus();
            }

            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Select * FROM " +
                        "dbo.[User] " +
                        $"Where Email='{LoginTb.Text}' ", sqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dataReader.Read();


                    if (dataReader[2].ToString() != PasswordPsb.Password)
                    {
                        MBClass.ErrorMB("Вы ввели не верный пароль");
                        PasswordPsb.Focus();
                    }
                    else
                    {
                        switch (dataReader[5].ToString())
                        {
                            case "20":
                               new AdminFolder1.MenuAdminFolderWindow().ShowDialog();
                                break;
                            case "21":
                                MBClass.InfoMB("Пользователь");
                                break;
                            case "22":
                                MBClass.InfoMB("Менеджер");
                                break;
                        }

                    }
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

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().ShowDialog();
            
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
