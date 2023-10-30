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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            var current_Agent = EyesEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = current_Agent;
            CB_Filtr.SelectedIndex = 0;
            CB_Sort.SelectedIndex = 0;
            Update_Agent();
        }
        public void Update_Agent()
        {
            var current_Agent = EyesEntities.GetContext().Agent.ToList();

            if(CB_Sort.SelectedIndex == 1)
            {
                current_Agent = current_Agent.OrderBy(p => p.Title).ToList();
            }
            if (CB_Sort.SelectedIndex == 2)
            {
                current_Agent = current_Agent.OrderByDescending(p => p.Title).ToList();
            }
            if (CB_Sort.SelectedIndex == 3)
            {
                current_Agent = current_Agent.OrderBy(p => p.Title).ToList();
            }
            if (CB_Sort.SelectedIndex == 4)
            {
                current_Agent = current_Agent.OrderBy(p => p.Title).ToList();
            }
            if (CB_Sort.SelectedIndex == 5)
            {
                current_Agent = current_Agent.OrderBy(p => p.Priority).ToList();
            }
            if (CB_Sort.SelectedIndex == 6)
            {
                current_Agent = current_Agent.OrderByDescending(p => p.Priority).ToList();
            }
            current_Agent = current_Agent.Where(p => p.Title.ToLower().Contains(TB_Search.Text.ToLower())).ToList();
            if(CB_Filtr.SelectedIndex == 1)
            {
                current_Agent = current_Agent.Where(p => p.AgentTypeString == "МФО").ToList();
            }
            if (CB_Filtr.SelectedIndex == 2)
            {
                current_Agent = current_Agent.Where(p => p.AgentTypeString == "ООО").ToList();
            }
            if (CB_Filtr.SelectedIndex == 3)
            {
                current_Agent = current_Agent.Where(p => p.AgentTypeString == "ЗАО").ToList();
            }
            if (CB_Filtr.SelectedIndex == 4)
            {
                current_Agent = current_Agent.Where(p => p.AgentTypeString == "МКК").ToList();
            }
            if (CB_Filtr.SelectedIndex == 5)
            {
                current_Agent = current_Agent.Where(p => p.AgentTypeString == "ОАО").ToList();
            }
            if (CB_Filtr.SelectedIndex == 6)
            {
                current_Agent = current_Agent.Where(p => p.AgentTypeString == "ПАО").ToList();
            }

            AgentListView.ItemsSource = current_Agent;
        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_Agent();

        }

        private void CB_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_Agent();
        }

        private void CB_Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_Agent();
        }
    }
}
