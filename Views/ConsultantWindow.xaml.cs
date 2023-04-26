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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace Homework11_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ConsultantWindow.xaml
    /// </summary>
    public partial class ConsultantWindow : Window
    {
        private ObservableCollection<Consultant> peopleList { get; set; }

        public ConsultantWindow()
        {
            InitializeComponent();
            ShowPersonData();
        }

       private void ShowPersonData()
       {
            XDocument xDocument = XDocument.Load("people.xml");
            XElement? people = xDocument.Element("People");

            peopleList = new ObservableCollection<Consultant>();
            int index = (from xelement in xDocument.Root.Descendants("Person") select xelement).Count(); //подсчет количества дочерних узлов в "Person"
            Consultant[] consultant = new Consultant[index];

            if (!(people is null))
            {
                foreach (XElement person in people.Elements("Person"))
                {
                    for (int i = 0; i < 1; i++)
                    {
                        consultant[i] = new Consultant();
                        XElement? SurnamePersone = person.Element("Surname");
                        consultant[i].Surname = SurnamePersone.Value;

                        XElement? FirstNamePersone = person.Element("FirstName");
                        consultant[i].FirstName = FirstNamePersone.Value;

                        XElement? LastNamePersone = person.Element("LastName");
                        consultant[i].LastName = LastNamePersone.Value;

                        XElement? PassportDataPersone = person.Element("PassportData");

                        if (PassportDataPersone.Value != "")
                        {
                            consultant[i].PasportData = PassportDataPersone.Value;
                        }

                        XElement? PhoneNumberPersone = person.Element("MobilePhone");
                        if (PhoneNumberPersone.Value != "")
                        {
                            consultant[i].PhoneNumber = double.Parse(PhoneNumberPersone.Value);
                        }

                        peopleList.Add(consultant[i]);
                    }
                }
            }
            dataGridConsultantPerson.ItemsSource = peopleList;
        }
    }
}
