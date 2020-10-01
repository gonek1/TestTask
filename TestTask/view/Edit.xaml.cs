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
        private Company company;
        public Edit(Company company)
        {
            if (company==null)
            {
                return;
            }
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
            SaveAllChangesAndClose();
        }

        private void SaveAllChangesAndClose()
        {
            if (!IsCorrectData())
            {
                MessageBox.Show("Неверно заполнены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveCompanyChanges();
            MainWindow.FillDataGrid();
            Close();
        }

        private void SaveCompanyChanges()
        {
            var bd = MainWindow.bd;
            Company companyEdit = (from m in bd.Companies where m.Id == company.Id select m).Single();
            company.Name = nameTextBox.Text;
            company.ContractStatus = comboxStatus.Text;
            bd.SaveChanges();
        }

        bool IsCorrectData()
        {
            if (nameTextBox.Text == string.Empty)
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
