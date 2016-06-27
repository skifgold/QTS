using QTSDataManager.Repositories;
using QTSWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QTSWebUI.Controllers
{
    public class ThirdWindowApiController : ApiController
    {
        public List<ThirdWindowApiModel> Get(int id)
        {
            var res = new TicketsRepository().Read()
                   .Where(x => x.ConcertId == id)
                 .GroupBy(x => new { x.Fan, x.TicketType, x.Price})
                .Select(x => new ThirdWindowApiModel
                {
                    Fan = x.Key.Fan,
                    Price = x.Key.Price,
                    TypeOfTicket = x.Key.TicketType,
                    Count = x.Count()
                });
            return res.ToList();
        }
    }
}
