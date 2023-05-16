using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Homework11_WPF.Services
{
    interface IClient
    {
        public void GetChangePhoneNumber(double value, ObservableCollection<Worker> peopleList, int index);
    }
}
