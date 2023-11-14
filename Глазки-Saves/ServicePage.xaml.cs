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
using System.Xml;

namespace Глазки_Saves
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        int CountRecords = 0;
        int CountPage = 0;
        int CurrentPage = 0;

        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList = new List<Agent>();
        public ServicePage()
        {
            InitializeComponent();
            var current_Agent = EyesEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = current_Agent;
            CB_Filtr.SelectedIndex = 0;
            CB_Sort.SelectedIndex = 0;
            Update_Agent();

        }
        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }

            Boolean Ifupdate = true;
            int min;
            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = "из" + CountRecords.ToString();

                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();
            }
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
            current_Agent = current_Agent.Where(p => p.Title.ToLower().Contains(TB_Search.Text.ToLower()) || p.Email.ToLower().Contains(TB_Search.Text.ToLower()) || p.PhoneFiltr.Contains(TB_Search.Text.Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", ""))).ToList();
            if (CB_Filtr.SelectedIndex == 1)
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
            TableList = current_Agent;
            ChangePage(0, 0);
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

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));

        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                EyesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                AgentListView.ItemsSource = EyesEntities.GetContext().Agent.ToList();
                Update_Agent();
            }
        }
    }
}
