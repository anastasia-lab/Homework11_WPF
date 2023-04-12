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

namespace Homework11_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ConsultantWindow.xaml
    /// </summary>
    public partial class ConsultantWindow : Window
    {
        ObservableCollection<Consultant> peopleList { get; set; }
        ObservableCollection<Consultant> collection { get; set; }

        public ConsultantWindow()
        {
            InitializeComponent();
            ShowPersonData();
        }

        void ShowPersonData()
        {
            collection = new ObservableCollection<Consultant>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Consultant[]));
            using (FileStream fs = new FileStream("people.xml", FileMode.Open))
            {
                Consultant[] consultants = (Consultant[])xmlSerializer.Deserialize(fs);

                if (consultants != null)
                {
                    for (int i = 0; i < consultants.Length; i++)
                    {
                        foreach (Consultant consultant in consultants)
                        {
                            consultants[i].FirstName = consultant.FirstName;
                        }
                    }
                }

                foreach (Consultant consultant1 in consultants)
                    collection.Add(consultant1);

                dataGridConsultantPerson.ItemsSource = collection;
                
            }
        }
    }
}
