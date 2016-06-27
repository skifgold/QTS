using System.Web.Mvc;

namespace QTSWebUI.Controllers
{
    public class CustomerController : MyBaseController
    {
        /// <summary>
        /// </summary>
        /// <param name="id">Concert id</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index(int id)
        {
            return View();
        }
    }
}