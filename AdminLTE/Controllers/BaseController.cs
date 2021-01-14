﻿using AdminLTE.DataAccess;
using AdminLTE.Manager;
using Microsoft.AspNet.SignalR.Client;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AdminLTE.Controllers
{
    public class BaseController : Controller
    {
        private UnitOfWork _unitOfWork;
        private static object _lock;

        protected UnitOfWork UnitOfWork 
        {
            get 
            {
                if (_unitOfWork == null)
                {
                    if (_unitOfWork == null)
                        return new UnitOfWork(new DbModelContext(), User);
                    //lock (_lock)
                    //{
                    //    if (_unitOfWork == null)
                    //        return new UnitOfWork(new DbModelContext());

                    //}
                }
                return _unitOfWork;
            } 
        }

        public BaseController()
        {
        }

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