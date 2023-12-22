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

namespace Глазки_Saves
{
    /// <summary>
    /// Логика взаимодействия для ProdRealizAdd.xaml
    /// </summary>
    public partial class ProdRealizAdd : Page
    {
        private ProductSale _currentp = new ProductSale();
        private int AgID;
        public ProdRealizAdd(int selAg)
        {
            InitializeComponent();
            ProdCombo.ItemsSource = EyesEntities.GetContext().Product.ToList();
            ProdCombo.DisplayMemberPath = "Title";
            AgID = Convert.ToInt32(selAg);
            DataContext = _currentp;
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (ProdCombo.SelectedItem == null)
                errors.AppendLine("Выберите продукцию");
            if (string.IsNullOrWhiteSpace(SaleDateTime.Text))
                errors.AppendLine("Выберите дату");
            if (_currentp.ProductCount <= 0)
                errors.AppendLine("Укажите количество продаж");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            SaleDateTime.Text = System.DateTime.Now.ToString();
            _currentp.ProductID = ProdCombo.SelectedIndex + 1;
            _currentp.AgentID = AgID;
            _currentp.SaleDate = Convert.ToDateTime(SaleDateTime.Text);
            if (_currentp.ID == 0)
            {
                EyesEntities.GetContext().ProductSale.Add(_currentp);
            }
            try
            {
                EyesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CountSale_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
