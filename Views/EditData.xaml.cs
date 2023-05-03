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
        }

        public EditData(ObservableCollection<Worker> peopleList, Worker worker, int index, string _equalValue)
        {
            InitializeComponent();
            this.peopleList = peopleList;
            this.worker = worker;
            this.index = index;
            this.equalValue = _equalValue;
            GetShowSelectData();
        }
        #endregion

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (peopleList != null && worker != null)
                GetEditData();

            if (peopleList != null && worker == null)
                GetAddNewData();

            this.Close();
        }

        #region Методы
        /// <summary>
        /// Загрузка данных
        /// </summary>
        private void GetShowSelectData()
        {
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
            XElement save = xDoc.Element("People").Elements("Person").Where(e => e.Element("Surname").Value == textBoxSurname.Text).Single();

            if (equalValue == "Консультант")
            {
                if (peopleList != null && worker != null)
                {
                    worker.PhoneNumber = double.Parse(textBoxPhoneNumber.Text);
                    save.Element("MobilePhone").Value = worker.GetChangePhoneNumber(double.Parse(textBoxPhoneNumber.Text)).ToString();
                }
            }

            if (equalValue == "Менеджер")
            {
                if (peopleList != null && worker != null)
                {
                    worker.Surname = textBoxSurname.Text;
                    worker.FirstName = textBoxFirstName.Text;
                    worker.LastName = textBoxLastName.Text;
                    worker.PasportData = textBoxPassportData.Text;
                    worker.PhoneNumber = double.Parse(textBoxPhoneNumber.Text);
                }
            }

            save.Save("people.xml");
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
            manager.GetSaveNewClient(xDoc);
        }
        #endregion
    }
}
