using Homework11_WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Homework11_WPF
{
    public class Worker: IClient
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
        internal ObservableCollection<Worker> ReadXmlFile(XDocument xDocument, string equalValue, ObservableCollection<Worker> peopleList)
        {
            XElement? people = xDocument.Element("People");
            int index = (from xelement in xDocument.Root.Descendants("Person") select xelement).Count(); //подсчет количества дочерних узлов в "Person"
            
            bool resultConsultant = equalValue == "Консультант";
            bool resultManager = equalValue == "Менеджер";
            Consultant[] consultant = new Consultant[index];
            Manager[] manager = new Manager[index];

            #region XElement
            //foreach (XElement person in people.Elements("Person"))
            //{
            //    for (int i = 0; i < 1; i++)
            //    {
            //        XElement? SurnamePersone = person.Element("Surname");
            //        XElement? FirstNamePersone = person.Element("FirstName");
            //        XElement? LastNamePersone = person.Element("LastName");
            //        XElement? PassportDataPersone = person.Element("PassportData");
            //        XElement? PhoneNumberPersone = person.Element("MobilePhone");

            //        if (resultConsultant == true)
            //        {
            //            consultant[i] = new Consultant();
            //            consultant[i].Surname = SurnamePersone.Value;
            //            consultant[i].FirstName = FirstNamePersone.Value;
            //            consultant[i].LastName = LastNamePersone.Value;
            //            if (PassportDataPersone.Value != "")
            //                consultant[i].PasportData = PassportDataPersone.Value;
            //            if (PhoneNumberPersone.Value != "")
            //                consultant[i].PhoneNumber = double.Parse(PhoneNumberPersone.Value);
            //            peopleList.Add(consultant[i]);
            //        }

            //        if (resultManager == true)
            //        {
            //            manager[i] = new Manager();
            //            manager[i].Surname = SurnamePersone.Value;
            //            manager[i].FirstName = FirstNamePersone.Value;
            //            manager[i].LastName = LastNamePersone.Value;
            //            if (PassportDataPersone.Value != "")
            //                manager[i].PasportData = PassportDataPersone.Value;
            //            if (PhoneNumberPersone.Value != "")
            //                manager[i].PhoneNumber = double.Parse(PhoneNumberPersone.Value);
            //            peopleList.Add(manager[i]);
            //        }
            //    }
            //}
            #endregion

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worker[]));

            #region Deserialize
            using (var reader = new StringReader(xDocument.ToString()))
            {
                Worker[] workers = xmlSerializer.Deserialize(reader) as Worker[];

                if (workers != null)
                {
                    foreach (Worker worker in workers)
                    {
                        for (int i = 0; i < workers.Length; i++)
                        {
                            if (resultConsultant == true)
                            {
                                consultant[i] = new Consultant();
                                consultant[i].Surname = worker.Surname;
                                consultant[i].FirstName = worker.FirstName;
                                consultant[i].LastName = worker.LastName;
                                if (worker.PasportData != "")
                                    consultant[i].PasportData = worker.PasportData;
                                if (worker.PhoneNumber.ToString() != "")
                                    consultant[i].PhoneNumber = worker.PhoneNumber;
                                peopleList.Add(consultant[i]);
                            }

                            if (resultManager == true)
                            {
                                manager[i] = new Manager();
                                manager[i].Surname = worker.Surname;
                                manager[i].FirstName = worker.FirstName;
                                manager[i].LastName = worker.LastName;
                                if (worker.PasportData != "")
                                    manager[i].PasportData = worker.PasportData;
                                if (worker.PhoneNumber.ToString() != "")
                                    manager[i].PhoneNumber = worker.PhoneNumber;
                                peopleList.Add(manager[i]);
                            }
                        }
                    }
                }
            }
            #endregion

                return peopleList;
        }

        /// <summary>
        /// Изменение номера телефона
        /// </summary>
        /// <param name="value"> Новый номер телефона</param>
        public double GetChangePhoneNumber(double value)
        {
            PhoneNumber = value;
            return PhoneNumber;
        }

        /// <summary>
        /// Сохранение измененных данных в отдельном txt-файле
        /// </summary>
        /// <param name="value">Кто изменил(менеджер или консультант)</param>
        /// <param name="infoAboutEdit">Информация об изменении (редактирование/удаление/добавление)</param>
        /// <param name="infoWhatEdit">Что именно изменили (Имя/Фамилию/Отчество/Номер телефона/Паспорт)</param>
        internal void GetSaveTxtEditData(string value, string infoAboutEdit, string infoWhatEdit, string oldInformation, string newInformation)
        {
            string path = "EditedData.txt";
            Encoding conding = Encoding.UTF8;

            string ReadLineFile = ""; //чтение отдельных строк из файла

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            using (StreamReader streamReader = new StreamReader(path, conding))
            {
                while (!streamReader.EndOfStream)
                {
                    ReadLineFile = streamReader.ReadLine();
                }
            }

            using (StreamWriter streamWriter = new StreamWriter(path, true, conding))
            {
                streamWriter.WriteLine($"{DateTime.Now}#{value}#{infoAboutEdit}#{infoWhatEdit}:" +
                    $"{oldInformation} -> {newInformation}");
            }
        }

        /// <summary>
        /// Редактирование фамилии
        /// </summary>
        /// <param name="newSurName">Новая фамилия</param>
        internal void GetEditSurName(string newSurName)
        {
            this.Surname = newSurName;
        }

        /// <summary>
        /// Редактирование имени
        /// </summary>
        /// <param name="newFirstName">Новое имя</param>
        internal void GetEditFirstName(string newFirstName)
        {
            this.FirstName = newFirstName;
        }

        /// <summary>
        /// Редактирование отчества
        /// </summary>
        /// <param name="newLastName">Новое отчество</param>
        internal void GetEditLastName(string newLastName)
        {
            this.LastName = newLastName;
        }

        /// <summary>
        /// Редактирование паспортных данных
        /// </summary>
        /// <param name="newPasportData">Новые паспортные данные</param>
        internal void GetEditPasportData(string newPasportData)
        {
            this.PasportData = newPasportData;
        }

        /// <summary>
        /// Редактирование номера телефона
        /// </summary>
        /// <param name="newPhoneNumber">Новый номер телефона</param>
        internal void GetEditPhoneNumber(string newPhoneNumber)
        {
            this.PhoneNumber = double.Parse(newPhoneNumber);
        }

    }
}
