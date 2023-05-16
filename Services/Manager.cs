using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;

namespace Homework11_WPF
{
    [Serializable]
    public class Manager : Worker
    {
        #region Конструкторы

        public Manager() { }

        public Manager(Worker worker)
        {
            this.Surname = worker.Surname;
            this.FirstName = worker.FirstName;
            this.LastName = worker.LastName;
            this.PasportData = worker.PasportData;
            this.PhoneNumber = worker.PhoneNumber;
        }

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
            SaveXmlFile(peopleList);
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

        /// <summary>
        /// Удаление данных клиента
        /// </summary>
        /// <param name="peopleList"></param>
        /// <param name="xDocument"></param>
        /// <param name="worker"></param>
        public void GetDeleteDataClient(ObservableCollection<Worker> peopleList, XDocument xDoc, Worker worker)
        {
            xDoc.Root.Element("People").Element(worker.ToString()).Remove();
            peopleList.Remove(worker);
        }
        #endregion
    }
}
