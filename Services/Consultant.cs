using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Homework11_WPF
{
    [Serializable]
   public class Consultant : Worker
   {
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
        }
    }
}
