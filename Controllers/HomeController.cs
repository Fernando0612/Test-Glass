using Glass.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glass.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

           var documents = Helpers.DocumentHelper.GetAllDocuments();

            return View(documents);
        }

        [HttpPost]
        public ActionResult Index(string filter, bool match = true)
        {
            
            var documents = filter != "" ? DocumentHelper.SearchDocuments(filter, match) : DocumentHelper.GetAllDocuments();

            return View(documents);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}