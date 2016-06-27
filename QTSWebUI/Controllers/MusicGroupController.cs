using System.Web.Mvc;
using QTSDataManager.DB;
using QTSDataManager.Repositories;
using System;

namespace QTSWebUI.Controllers
{
    [Authorize]
    public class MusicGroupController : MyBaseController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                var mgr = new MusicGroupRepository();
                var list = mgr.Read();

                return View(list);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MusicGroup mg)
        {
            try
            {
                var mgr = new MusicGroupRepository();
                mgr.Create(mg);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var res = new MusicGroupRepository().Read(id);
                return View(res);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(MusicGroup mg)
        {
            try
            {
                var mgr = new MusicGroupRepository();
                mgr.Update(mg);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        public ActionResult AddGenre(int musicGroupId)
        {
            try
            {
                ViewBag.MusicGroupId = musicGroupId;
                var res = new GenreRepository();
                return View(res.Read());
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }

        }

        public ActionResult AddGenreToMusicGroup(int genreId, int musicGroupId)
        {
            try
            {
                var res = new GenreRepository();
                res.AddGenreToMusicGroup(musicGroupId, genreId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }
    }
}