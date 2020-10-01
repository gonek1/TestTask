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
            CreateNewCompany();
        }

        private static void CreateNewCompany()
        {
            Insert insertPage = new Insert();
            insertPage.ShowDialog();
        }

        private void deleteBtn_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteCompany();
        }

        private void DeleteCompany()
        {
            DeleteCompanyUsers();
            RemoveCompany();
            FillDataGrid();
        }

        private void RemoveCompany()
        {
            bd.Companies.Remove(ReturnCompany());
            bd.SaveChanges();
        }

        private void DeleteCompanyUsers()
        {
            int id = ReturnCompany().Id;
            var usersDelete = bd.Users.Where(x => x.id == id);
            if (usersDelete.Count() > 0)
            {
                foreach (var user in usersDelete)
                {
                    bd.Users.Remove(user);
                }
            }
        }

        public static void FillDataGrid()
        {
            datagrid.ItemsSource = bd.Companies.ToList();
        }

        static public void AddNewUser(User newUser)
        {
            if (newUser == null)
            {
                return;
            }
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
            ShowEditingForm();
        }

        private void ShowEditingForm()
        {
            Edit editPage = new Edit(ReturnCompany());
            editPage.ShowDialog();
        }

        private Company ReturnCompany()
        {
            return myDataGrid.SelectedItem as Company;
        }

        private void showAllBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowAllCompanyUsers();
        }

        private void ShowAllCompanyUsers()
        {
            UsersPage usersPage = new UsersPage(ReturnCompany());
            usersPage.ShowDialog();
        }
    }
}
