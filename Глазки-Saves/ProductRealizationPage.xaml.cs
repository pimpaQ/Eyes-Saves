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
    /// Логика взаимодействия для ProductRealizationPage.xaml
    /// </summary>
    public partial class ProductRealizationPage : Page
    {
        private int selAg;

        public ProductRealizationPage(Agent selectedAgent)
        {
            InitializeComponent();
            ProductSaleListView.ItemsSource = (from productSale in EyesEntities.GetContext().ProductSale
                                               where productSale.AgentID == selectedAgent.ID
                                               select productSale).ToList();
            var selectedProduct = ProductSaleListView.SelectedItems.Cast<ProductSale>();
            int k = 0;
            foreach(var product in ProductSaleListView.Items)
            {
                k++;
            }
            if(k <= 0)
            {
                NoProd.Text = "Нет реализованной продукции...";
            }
            else
            {
                NoProd.Text = "";
            }
            selAg = selectedAgent.ID;
        }

        private void AddProdSale_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProdRealizAdd(selAg));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentProdSale = (sender as Button).DataContext as ProductSale;
            
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        EyesEntities.GetContext().ProductSale.Remove(currentProdSale);
                        EyesEntities.GetContext().SaveChanges();
                        Manager.MainFrame.GoBack();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            
        }
    }
}
