using Homework11_WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Homework11_WPF
{
    //[XmlInclude(typeof(Manager)), XmlInclude(typeof(Consultant))]
    [Serializable]
    public class Worker: IClient
    {
        #region Поля
        double _phoneNumber;
        string _passportData;
        #endregion

        #region Свойства
        public virtual string Surname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual double PhoneNumber 
        { 
            get => this._phoneNumber;
            set
            {
                if (value.ToString() != "")
                {
                    if (value.ToString().Length >= 10)
                        this._phoneNumber = value;
                }
            }
        }
        public virtual string PasportData 
        { 
            get => this._passportData;
            set
            {
                if (value != null)
                {
                    if (value.Length >= 10)
                        _passportData = value;
                }
            }
        }
        #endregion

        #region Методы

        /// <summary>
        /// Чтение Xml-документа
        /// </summary>
        /// <param name="path">Название xml-документ</param>
        /// <param name="equalValue">Вид пользователя (консультант или менеджер)</param>
        /// <param name="peopleList">коллекция данных xml-документа</param>
        /// <returns></returns>
        internal ObservableCollection<Worker> ReadXmlFile(string path, string equalValue, ObservableCollection<Worker> peopleList)
        {
            bool resultConsultant = equalValue == "Консультант";
            bool resultManager = equalValue == "Менеджер";

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worker[]));

                using (FileStream reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Worker[] workers = xmlSerializer.Deserialize(reader) as Worker[];
                    Consultant[] consultant = new Consultant[workers.Length];
                    Manager[] manager = new Manager[workers.Length];

                    if (workers != null)
                    {
                        foreach (Worker worker in workers)
                        {
                            for (int i = 0; i < 1; i++)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return peopleList;
        }

        /// <summary>
        /// Сохранение xml-файл
        /// </summary>
        /// <param name="peopleList">коллекция данных для xml-документа</param>
        /// <returns></returns>
        internal void SaveXmlFile(ObservableCollection<Worker> peopleList)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worker[]));
            Worker[] workers = peopleList.ToArray();

            using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, workers);
            }
        }

        /// <summary>
        /// Изменение номера телефона
        /// </summary>
        /// <param name="value"> Новый номер телефона</param>
        public void GetChangePhoneNumber(double value, ObservableCollection<Worker> peopleList, int index)
        {
            peopleList[index].PhoneNumber = value;
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
        #endregion

    }
}
