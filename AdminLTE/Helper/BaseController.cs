using AdminLTE.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using AdminLTE.Manager;
using AdminLTE.DataAccess;

namespace AdminLTE.Controllers
{
    public class BaseController : Controller
    {
        protected UnitOfWork UnitOfWork = new UnitOfWork(new DbModelContext());

        private readonly static Dictionary<string, string> _translate = new Dictionary<string, string>();
        private static string _languageCode = "";

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.RouteData.Values["language"] != null)
            {
                string languageCode = filterContext.RequestContext.RouteData.Values["language"].ToString().ToUpper();
                if (_languageCode != languageCode)
                {
                    _languageCode = languageCode;
                    _translate.Clear();
                    var translateMessages = UnitOfWork.TranslationManager.GetTranslationWords(_languageCode);
                    foreach (var message in translateMessages)
                        _translate.Add(message.Code, message.Description);
                }
            }
            base.OnActionExecuting(filterContext);
        }

        internal string Translate(string key)
        {
            string result = key;
            _translate.TryGetValue(key, out result);
            return result;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            string userName = User != null && User.Identity != null ? User.Identity.Name : "";
            if (string.IsNullOrEmpty(userName))
                return;

            var exception = filterContext.Exception;
            string message = "";
            if (exception.GetType() == typeof(UserException))
                message = exception.Message;
            else
                message = Translate("error-exception");

            var hubConnection = new HubConnection(filterContext.HttpContext.Request.Url.OriginalString.Replace(filterContext.HttpContext.Request.Url.PathAndQuery, "") + "/signalr", useDefaultUrl: false);
            IHubProxy hubProxy = hubConnection.CreateHubProxy("SignalRHub");
            hubConnection.Start().Wait();
            hubProxy.Invoke("SendNotification", userName, message, "error").Wait();

            base.OnException(filterContext);
        }
    }
}