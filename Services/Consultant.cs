using System;
using System.Collections.Generic;
using System.Text;

namespace Homework11_WPF
{
   public class Consultant : Worker
   {
        /// <summary>
        /// Срздание консультанта
        /// </summary>
        public Consultant()
        { }

        /// <summary>
        /// Просмотр паспортных данных
        /// </summary>
        /// <param name="_passportData"> Паспортные данные </param>
        /// <returns></returns>
        public override string PassportData(string _passportData)
        {
            if(_passportData != "")
                return "*********";

            return _passportData;
        }
   }
}
