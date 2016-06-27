using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QTSDataManager.Repositories;
using QTSWebUI.Models;
using System;
using QTSDataManager.DB;

namespace QTSWebUI.Controllers
{
    public class CustomerApiController : ApiController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        public List<CustomerApiModel> Get()
        {
            try
            {
                var res = new CustomerRepository().Read()
                .Select(x => new CustomerApiModel
                {
                    Id = x.Id,
                    TicketId = x.TicketId,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    Phone = x.Phone,
                    Status = x.Status
                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                throw ex;
            }

        }

        //CustomerApi api/CustomerApi
        [HttpPost]
        public string Post([FromBody]Customer customer)
        {
            var ids = new List<int>();
            for (int i = 0; i < customer.Count; i++)
            {
                var cr = new CustomerRepository();
                var tr = new TicketsRepository();
                var ticket = tr.Read().First(x => x.ConcertId == customer.ConcertId && x.Fan == customer.Fan && x.TicketType == customer.TicketType && !x.Customers.Any());
                customer.TicketId = ticket.Id;
                cr.Create(customer);
                ids.Add(ticket.Id);
            }
            return string.Join(", ", ids);
        }

        private List<CustomerApiModel> View(string v)
        {
            throw new NotImplementedException();
        }
    }
}