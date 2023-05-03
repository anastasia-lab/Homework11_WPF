using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;

namespace Homework11_WPF
{
   public class Manager : Worker
   {
        #region Конструкторы
        public Manager()
        {}

        public Manager(string surname, string firstname, string lastname, string pasportdata, double phonenumber)
        {
            this.Surname = surname;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.PasportData = pasportdata;
            this.PhoneNumber = phonenumber;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Добавление в коллекцию нового клиента
        /// </summary>
        /// <param name="peopleList">Коллекция клиентов</param>
        /// <param name="manager">Менеджер</param>
        public void GetAddDataOfManager(ObservableCollection<Worker> peopleList, Manager manager)
        {
            peopleList.Add(manager);
        }

        /// <summary>
        /// Сохранение нового клиента в xml-документ
        /// </summary>
        /// <param name="xDoc">путь к документу</param>
        public void GetSaveNewClient(XDocument xDoc)
        {
            XElement? people = xDoc.Element("People");
            if (people != null)
            {
                people.Add(new XElement("Person",
                    new XElement("Surname", Surname),
                    new XElement("FirstName", FirstName),
                    new XElement("LastName", LastName),
                    new XElement("PassportData", PasportData),
                    new XElement("MobilePhone", PhoneNumber)));

                xDoc.Save("people.xml");
            }
        }
        #endregion
    }
}
