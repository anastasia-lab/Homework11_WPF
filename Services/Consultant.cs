using System;
using System.Collections.Generic;
using System.Text;

namespace Homework11_WPF
{
    [Serializable]
   public class Consultant : Worker
   {
        string _pasportData;
        /// <summary>
        /// Срздание консультанта
        /// </summary>
        public Consultant()
        { }
        public Consultant(string pasport)
        { 
            _pasportData = pasport;
        }

        /// <summary>
        /// Просмотр паспортных данных
        /// </summary>
        /// <param name="_pasportData"> Паспортные данные </param>
        /// <returns></returns>
        public override string PasportData
        {
            get { return "*****"; }
        }
    }
}
