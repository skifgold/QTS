using System;
using QTSPhoneApp.WebApiModels;

namespace QTSPhoneApp
{
    public sealed class NavigationContext
    {
        public int Id { get; set; }
        public string FanZone { get; set; }
        public string TypeOfTickets { get; set; }
        public string Name { get; set; }
        public DateTime Address { get; set; }
        public string Poster { get; set; }
        public int Price { get; set; }
        public int TicketStatus { get; set; }
        public int TicketsCount { get; set; }
        public Customer Content { get; set; }
    }
}