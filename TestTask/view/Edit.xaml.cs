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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        Company company;
        public Edit(Company company)
        {
            InitializeComponent();
            LoadData(company);

        }

        private void LoadData(Company company)
        {
            nameTextBox.Text = company.Name;
            comboxStatus.Text = company.ContractStatus;
            this.company = company;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsCorrectData())
            {
                MessageBox.Show("Неверно заполнены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var bd = MainWindow.bd;
            int id = company.Id;
            Company companyEdit = (from m in bd.Companies where m.Id == id select m).Single();
            company.Name = nameTextBox.Text;
            company.ContractStatus = comboxStatus.Text;
            bd.SaveChanges();
            MainWindow.datagrid.ItemsSource = bd.Companies.ToList();
            this.Close();    
        }
        bool IsCorrectData()
        {
            if (nameTextBox.Text.Length < 1)
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
