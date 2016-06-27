using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QTSWebUI
{
    public class DbInit : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var rootRole = new IdentityRole {Name = "Root"};
            var adminRole = new IdentityRole {Name = "Admin"};

            var root = new ApplicationUser {Email = "root@gmai.com", UserName = "root@gmail.com"};
            var admin = new ApplicationUser {Email = "admin@gmail.com", UserName = "admin@gmail.com"};

            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
            {
                roleManager.Create(rootRole);
                roleManager.Create(adminRole);
            }

            using (var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context)))
            {
                if (userManager.Create(root, "rootroot").Succeeded && userManager.Create(admin, "adminadmin").Succeeded)
                {
                    userManager.AddToRole(root.Id, rootRole.Name);
                    userManager.AddToRole(admin.Id, adminRole.Name);
                }
            }

            base.Seed(context);
        }
    }
}