using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestTask.model;
using TestTask.view;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DB bd = new DB();
        public static DataGrid datagrid;
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            myDataGrid.ItemsSource = bd.Companies.ToList();
            datagrid = myDataGrid;
        }
        private void addNewBtn_Click(object sender, RoutedEventArgs e)
        {
            Insert insertPage = new Insert();
            insertPage.ShowDialog();
        }
        private void deleteBtn_Click_1(object sender, RoutedEventArgs e)
        {
            var companyDelete = myDataGrid.SelectedItem as Company;
            if (companyDelete == null)
            {
                return;
            }
            var usersDelete = bd.Users.Where(x => x.id == companyDelete.Id);
            if (usersDelete.Count()>0)
            {
                foreach (var user in usersDelete)
                {
                    bd.Users.Remove(user);
                }
            }
            
            bd.Companies.Remove(companyDelete);
            bd.SaveChanges();
            datagrid.ItemsSource = bd.Companies.ToList();
        }
        static public void AddNewUser(User newUser)
        {
            try
            {
                bd.Users.Add(newUser);
                bd.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage);
                    }
                }
            }
           
        }
        private void editBtn_Click_1(object sender, RoutedEventArgs e)
        {
            var companyEdit = myDataGrid.SelectedItem as Company;
            if (companyEdit == null)
            {
                return;
            }
            Edit editPage = new Edit(companyEdit);
            editPage.ShowDialog();
        }

        private void showAllBtn_Click(object sender, RoutedEventArgs e)
        {
            var companyUsers = myDataGrid.SelectedItem as Company;
            if (companyUsers ==null)
            {
                return;
            }
            UsersPage usersPage = new UsersPage(companyUsers);
            usersPage.ShowDialog();
        }
    }
}
