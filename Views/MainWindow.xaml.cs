﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        //XDocument xDocument = XDocument.Load("person.xml");
        private Worker SelectedData { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ButtonAdd.Visibility = Visibility.Hidden;
            ButtonEdit.Visibility = Visibility.Hidden;
            buttonInformation.Visibility = Visibility.Hidden;
            ButtonDelete.Visibility = Visibility.Hidden;
            StackPanelLableInfo.Visibility = Visibility.Hidden;
            StackPanelTextBoxInfo.Visibility = Visibility.Hidden;
            dataGridListPerson.Visibility = Visibility.Hidden;
            BorderRound.Visibility = Visibility.Hidden;
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

            peopleList = new ObservableCollection<Worker>();

            if (_equalValue == "Консультант")
            {
                consultant.ReadXmlFile("person.xml", _equalValue, peopleList);
                dataGridListPerson.ItemsSource = peopleList;

                ButtonAdd.Visibility = Visibility.Hidden;
                ButtonEdit.Visibility = Visibility.Visible;
                buttonInformation.Visibility = Visibility.Visible;
                ButtonDelete.Visibility = Visibility.Hidden;
                StackPanelLableInfo.Visibility = Visibility.Visible;
                StackPanelTextBoxInfo.Visibility = Visibility.Visible;
                dataGridListPerson.Visibility = Visibility.Visible;
                BorderRound.Visibility = Visibility.Visible;
            }
            if (_equalValue == "Менеджер")
            {
                manager.ReadXmlFile("person.xml", _equalValue, peopleList);
                dataGridListPerson.ItemsSource = peopleList;

                ButtonAdd.Visibility = Visibility.Visible;
                ButtonEdit.Visibility = Visibility.Visible;
                buttonInformation.Visibility = Visibility.Visible;
                ButtonDelete.Visibility = Visibility.Visible;
                StackPanelLableInfo.Visibility = Visibility.Visible;
                StackPanelTextBoxInfo.Visibility = Visibility.Visible;
                dataGridListPerson.Visibility = Visibility.Visible;
                BorderRound.Visibility = Visibility.Visible;
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
                int recordIndex = peopleList.IndexOf(SelectedData);
                EditData editData = new EditData(peopleList, SelectedData, recordIndex, _equalValue);
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

        /// <summary>
        /// Удаление данных клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedData != null)
            {
                Manager managerDelete = new Manager(SelectedData);
                try
                {
                    managerDelete.GetSaveTxtEditData("Менеджер", "Удаление", "Клиент: ", managerDelete.Surname + " " +
                                                      managerDelete.FirstName + " " + managerDelete.LastName, "");
                    peopleList.Remove(SelectedData);
                    managerDelete.SaveXmlFile(peopleList);

                    MessageBox.Show($"Данные {SelectedData.Surname}" + " " +
                                    $"{ SelectedData.FirstName + " " + SelectedData.LastName} удалены",
                                    "Удалить", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void buttonInformation_Click(object sender, RoutedEventArgs e)
        {
            InformationData informationData = new InformationData();
            informationData.ShowDialog();
        }
    }
}
