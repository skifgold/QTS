using System;
using System.IO;
using System.Web.Mvc;

namespace QTSWebUI.Controllers
{
    public class HomeController : MyBaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}