using Chapter2.TheMVCPattern.Models;
using System.Web.Mvc;

namespace Chapter2.TheMVCPattern.Controllers {
    public class AdminController : Controller {
        private readonly IUserRepository _repository;

        public AdminController(IUserRepository repository) {
            _repository = repository;
        }
        public ActionResult Index() {
            return View();
        }
        public ActionResult ChangeLoginName(string oldName, string newName) {
            User user = _repository.FetchByLoginName(oldName);
            user.LoginName = newName;

            _repository.SubmitChanges();
            // render some view to show the result
            return View();
        }
    }
}