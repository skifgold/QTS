using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QTSDataManager.DB
{
    [MetadataType(typeof(TicketMetadataType))]
    public partial class Ticket
    {
        public string Customer
        {
            get
            {
                return Customers.Any()
                    ? string.Join(", ", Customers.Select(x => x.Name))
                    : string.Empty;
            }
        }

        public string HaveCustomerImg
        {
            get
            {
                if (Customers.Any())
                {
                    return "/Content/Images/true.jpg";
                }
                else
                {
                    return "/Content/Images/false.jpg";
                }
            }
        }
    }

    public class TicketMetadataType
    {
        [DisplayName("Customers")]
        public string Customer { get; }

        [DisplayName("Ticket type")]
        public string TicketType { get; }

        [DisplayName("Have a customer?")]
        public string HaveCustomerImg { get; }
    }
}