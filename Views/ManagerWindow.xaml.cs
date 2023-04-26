using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Linq;

namespace Homework11_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private ObservableCollection<Manager> peopleList { get; set; }
        public ManagerWindow()
        {
            InitializeComponent();
            ShowPersonData();
        }

        /// <summary>
        /// Чтение xml файла
        /// </summary>
        private void ShowPersonData()
        {
            XDocument xDocument = XDocument.Load("people.xml");
            XElement? people = xDocument.Element("People");

            peopleList = new ObservableCollection<Manager>();
            int index = (from xelement in xDocument.Root.Descendants("Person") select xelement).Count(); //подсчет количества дочерних узлов в "Person"
            Manager[] manager = new Manager[index];

            if (!(people is null))
            {
                foreach (XElement person in people.Elements("Person"))
                {
                    for (int i = 0; i < 1; i++)
                    {
                        manager[i] = new Manager();
                        XElement? SurnamePersone = person.Element("Surname");
                        manager[i].Surname = SurnamePersone.Value;

                        XElement? FirstNamePersone = person.Element("FirstName");
                        manager[i].FirstName = FirstNamePersone.Value;

                        XElement? LastNamePersone = person.Element("LastName");
                        manager[i].LastName = LastNamePersone.Value;

                        XElement? PasportDataPersone = person.Element("PassportData");

                        if (PasportDataPersone.Value != "")
                        {
                            manager[i].PasportData = PasportDataPersone.Value;
                        }

                        XElement? PhoneNumberPersone = person.Element("MobilePhone");
                        if (PhoneNumberPersone.Value != "")
                        {
                            manager[i].PhoneNumber = double.Parse(PhoneNumberPersone.Value);
                        }

                        peopleList.Add(manager[i]);
                    }
                }
            }
            dataGridManagerPerson.ItemsSource = peopleList;
        }
    }

}
