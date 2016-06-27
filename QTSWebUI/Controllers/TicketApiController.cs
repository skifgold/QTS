using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QTSDataManager.Repositories;
using QTSWebUI.Models;
using System;

namespace QTSWebUI.Controllers
{
    public class TicketApiController : ApiController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        public List<TicketApiModel> Get()
        {
            try
            {
                var res = new TicketsRepository().Read()
                .Select(x => new TicketApiModel
                {
                    Id = x.Id,
                    TicketType = x.TicketType,
                    Price = x.Price,
                    Fan = x.Fan,
                    ConcertId = x.ConcertId
                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                throw ex;
            }

        }

        private List<TicketApiModel> View(string v)
        {
            throw new NotImplementedException();
        }
    }
}