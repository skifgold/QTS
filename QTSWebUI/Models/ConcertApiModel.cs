using System;

namespace QTSWebUI.Models
{
    public class ConcertApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TypeOfPlace { get; set; }
        public DateTime Date { get; set; }
        public string PosterUrl { get; set; }
    }
}