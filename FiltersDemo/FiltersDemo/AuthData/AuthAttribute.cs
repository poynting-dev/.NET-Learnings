using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace FiltersDemo.AuthData
{
    public class AuthAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        //public bool _auth;
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserName"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //write some code for the other tasks
            var user = filterContext.HttpContext.User;
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                //filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            }
        }

    }

    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] allowedroles;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        // Summary:
        //     When overridden, provides an entry point for custom authorization checks.
        //
        // Parameters:
        //   httpContext:
        //     The HTTP context, which encapsulates all HTTP-specific information about an individual
        //     HTTP request.
        //
        // Returns:
        //     true if the user is authorized; otherwise, false.
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToString(httpContext.Session["UserName"]);
            if (!string.IsNullOrEmpty(userId))
            {
                if(allowedroles.Contains("super-admin") || allowedroles.Contains("admin"))
                    return true;
                //using (var context = new SqlDbContext())
                //{
                //    var userRole = (from u in context.Users
                //                    join r in context.Roles on u.RoleId equals r.Id
                //                    where u.UserId == userId
                //                    select new
                //                    {
                //                        r.Name
                //                    }).FirstOrDefault();
                foreach (var role in allowedroles)
                {
                    if (role == "") return true;
                }
                //}
                return true;
            }

            return authorize;
        }

        //Processes HTTP requests that fail authorization
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new RedirectToRouteResult(
            //   new RouteValueDictionary
            //   {
            //        { "controller", "Home" },
            //        { "action", "Index" }
            //   });

            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Shared/Unauthorized.cshtml"
            };
        }
    }
}