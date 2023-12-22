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

namespace Глазки_Saves
{
    /// <summary>
    /// Логика взаимодействия для PriorityEdit.xaml
    /// </summary>
    public partial class PriorityEdit : Window
    {
        public int MaxPriority { get; set; }
        private ListView AgentListView;

        public PriorityEdit(ListView agentListView)
        {
            InitializeComponent();
            AgentListView = agentListView;
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int k = 0;
            var selectedAgents = AgentListView.SelectedItems.Cast<Agent>();

            foreach (var agent in selectedAgents)
            {
                try
                {
                    agent.Priority = Convert.ToInt32(Prior.Text);
                    EyesEntities.GetContext().SaveChanges();
                    Close();
                    k++;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            if(k > 0)
            {
                MessageBox.Show("Информация сохранена");
                

            }
            else
            {
                MessageBox.Show("Информация не сохранена");

            }
        }

        private void Prior_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
