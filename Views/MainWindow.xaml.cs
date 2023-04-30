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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void comboBoxChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem _comboBoxItem = (ComboBoxItem)comboBoxChoice.SelectedItem;
            string _equalValue = _comboBoxItem.Content.ToString();

            XDocument xDocument = XDocument.Load("people.xml");

            if (_equalValue == "Консультант")
            {
                ReadXmlFile(xDocument,_equalValue);
            }
            if (_equalValue == "Менеджер")
            {
                ReadXmlFile(xDocument,_equalValue);
            }
        }

        private void ReadXmlFile(XDocument xDocument, string equalValue)
        {
            XElement? people = xDocument.Element("People");

            peopleList = new ObservableCollection<Worker>();
            int index = (from xelement in xDocument.Root.Descendants("Person") select xelement).Count(); //подсчет количества дочерних узлов в "Person"

            if (!(people is null))
            {
                bool resultConsultant = equalValue == "Консультант";
                bool resultManager = equalValue == "Менеджер";
                Consultant[] consultant = new Consultant[index];
                Manager[] manager = new Manager[index];

                foreach (XElement person in people.Elements("Person"))
                {
                    for (int i = 0; i < 1; i++)
                    {
                        XElement? SurnamePersone = person.Element("Surname");
                        XElement? FirstNamePersone = person.Element("FirstName");
                        XElement? LastNamePersone = person.Element("LastName");
                        XElement? PassportDataPersone = person.Element("PassportData");
                        XElement? PhoneNumberPersone = person.Element("MobilePhone");

                        if (resultConsultant == true)
                        {
                            consultant[i] = new Consultant();
                            consultant[i].Surname = SurnamePersone.Value;
                            consultant[i].FirstName = FirstNamePersone.Value;
                            consultant[i].LastName = LastNamePersone.Value;
                            if (PassportDataPersone.Value != "")
                                consultant[i].PasportData = PassportDataPersone.Value;
                            if (PhoneNumberPersone.Value != "")
                                consultant[i].PhoneNumber = double.Parse(PhoneNumberPersone.Value);
                            peopleList.Add(consultant[i]);
                        }

                        if (resultManager == true)
                        {
                            manager[i] = new Manager();
                            manager[i].Surname = SurnamePersone.Value;
                            manager[i].FirstName = FirstNamePersone.Value;
                            manager[i].LastName = LastNamePersone.Value;
                            if (PassportDataPersone.Value != "")
                                manager[i].PasportData = PassportDataPersone.Value;
                            if (PhoneNumberPersone.Value != "")
                                manager[i].PhoneNumber = double.Parse(PhoneNumberPersone.Value);
                            peopleList.Add(manager[i]);
                        }
                    }
                }
            }
            dataGridListPerson.ItemsSource = peopleList;
        }
    }
}
