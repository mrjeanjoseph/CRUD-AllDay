using Chapter24.ModelBinding.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Chapter24.ModelBinding.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter24/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NamesArray(string[] names)
        {
            names = names ?? new string[0];
            return View(names);
        }
        public ActionResult Names(IList<string> names)
        {
            names = names ?? new List<string>();
            return View(names);
        }

        public ActionResult Address()
        {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            UpdateModel(addresses);
            return View(addresses);
        }

        public ActionResult AddressOldThree()
        {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            UpdateModel(addresses, new FormValueProvider(ControllerContext));
            return View(addresses);
        }

        public ActionResult AddressOldThree(FormCollection formData)
        {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            if (TryUpdateModel(addresses, formData))
            {
                // Process as normal
            } else
            {
                // Provide more feedback
            }
            return View(addresses);
        }

        public ActionResult AddressOldTwo(FormCollection formData)
        {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            try
            {
                UpdateModel(addresses, formData);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            return View(addresses);
        }

        public ActionResult AddressOldOne(FormCollection formData)
        {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            UpdateModel(addresses, new FormValueProvider(ControllerContext));
            return View(addresses);
        }
    }
}