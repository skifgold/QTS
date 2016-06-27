using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NLog;
using QTSWebUI.Models;

namespace QTSWebUI.Controllers
{
    public class AccountController : MyBaseController
    {
        private static readonly Logger CurrentClassLogger = LogManager.GetCurrentClassLogger();

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                CurrentClassLogger.Info("asdasd");
                if (Request.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Concert");
                }
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindAsync(model.Email, model.Password);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Login or password is incorect");
                    }
                    else
                    {
                        var claim =
                            await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction("Index", "Concert");
                        }

                        return Redirect(returnUrl);
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }

        public ActionResult LogOff()
        {
            try
            {
                AuthenticationManager.SignOut();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }
    }
}