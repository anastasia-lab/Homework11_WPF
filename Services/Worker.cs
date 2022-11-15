using System;
using System.Collections.Generic;
using System.Text;

namespace Homework11_WPF
{
    public class Worker
    {
        public virtual string Surname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual double PhoneNumber { get; set; }
        public virtual string PassportData(string _passportData) 
        {
            return _passportData;
        }
    }
}
