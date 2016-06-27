using System;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Script.Serialization;
using NLog;

namespace QTSWebUI.Attributes
{
    public class ActionApiObserver : ActionFilterAttribute
    {
        private static readonly Logger CurrentClassLogger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            try
            {
                var currentUserName = HttpContext.Current.User.Identity.Name;
                var ipAddress = HttpContext.Current.Request.UserHostAddress;
                var actionDescriptor = filterContext.ActionDescriptor;
                var actionParameters = filterContext.ActionArguments;

                var obj = new
                {
                    Method = HttpContext.Current.Request.HttpMethod,
                    IP = ipAddress,
                    UserName = currentUserName,
                    Browser = $"{HttpContext.Current.Request.Browser.Browser}{HttpContext.Current.Request.Browser.Version}",
                    actionDescriptor.ControllerDescriptor.ControllerName,
                    actionDescriptor.ActionName,
                    Parameters = actionParameters
                };

                var sb = new StringBuilder();
                var json = new JavaScriptSerializer();
                sb.Append(json.Serialize(obj));

                CurrentClassLogger.Info(sb.ToString);
            }
            catch (Exception exception)
            {
                CurrentClassLogger.Error(exception);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}