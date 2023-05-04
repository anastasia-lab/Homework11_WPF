using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using Microsoft.Win32;

namespace Homework11_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для EditData.xaml
    /// </summary>
    public partial class EditData : Window
    {
        XDocument xDoc = XDocument.Load("people.xml");
        ObservableCollection<Worker> peopleList;
        Worker worker;
        int index;
        string equalValue; //выбранное значение в ComboBox

        #region Конструкторы
        public EditData(ObservableCollection<Worker> _peopleList)
        {
            InitializeComponent();
            this.peopleList = _peopleList;

            checkBoxSurname.Visibility = Visibility.Hidden;
            checkBoxFirstName.Visibility = Visibility.Hidden;
            checkBoxLastName.Visibility = Visibility.Hidden;
            checkBoxPasportData.Visibility = Visibility.Hidden;
            checkBoxPhoneNumber.Visibility = Visibility.Hidden;
            lableInfo.Visibility = Visibility.Hidden;
        }

        public EditData(ObservableCollection<Worker> peopleList, Worker worker, int index, string _equalValue)
        {
            InitializeComponent();
            this.peopleList = peopleList;
            this.worker = worker;
            this.index = index;
            this.equalValue = _equalValue;

            checkBoxSurname.Visibility = Visibility.Hidden;
            checkBoxFirstName.Visibility = Visibility.Hidden;
            checkBoxLastName.Visibility = Visibility.Hidden;
            checkBoxPasportData.Visibility = Visibility.Hidden;
            checkBoxPhoneNumber.Visibility = Visibility.Hidden;
            lableInfo.Visibility = Visibility.Hidden;

            GetShowSelectData(equalValue);
        }
        #endregion

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (peopleList != null && worker != null)
            {
                GetEditData();
            }

            if (peopleList != null && worker == null)
                GetAddNewData();

            this.Close();
        }

        #region Методы
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="EqualsValue">выбранное значение в ComboBox(консультант или менеджер)</param>
        private void GetShowSelectData(string EqualsValue)
        {
            if (EqualsValue == "Консультант")
            {
                checkBoxPhoneNumber.Visibility = Visibility.Visible;
                lableInfo.Visibility = Visibility.Visible;
                textBoxSurname.IsReadOnly = true;
                textBoxFirstName.IsReadOnly = true;
                textBoxLastName.IsReadOnly = true;
                textBoxPassportData.IsReadOnly = true;
            }
            if (EqualsValue == "Менеджер")
            {
                checkBoxSurname.Visibility = Visibility.Visible;
                checkBoxFirstName.Visibility = Visibility.Visible;
                checkBoxLastName.Visibility = Visibility.Visible;
                checkBoxPasportData.Visibility = Visibility.Visible;
                checkBoxPhoneNumber.Visibility = Visibility.Visible;
                lableInfo.Visibility = Visibility.Visible;
            }

            textBoxSurname.Text = worker.Surname;
            textBoxFirstName.Text = worker.FirstName;
            textBoxLastName.Text = worker.LastName;
            textBoxPassportData.Text = worker.PasportData;
            textBoxPhoneNumber.Text = worker.PhoneNumber.ToString();
        }

        /// <summary>
        /// Редактирование элементов
        /// </summary>
        private void GetEditData()
        {
            //XElement save = xDoc.Element("People").Elements("Person").Where(e => e.Element("Surname").Value == textBoxSurname.Text).Single();

            if (equalValue == "Консультант")
            {
                if (peopleList != null && worker != null)
                {
                    if ((bool)checkBoxPhoneNumber.IsChecked) 
                    {
                        string oldInfo = worker.PhoneNumber.ToString(); //старая информация при редактировании

                        worker.GetChangePhoneNumber(double.Parse(textBoxPhoneNumber.Text));
                        worker.GetSaveTxtEditData(equalValue,"редактирование",checkBoxPhoneNumber.Content.ToString(), 
                                                  oldInfo, worker.PhoneNumber.ToString());
                    } 

                    //save.Element("MobilePhone").Value = worker.GetChangePhoneNumber(double.Parse(textBoxPhoneNumber.Text)).ToString();
                }
            }

            if (equalValue == "Менеджер")
            {
                if (peopleList != null && worker != null)
                {
                    string oldInfo; //старая информация при редактировании
                    if ((bool)checkBoxSurname.IsChecked)
                    {
                        oldInfo = worker.Surname;
                        worker.GetEditSurName(textBoxSurname.Text);
                        worker.GetSaveTxtEditData(equalValue, "редактирование", checkBoxSurname.Content.ToString(),
                            oldInfo, worker.Surname);
                    }
                    if ((bool)checkBoxFirstName.IsChecked)
                    {
                        oldInfo = worker.FirstName;
                        worker.GetEditFirstName(textBoxFirstName.Text);
                        worker.GetSaveTxtEditData(equalValue, "редактирование", checkBoxFirstName.Content.ToString(),
                            oldInfo, worker.FirstName);
                    }
                    if ((bool)checkBoxLastName.IsChecked)
                    {
                        oldInfo = worker.LastName;
                        worker.GetEditLastName(textBoxLastName.Text);
                        worker.GetSaveTxtEditData(equalValue, "редактирование", checkBoxLastName.Content.ToString(),
                            oldInfo, worker.LastName);
                    }
                    if ((bool)checkBoxPasportData.IsChecked)
                    {
                        oldInfo = worker.PasportData;
                        worker.GetEditPasportData(textBoxPassportData.Text);
                        worker.GetSaveTxtEditData(equalValue, "редактирование", checkBoxPasportData.Content.ToString(),
                            oldInfo, worker.PasportData);
                    }
                    if ((bool)checkBoxPhoneNumber.IsChecked)
                    {
                        oldInfo = worker.PhoneNumber.ToString();
                        worker.GetEditPhoneNumber(textBoxPhoneNumber.Text);
                        worker.GetSaveTxtEditData(equalValue, "редактирование", checkBoxPhoneNumber.Content.ToString(),
                            oldInfo, worker.PhoneNumber.ToString());
                    }
                }
            }

            peopleList.RemoveAt(index);
            peopleList.Insert(index, worker);
            //save.Save("people.xml");
        }

        /// <summary>
        /// Добавление элемента
        /// </summary>
        private void GetAddNewData()
        {
            string _Surname = textBoxSurname.Text;
            string _FirstName = textBoxFirstName.Text;
            string _LastName = textBoxLastName.Text;
            string _PasportData = textBoxPassportData.Text;
            double _PhoneNumber = double.Parse(textBoxPhoneNumber.Text);

            Manager manager = new Manager(_Surname, _FirstName, _LastName, _PasportData, _PhoneNumber);

            manager.GetAddDataOfManager(peopleList, manager);
            //manager.GetSaveNewClient(xDoc);
            manager.GetSaveTxtEditData("Менеджер","добавление","Новый клиент", $"{manager.Surname}" + " " + 
                                        $"{manager.FirstName}" + " " + $"{manager.LastName}","");
        }
        #endregion
    }
}
