using QTSDataManager.Repositories;
using QTSWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace QTSWebUI.Controllers
{
    public class SecondWindowApiController : ApiController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        public List<SecondWindowApiModel> Get(int id)
        {
            try
            {
                var res = new ConcertRepository().Read()
                     .Where(x => x.Id == id)
                    .GroupBy(x => new { x.Name, x.Date, x.Address, x.TypeOfPlace, x.PosterURL, x.ConcertGroups })
                    .Select(x => new SecondWindowApiModel
                    {
                        NameOfConcert = x.Key.Name,
                        Date = x.Key.Date,
                        AdressOfConcert = x.Key.Address,
                        TypeOfPlace = x.Key.TypeOfPlace,
                        PosterURL = x.Key.PosterURL,
                        GroupList = x.Key.ConcertGroups.Select(y => y.MusicGroup.Name).Distinct().Distinct()
                    });

                return res.ToList();
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                throw ex;
            }
        }

        private List<SecondWindowApiModel> View(string v)
        {
            throw new NotImplementedException();
        }
    }
}
