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
using UP02Mollaev.СlassFolder;

namespace UP02Mollaev.WindowFolder.AdminFolder1
{
    /// <summary>
    /// Логика взаимодействия для MenuAdminFolderWindow.xaml
    /// </summary>
    public partial class MenuAdminFolderWindow : Window
    {
        DGClass dGClass;
        public MenuAdminFolderWindow()
        {
            InitializeComponent();
            dGClass=new DGClass(ListUserDG);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
              dGClass.LoadDG("Select * From dbo.[User]");
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[User] " +
                $"Where LastName Like '%{SearchTb.Text}%'" +
                $"OR Email Like '%{SearchTb.Text}%'");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.UserID=dGClass.SelectId();
                    new EditUserWindow().ShowDialog();
                    dGClass.LoadDG("Select * From dbo.[User]");

                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }
    }
}
