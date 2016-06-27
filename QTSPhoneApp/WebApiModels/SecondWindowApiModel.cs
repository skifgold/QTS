using System;
using System.Collections.Generic;

namespace QTSPhoneApp.WebApiModels
{
    public class SecondWindowApiModel
    {
        public List<string> GroupList { get; set; }
        public string NameOfConcert { get; set; }
        public string AdressOfConcert { get; set; }
        public string TypeOfPlace { get; set; }
        public string PosterURL { get; set; }
        public DateTime Date { get; set; }
        public object Fan { get; internal set; }
    }
}