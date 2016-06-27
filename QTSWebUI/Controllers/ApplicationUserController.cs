using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QTSWebUI.Models;

namespace QTSWebUI.Controllers
{
    [Authorize]
    public class ApplicationUserController : MyBaseController
    {
        private static readonly NLog.Logger CurrentClassLogger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ApplicationDbContext _userManager = new ApplicationDbContext();
        private IdentityRole AdminRole => _userManager.Roles.FirstOrDefault(y => y.Name == "Admin");

        public ActionResult Index()
        {
            try
            {
                var res = _userManager.Users.Where(x => x.Roles.Any(z => z.RoleId == AdminRole.Id));
                return View(res);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateNewUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = MyUserManager.Create(user, model.Password);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Creation user error!");
                    }
                    MyUserManager.AddToRole(user.Id, AdminRole.Name);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                CurrentClassLogger.Fatal(ex);
                return View("Error");
            }
        }


        public ActionResult Delete(string id)
        {
            try
            {
                var toDelete = MyUserManager.Users.First(x => x.Id == id);
                if (toDelete == null)
                {
                    throw new Exception("Removing user error!");
                }
                MyUserManager.Delete(toDelete);
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