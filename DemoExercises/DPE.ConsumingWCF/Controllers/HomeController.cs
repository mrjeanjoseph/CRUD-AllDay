using DPE.ConsumingWCF.ServiceReference2;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DPE.ConsumingWCF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult CallService(string inputValue)
        {
            int value;
            if (int.TryParse(inputValue, out value))
            {
                string result;
                using (var client = new ServiceClient())
                {
                    result = client.GetData(value);

                    client.Close();
                }
                return Json(result);
            }
            else
            {
                return Json("Please enter a valid integer.");
            }
        }


        [HttpPost]
        public ActionResult CallGetAllData(string inputValue)
        {
            int value;
            if (int.TryParse(inputValue, out value))
            {
                List<int> result;

                using (var client = new ServiceClient())
                {
                    result = client.GetAllData(value).ToList();

                    client.Close();
                }
                return Json(result);
            }
            else
            {
                return Json("Please enter a valid integer.");
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}