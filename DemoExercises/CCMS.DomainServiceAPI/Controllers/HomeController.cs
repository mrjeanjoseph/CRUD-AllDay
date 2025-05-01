using CCMS.DomainServiceAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CCMS.DomainServiceAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _usermanager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> usermanager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _usermanager = usermanager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            string[] roles = { "Admin", "Manager", "Cashier" };
            foreach (var role in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (roleExists == false)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var user = await _usermanager.FindByEmailAsync("jeanjoseph@crudallday.com");

            if (user != null)
            {
                await _usermanager.AddToRoleAsync(user, "Admin");
                await _usermanager.AddToRoleAsync(user, "Cashier");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
