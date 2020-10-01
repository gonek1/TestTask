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
    /// Логика взаимодействия для Insert.xaml
    /// </summary>
    public partial class Insert : Window
    {
       
        public Insert()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewCompany();
        }

        private void AddNewCompany()
        {
            var bd = MainWindow.bd;
            if (!IsCorrectData())
            {
                MessageBox.Show("Неверно заполнены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            CreateNewCompany(bd);
            MainWindow.FillDataGrid();
            Close();
        }

        private void CreateNewCompany(DB bd)
        {
            Company company = new Company()
            {
                Name = loginTextBox.Text,
                ContractStatus = comboxStatus.Text,
            };
            bd.Companies.Add(company);
            bd.SaveChanges();
        }

        bool IsCorrectData()
        {
            if (loginTextBox.Text.Length < 1)
            {
                return false;
            }
            if (comboxStatus.Text == string.Empty)
            {
                return false;
            }
            
            return true;
        }
        
    }
}

