using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace Homework11_WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Worker> peopleList { get; set; }
        private Consultant consultant = new Consultant();
        private Manager manager = new Manager();
        private Worker SelectedData { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ButtonAdd.Visibility = Visibility.Hidden;
            ButtonEdit.Visibility = Visibility.Hidden;
            buttonInformation.Visibility = Visibility.Hidden;
        }

        /// <summary>
            /// Выбор вида пользователя (менеджер или консультант)
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void comboBoxChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem _comboBoxItem = (ComboBoxItem)comboBoxChoice.SelectedItem;
            string _equalValue = _comboBoxItem.Content.ToString(); //выбранное значение в ComboBox

            XDocument xDocument = XDocument.Load("people.xml");
            peopleList = new ObservableCollection<Worker>();

            if (_equalValue == "Консультант")
            {
                consultant.ReadXmlFile(xDocument, _equalValue, peopleList);
                dataGridListPerson.ItemsSource = peopleList;

                ButtonAdd.Visibility = Visibility.Hidden;
                ButtonEdit.Visibility = Visibility.Visible;
                buttonInformation.Visibility = Visibility.Visible;
            }
            if (_equalValue == "Менеджер")
            {
                manager.ReadXmlFile(xDocument, _equalValue, peopleList);
                dataGridListPerson.ItemsSource = peopleList;

                ButtonAdd.Visibility = Visibility.Visible;
                ButtonEdit.Visibility = Visibility.Visible;
                buttonInformation.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditData editData = new EditData(peopleList);
            editData.ShowDialog();
        }

        /// <summary>
        /// Редактирование данных клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem _comboBoxItem = (ComboBoxItem)comboBoxChoice.SelectedItem;
            string _equalValue = _comboBoxItem.Content.ToString(); //выбранное значение в ComboBox

            if (peopleList != null && SelectedData != null)
            {
                int index = peopleList.IndexOf(SelectedData);
                EditData editData = new EditData(peopleList, SelectedData, index, _equalValue);
                editData.ShowDialog();
            }
        }

        /// <summary>
        /// Выбор клиента, чьи данные необходимо изменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridListPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Worker selected = dataGridListPerson.SelectedItem as Worker;
            if (selected != null)
                SelectedData = selected;
        }
    }
}
