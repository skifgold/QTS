using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace QTSWebUI.Controllers
{
    public class CreateTicketViewModel
    {
        [Browsable(false)]
        public int ConcertId { get; set; }

        [DisplayName("Ticket type")]
        public string TicketType { get; set; }

        [DisplayName("Price")]
        public int Price { get; set; }

        [DisplayName("Fan")]
        public string Fan { get; set; }

        [Browsable(false)]
        public IEnumerable<string> AvailableFans { get; set; }

        [DisplayName("Count")]
        public int Count { get; set; }
    }
}