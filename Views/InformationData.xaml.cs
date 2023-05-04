using Homework11_WPF.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Homework11_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для InformationData.xaml
    /// </summary>
    public partial class InformationData : Window
    {
        public InformationData()
        {
            InitializeComponent();
            GetReadTxtFile("EditedData.txt");
        }

        private void GetReadTxtFile(string path)
        {
            string[] SplitString = null; // Массив для хранения строк, разделенных знаком '#'

            string[] ArrayFile = File.ReadAllLines(path); // Хранение данных из файла
            ReadDataInTxt[] readData = new ReadDataInTxt[ArrayFile.Length];

            for (int i = 0; i < ArrayFile.Length; i++)
            {
                SplitString = ArrayFile[i].Split('#');
                readData[i] = new ReadDataInTxt();
                readData[i].DateTimeChanged = DateTime.Parse(SplitString[0]);
                readData[i].WhoChanged = SplitString[1];
                readData[i].TypeOfChange = SplitString[2];
                readData[i].WhatChanged = SplitString[3];
            }

            listLoadedData.ItemsSource = readData;
        }
    }
}
