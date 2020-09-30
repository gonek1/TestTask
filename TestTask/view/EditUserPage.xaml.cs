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
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Window
    {
        User user;
        public EditUserPage(User editUser)
        {
            InitializeComponent();
            loadInfo(editUser);
        }
        void loadInfo(User editUser)
        {
            user = editUser;
            nameText.Text = user.Name;
            loginText.Text = user.Login;
            passwordText.Text = user.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsCorrectData())
            {
                MessageBox.Show("Неверно заполнены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var bd = MainWindow.bd;
            int id = user.id;
            user.Name = nameText.Text;
            user.Password = passwordText.Text;
            bd.SaveChanges();
            var users = MainWindow.bd.Users.Where(x => x.id == user.id).ToList();
            UsersPage.datagrid.ItemsSource = users;
            this.Close();
        }
        bool IsCorrectData()
        {
            if (nameText.Text.Length < 1)
            {
                return false;
            }
            if (passwordText.Text == string.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
