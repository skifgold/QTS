using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QTSDataManager.Repositories;
using QTSWebUI.Models;
using System;

namespace QTSWebUI.Controllers
{
    [Route("api/MusicGroupApi/{name}")]
    public class MusicGroupApiController : ApiController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        public List<MusicGroupApiModel> Get(string name)
        {
            try
            {
                var res = new MusicGroupRepository().Read()
                                .Where(x => x.Name == name)
                                .Select(x => new MusicGroupApiModel
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Annotation = x.Annotation,
                                    PosterUrl = x.PosterURL,
                                    Genres = x.JoinedGenres
                                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                throw ex;
            }
        }

        private List<MusicGroupApiModel> View(string v)
        {
            throw new NotImplementedException();
        }
    }
}