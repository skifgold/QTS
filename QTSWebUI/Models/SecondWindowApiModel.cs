using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using QTSDataManager.DB;

namespace QTSWebUI.Models
{
    public class SecondWindowApiModel
    {

        public IEnumerable<string> GroupList { get; set; }
        public string NameOfConcert { get; set; }
        public string AdressOfConcert { get; set; }
        public string TypeOfPlace { get; set; }
        public string PosterURL { get; set; }
        public DateTime Date { get; set; }
        public object Fan { get; internal set; }
    }
}