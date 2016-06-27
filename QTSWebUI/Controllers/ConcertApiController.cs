using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QTSDataManager.Repositories;
using QTSWebUI.Models;
using System;

namespace QTSWebUI.Controllers
{
    public class ConcertApiController : ApiController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        public List<ConcertApiModel> Get()
        {
            try
            {
                var res = new ConcertRepository().Read()
                .Where(x => x.Date > DateTime.Now)
                .OrderBy(x => x.Date)
                .Select(x => new ConcertApiModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    TypeOfPlace = x.TypeOfPlace,
                    Date = x.Date,
                    PosterUrl = x.PosterURL
                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                throw ex;
            }
        }

        private List<ConcertApiModel> View(string v)
        {
            throw new NotImplementedException();
        }
    }
}