using SportsStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class NavController : Controller
    {
        private readonly IMerchRepo _repository;

        public NavController(IMerchRepo repository)
        {
            _repository = repository;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = _repository.Merch
                .Select(c => c.Category)
                .Distinct()
                .OrderBy(c => c);

            return PartialView(categories);
        }
    }
}