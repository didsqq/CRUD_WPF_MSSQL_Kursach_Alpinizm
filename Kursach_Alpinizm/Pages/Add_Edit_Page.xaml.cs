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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Collections.Specialized;



namespace Kursach_Alpinizm
{
    /// <summary>
    /// Логика взаимодействия для Add_Edit_Page.xaml
    /// </summary>
    public partial class Add_Edit_Page : Page
    {
        private mountain _currentMountain = new mountain();
        
        public Add_Edit_Page(mountain selectedmountain)
        {
            InitializeComponent();

            if (selectedmountain != null)
                _currentMountain = selectedmountain;
            DataContext = _currentMountain;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentMountain.Title))
                errors.AppendLine("Укажите название горы");
            if (string.IsNullOrWhiteSpace(_currentMountain.Mountain_range))
                errors.AppendLine("Укажите название хребта");
            if (_currentMountain.Height < 372 || _currentMountain.Height > 8848)
                errors.AppendLine("Высоты с такой горой нет(");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentMountain.ID_mountain == 0)
                AlpinizmEntities.GetContext().mountain.Add(_currentMountain);
            try
            {
                AlpinizmEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.Navigate(new Page_Mountains());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Mountains());
        }
    }
}
