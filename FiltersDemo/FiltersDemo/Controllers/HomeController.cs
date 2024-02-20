using FiltersDemo.AuthData;
using FiltersDemo.AuthData.Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FiltersDemo.Controllers
{
    public class ArgsSpecial: EventArgs
    {

    }
    [LogAttribute]
    public class HomeController : Controller
    {
        public Action Run {  get; set; }
        public event EventHandler runEvent = delegate { };
        //[CustomHandleError]
        [Route("gohere/{*param}")]
        public ActionResult Index(string param)
        {
            Run += () => { Response.Write("First Executed!"); };
            Run += () => { Response.Write("Second Executed!"); };
            Run = () => { Response.Write("Third Executed!"); };

            Run();

            runEvent += delegate(object args, EventArgs e) { Response.Write("First Executed!"); };
            runEvent += delegate (object args, EventArgs e) { Response.Write("Second Executed!"); };
            //runEvent = () => { Response.Write("Third Executed!"); };
            //throw new DivideByZeroException();
            return View();
        }



        [Authorize]
        public ActionResult LoggedUser()
        {

            return View();
        }

        

        [Route("goto/{deviceId?}/{appId?}", Name="ClickHereLogin")]
        public ActionResult ClickHereToLogIn(int? deviceId, int? appId)
        {
            FormsAuthentication.SetAuthCookie("Sony", false);
            return View();
        }
        
        [HttpPost]
        [Route("feedback", Name = "submitFeedback")]
        [ValidateAntiForgeryToken]
        //[ValidateInput(enableValidation:true)]
        public ActionResult Feedback(string feedback)
        {
            bool error;
            if (!String.IsNullOrEmpty(feedback))
                ViewBag.RedirectText = "Feedback has been submitted successfully!";
            else
                ViewBag.RedirectText = "Oh No! Some issue happened!";
            return PartialView("_Feedback");
        }


        [AuthAttribute]
        public ActionResult About()
        {
            Session["UserName"] = "Sony";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [CustomAuthorize("admin", "Super Admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [CustomAuthorize("super-admin", "Super Admin")]
        public ActionResult SuperAdmin()
        {
            ViewBag.Message = "Your contact page.";
            Session["UserName"] = "Sony";

            return View();
        }

        public ActionResult NoAuthPage()
        {
            ViewBag.Message = "Your contact page.";
            Session["UserName"] = "Sony";

            return View();
        }


    }
}