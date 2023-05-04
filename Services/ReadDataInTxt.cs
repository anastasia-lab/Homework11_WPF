using System;
using System.Collections.Generic;
using System.Text;

namespace Homework11_WPF.Services
{
    public class ReadDataInTxt
    {
        public ReadDataInTxt() { }

        public string WhoChanged {get; set;}
        public DateTime DateTimeChanged { get; set; }
        public string WhatChanged { get; set; }
        public string TypeOfChange { get; set; }
    }
}
