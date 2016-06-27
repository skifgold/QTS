using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTSDataManager.DB
{
    [MetadataType(typeof(CustomerMetadataType))]
    partial class Customer
    {
        public int Count { get; set; }
        public string Fan { get; set; }
        public int ConcertId { get; set; }
        public string TicketType { get; set; }

    }

    public class CustomerMetadataType
    {
        [DisplayName("CountTicket")]
        public int Count { get; set; }
    }
}
