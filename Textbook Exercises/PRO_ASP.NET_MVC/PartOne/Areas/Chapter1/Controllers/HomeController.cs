using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Introduction.FirstApp.MVC.Areas.Chapter1.Controllers {
    public class HomeController : Controller {
        // GET: Chapter1/Home
        public string Index() {
            return "<h1>Hello ASP.NET MVC 5</h1> - \nThis is Chapter 1, the introduction";
        }
    }
}