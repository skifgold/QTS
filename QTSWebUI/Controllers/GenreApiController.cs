using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QTSDataManager.Repositories;
using QTSWebUI.Models;
using System;

namespace QTSWebUI.Controllers
{
    public class GenreApiController : ApiController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        public List<GenreApiModel> Get()
        {
            try
            {
                var res = new GenreRepository().Read()
                                .Select(x => new GenreApiModel
                                {
                                    Id = x.Id,
                                    Name = x.Name
                                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                throw ex;
            }
        }

        private List<GenreApiModel> View(string v)
        {
            throw new NotImplementedException();
        }
    }
}