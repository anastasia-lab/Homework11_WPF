using Homework11_WPF.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework11_WPF
{
    [Serializable]
   public class Consultant : Worker, IClient
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
        /// Замена паспортных данных на *****
        /// </summary>
        /// <param name="_pasportData"> Паспортные данные </param>
        /// <returns></returns>
        public override string PasportData
        {
            get { return "*****"; }
        }
    }
}
