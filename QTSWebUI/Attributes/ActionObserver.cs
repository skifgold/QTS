using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NLog;

namespace QTSWebUI.Attributes
{
    public class ActionObserver : ActionFilterAttribute
    {
        private static readonly Logger CurrentClassLogger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var currentUserName = filterContext.HttpContext.User.Identity.Name;
                var ipAddress = filterContext.HttpContext.Request.UserHostAddress;
                var actionDescriptor = filterContext.ActionDescriptor;
                var actionParameters = filterContext.ActionParameters;

                var obj = new
                {
                    Method = filterContext.HttpContext.Request.HttpMethod,
                    IP = ipAddress,
                    UserName = currentUserName,
                    Browser = $"{filterContext.HttpContext.Request.Browser.Browser}{filterContext.HttpContext.Request.Browser.Version}",
                    actionDescriptor.ControllerDescriptor.ControllerName,
                    actionDescriptor.ActionName,
                    Parameters = actionParameters.ToDictionary(item => item.Key, item => item.Value?.ToString() ?? string.Empty)
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