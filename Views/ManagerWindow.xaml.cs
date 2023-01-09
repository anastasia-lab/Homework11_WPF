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

namespace Homework11_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ObservableCollection<Manager> peopleList { get; set; }
        public ManagerWindow()
        {
            InitializeComponent();

            GetShowDataPerson();
        }

        /// <summary>
        /// вывод данных в DataGrid
        /// </summary>
        private void GetShowDataPerson()
        {
            GetReadXmlFile();
            dataGridPerson.ItemsSource = peopleList;
        }

        /// <summary>
        /// Чтение xml файла
        /// </summary>
        private void GetReadXmlFile()
        {
            peopleList = new ObservableCollection<Manager>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("people.xml");
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    Manager[] manager = new Manager[xnode.ChildNodes.Count];

                    for (int i = 0; i < xnode.ChildNodes.Count; i++)
                    {
                        manager[i] = new Manager();
                        string childnode = xnode.ChildNodes[i].Name;

                        if (childnode == "Surname")
                            manager[i].Surname = xnode.ChildNodes[i].InnerText;

                        if (childnode == "FirstName")
                            manager[i].FirstName = xnode.ChildNodes[i].InnerText;

                        if (childnode == "LastName")
                            manager[i].LastName = xnode.ChildNodes[i].InnerText;

                        if (childnode == "MobilePhone")
                        {
                            if (xnode.ChildNodes[i].InnerText != "")
                                manager[i].PhoneNumber = double.Parse(xnode.ChildNodes[i].InnerText);
                        }

                        if (childnode == "PassportData")
                        {
                            if (xnode.ChildNodes[i].InnerText != "")
                                manager[i].GetPassportData(xnode.ChildNodes[i].InnerText);
                        }
                    }

                    foreach (Manager clientData in manager)
                        peopleList.Add(clientData);
                }
            }
        
        }
    }

}
