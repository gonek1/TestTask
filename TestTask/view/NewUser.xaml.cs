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
    /// Логика взаимодействия для NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        private Company companyParent;
        public NewUser(Company company)
        {
            if (company == null)
            {
                return;
            }
            InitializeComponent();
            LoadInfo(company);
        }

        private void LoadInfo(Company company)
        {
            companyParent = company;
            companyName.Text = companyParent.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewUser();
        }

        private void AddNewUser()
        {
            if (!IsCorrectData())
            {
                MessageBox.Show("Неверно заполнены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var bd = MainWindow.bd;
            User newUser = CreateNewUser();
            MainWindow.AddNewUser(newUser);
            UsersPage.datagrid.ItemsSource = bd.Users.Where(x => x.id == newUser.id).ToList();
            Close();
        }

        private User CreateNewUser()
        {
            return new User()
            {
                id = companyParent.Id,
                Name = nameText.Text,
                Login = loginRext.Text,
                Password = passwordText.Text
            };
        }

        bool IsCorrectData()
        {
            if (nameText.Text.Length < 1)
            {
                return false;
            }
            if (loginRext.Text.Length < 1)
            {
                return false;
            }
            if (passwordText.Text.Length <1)
            {
                return false;
            }
            return true;
        }

    }
}
