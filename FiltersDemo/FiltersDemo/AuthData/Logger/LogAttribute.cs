using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;

namespace FiltersDemo.AuthData.Logger
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting ", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0}- controller:{1} action:{2}", methodName,
                                                                        controllerName,
                                                                        actionName);

            FileController fc = new FileController();
            fc.WriteToFile(message);
            Debug.WriteLine(message);
        }

    }

    public class CustomHandleError : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
            };
        }
    }

    public class FileController: Controller
    {
        public EmptyResult WriteToFile(string content)
        {
            //// Get the current working directory
            //string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string filePath = Path.Combine(currentDirectory, "log.txt");

            //// Write the data to the file
            //System.IO.File.AppendAllText(filePath, content + "\n");
            return new EmptyResult();
        }

    }
}