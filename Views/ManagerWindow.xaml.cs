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

namespace Homework11_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            DataContext = this;
            ShowDataManager();
        }

       public ObservableCollection<Manager> peopleList { get; set; }
        void ShowDataManager()
        {
            Manager manager = new Manager();
            string PassportDataStringResult = "";
            double PhoneNumberResult = 0;
            peopleList = new ObservableCollection<Manager>();

            XDocument xDocument = XDocument.Load("people.xml");
            XElement? people = xDocument.Element("People");

            if (!(people is null))
            {
                foreach (XElement person in people.Elements("Person"))
                {
                    XElement? SurnamePersone = person.Element("Surname");
                    manager.Surname = SurnamePersone.Value;

                    XElement? FirstNamePersone = person.Element("FirstName");
                    manager.FirstName = FirstNamePersone.Value;

                    XElement? LastNamePersone = person.Element("LastName");
                    manager.LastName = LastNamePersone.Value;

                    XElement? PassportDataPersone = person.Element("PassportData");

                    if (PassportDataPersone.Value != "")
                    {
                        PassportDataStringResult = manager.PassportData(PassportDataPersone.Value);
                    }
                    XElement? PhoneNumberPersone = person.Element("MobilePhone");
                    if (PhoneNumberPersone.Value != "")
                    {
                        manager.PhoneNumber = double.Parse(PhoneNumberPersone.Value);
                        PhoneNumberResult = manager.PhoneNumber;
                    }

                    peopleList.Add(manager);
                }

            }

            //textBoxSurname.Text = manager.Surname;
            //textBoxFirstName.Text = manager.FirstName;
            //textBoxLastName.Text = manager.LastName;
            //textBoxPassportData.Text = PassportDataStringResult;

            //if (PhoneNumberResult == 0)
            //{
            //    labelError.Content = "Введите номер телефона";
            //    labelError.Foreground = Brushes.Red;
            //}
            //else textBoxPhoneNumber.Text = manager.PhoneNumber.ToString();
        }

        private void listViewPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
