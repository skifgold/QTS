using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QTSWebUI.Models;
using QTSDataManager.Repositories;

namespace QTSWebUI.Controllers
{
    public class FourthWindowApiController : ApiController
    {
        [Route("api/FourthWindowApi/{id}/{fan}/{ticketType}")]
        public int Get(int id, string fan, string ticketType)
        {
            var res = new TicketsRepository()
                .Read().Count(x => x.ConcertId == id && !x.Customers.Any() && x.Fan == fan && x.TicketType == ticketType);
            return res;
        }
    }
}