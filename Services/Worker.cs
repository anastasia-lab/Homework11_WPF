using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Homework11_WPF
{
    public class Worker
    {
        public virtual string Surname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual double PhoneNumber { get; set; }
        public virtual string PasportData { get; set; }

        /// <summary>
        /// Чтение Xml-документа
        /// </summary>
        /// <param name="xDocument">Xml-документ</param>
        /// <param name="equalValue">Вид пользователя (консультант или менеджер)</param>
        /// <param name="peopleList">коллекция данных xml-документа</param>
        /// <returns></returns>
        public ObservableCollection<Worker> ReadXmlFile(XDocument xDocument, string equalValue, ObservableCollection<Worker> peopleList)
        {
            XElement? people = xDocument.Element("People");
            int index = (from xelement in xDocument.Root.Descendants("Person") select xelement).Count(); //подсчет количества дочерних узлов в "Person"
            
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

            return peopleList;
        }
    }
}
