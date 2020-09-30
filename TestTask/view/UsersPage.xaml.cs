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
using TestTask.model;

namespace TestTask.view
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Window
    {
        
        public static DataGrid datagrid;
        private Company companyParent;
        public UsersPage(Company company)
        {
            
            InitializeComponent();
            LoadData(company);
        }
        void LoadData(Company company)
        {
            companyParent = company;
            var users = MainWindow.bd.Users.Where(x => x.id == companyParent.Id).ToList();
            myDataGrid.ItemsSource = users;
            datagrid = myDataGrid;
        }

        private void addNewbtn_Click(object sender, RoutedEventArgs e)
        {
           
            NewUser newUserPage = new NewUser(companyParent);
            newUserPage.ShowDialog();
        }

        private void editUser_Click(object sender, RoutedEventArgs e)
        {
            var user = myDataGrid.SelectedItem as User;
            if (user==null)
            {
                return;
            }
            EditUserPage editUserPage = new EditUserPage(user);
            editUserPage.ShowDialog();
        }

        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {
            var bd = MainWindow.bd;
            var user = myDataGrid.SelectedItem as User;
            if (user==null)
            {
                return;
            }
            bd.Users.Remove(user);
            bd.SaveChanges();
            var users = MainWindow.bd.Users.Where(x => x.id == companyParent.Id).ToList();
            myDataGrid.ItemsSource = users;

        }
    }
}
