using Microsoft.Win32;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Глазки_Saves
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        int k = 0;
        private Agent _currentAgent = new Agent();
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();
            if (SelectedAgent != null)
            {
                _currentAgent = SelectedAgent;
                k++;
            }
            DataContext = _currentAgent;
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _currentAgent.Logo = openFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
            {
                errors.AppendLine("Укажите наименование");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
            {
                errors.AppendLine("Укажите адрес");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
            {
                errors.AppendLine("Укажите директора");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
            {
                errors.AppendLine("Укажите ИНН");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
            {
                errors.AppendLine("Укажите КПП");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
            {
                errors.AppendLine("Укажите приоритет");
            }
            if (CB_Sel.SelectedItem == null)
            {
                errors.AppendLine("Укажите тип агента");
            }
            if (_currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет");
            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
            {
                errors.AppendLine("Укажите Email");
            }
            else if (!_currentAgent.Email.Contains('@') && !_currentAgent.Email.Contains('.'))
            {
                errors.AppendLine("Укажите почту правильно");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
            {
                errors.AppendLine("Укажите телефон");
            }
            else
            {
                string ph = _currentAgent.PhoneFiltr;
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || ph[1] == '3' && ph.Length != 12)
                    errors.AppendLine("Введите телефон правильно");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentAgent.AgentTypeID = CB_Sel.SelectedIndex + 1;
            if (_currentAgent.ID == 0)
            {
                EyesEntities.GetContext().Agent.Add(_currentAgent);
            }
            try
            {
                EyesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent;
            var currentProductAgent = EyesEntities.GetContext().ProductSale.ToList();
            currentProductAgent = currentProductAgent.Where(p => p.AgentID == currentAgent.ID).ToList();
            if (currentProductAgent.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, т.к услуга реализована");
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        EyesEntities.GetContext().Agent.Remove(currentAgent);
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

        private void CB_Sel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
