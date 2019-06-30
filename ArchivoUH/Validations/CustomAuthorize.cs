using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ArchivoUH.Controllers;

namespace ArchivoUH.Validations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private bool _admin;

        public CustomAuthorizeAttribute(bool admin)
        {
            _admin = admin;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var core = httpContext.Request.IsAuthenticated && ((_admin) ? httpContext.User.Identity.Name == "admin" : true);
            return core;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpNotFoundResult();
        }
    }
}