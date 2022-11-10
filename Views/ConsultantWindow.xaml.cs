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
using System.Xml.Linq;


namespace Homework11_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ConsultantWindow.xaml
    /// </summary>
    public partial class ConsultantWindow : Window
    {
        public ConsultantWindow()
        {
            InitializeComponent();
            ShowPersonData();
        }

        void ShowPersonData()
        {
            Consultant consultant = new Consultant();
            string PassportDataStringResult = "";
            double PhoneNumberResult = 0;

            XDocument xDocument = XDocument.Load("people.xml");
            XElement? people = xDocument.Element("People");

            List<Consultant> peopleList = new List<Consultant>();

            if (!(people is null))
            {
                foreach (XElement person in people.Elements("Person"))
                {
                    XElement? SurnamePersone = person.Element("Surname");
                    consultant.Surname = SurnamePersone.Value;

                    XElement? FirstNamePersone = person.Element("FirstName");
                    consultant.FirstName = FirstNamePersone.Value;

                    XElement? LastNamePersone = person.Element("LastName");
                    consultant.LastName = LastNamePersone.Value;

                    XElement? PassportDataPersone = person.Element("PassportData");

                    if (PassportDataPersone.Value != "")
                    {
                        PassportDataStringResult = consultant.PassportData(PassportDataPersone.Value);
                    }
                    XElement? PhoneNumberPersone = person.Element("MobilePhone");
                    if (PhoneNumberPersone.Value != "")
                    {
                        consultant.PhoneNumber = double.Parse(PhoneNumberPersone.Value);
                        PhoneNumberResult = consultant.PhoneNumber;
                    }
                }

                peopleList.Add(consultant);
            }

            textBoxSurname.Text = consultant.Surname;
            textBoxFirstName.Text = consultant.FirstName;
            textBoxLastName.Text = consultant.LastName;
            textBoxPassportData.Text = PassportDataStringResult;

            if (PhoneNumberResult == 0)
            {
                labelError.Content = "Введите номер телефона";
                labelError.Foreground = Brushes.Red;
            }
            else textBoxPhoneNumber.Text = consultant.PhoneNumber.ToString();
        }
    }
}
