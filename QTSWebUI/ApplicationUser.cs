using Microsoft.AspNet.Identity.EntityFramework;

namespace QTSWebUI
{
    public class ApplicationUser : IdentityUser
    {
        public int Year { get; set; }
    }
}