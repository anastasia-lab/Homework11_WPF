using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace Homework11_WPF
{
    [Serializable]
   public class Consultant : Worker
   {
        string passportData;
        /// <summary>
        /// Создание консультанта
        /// </summary>
        public Consultant()
        {}

        /// <summary>
        /// Замена паспортных данных
        /// </summary>
        /// <param name="_pasportData"> Паспортные данные </param>
        /// <returns></returns>
        public override string PasportData
        {
            get { return "*****"; }
            //set { passportData = value; }
        }
    }
}
