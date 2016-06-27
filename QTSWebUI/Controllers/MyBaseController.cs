using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace QTSWebUI.Controllers
{
    public class MyBaseController : Controller
    {
        internal ApplicationUserManager MyUserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
    }
}