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
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {

        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
            Initial Catalog=UP02Mollaev;
            Integrated Security=True");

        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        CBClass cBClass;
        public EditUserWindow()
        {
            InitializeComponent();
            cBClass=new CBClass();
        }

        private void SaveChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand=
                    new SqlCommand("Update " +
                    "dbo.[User] " +
                    $"Set [Email] ='{EmailTb.Text}', " +
                    $"[Password]='{PasswordPsb.Password}'," +
                    $"FirstName='{NameTb.Text}'," +
                    $"LastName='{SurNameTb.Text}'," +
                    $"RoleID='{RoleCb.SelectedValue.ToString()}'," +
                    $"Where UserID='{VariableClass.UserID}'",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Данные пользователя " +
                    $"{SurNameTb.Text} {NameTb.Text} " +
                    $"успешно отредактированные");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {

            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.RoleCBLoad(RoleCb);
            try
            {
                sqlConnection.Open();
                sqlCommand=
                    new SqlCommand("Select * from dbo.[User] " +
                    $"Where UserID='{VariableClass.UserID}'",
                    sqlConnection);
                dataReader=sqlCommand.ExecuteReader();
                dataReader.Read();
                EmailTb.Text=dataReader[1].ToString();
                PasswordPsb.Password = dataReader[2].ToString();
                NameTb.Text = dataReader[3].ToString();
                SurNameTb.Text= dataReader[4].ToString();
                RoleCb.SelectedValue=dataReader[5].ToString();
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
