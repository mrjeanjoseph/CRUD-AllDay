using DPE.DomainService.DataAccess;
using DPE.DomainService.Models;
using DPE.DomainServiceAPI.Data;
using DPE.DomainServiceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DPE.DomainServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserData _userData;
        public readonly UserManager<IdentityUser> _usermanager;
        private readonly ILogger<UserController> logger;

        public UserController(ApplicationDbContext context, IUserData userData,
            UserManager<IdentityUser> usermanager, ILogger<UserController> logger)
        {
            _context = context;
            _userData = userData;
            _usermanager = usermanager;
            this.logger = logger;
        }

        [HttpGet]
        public UserModel GetById()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                throw new InvalidOperationException("User ID cannot be null.");

            return _userData.GetUserById(userId).First();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> result = [];

            var users = _context.Users.ToList();
            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

            foreach (var user in users)
            {
                ApplicationUserModel u = new ApplicationUserModel
                {
                    Id = user.Id,
                    Email = user.Email
                };

                u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(key => key.RoleId, value => value.Name);
                result.Add(u);
            }

            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            var roles = _context.Roles
                //.Where(role => role.Name != null) // Ensure no null values in role names
                .ToDictionary(key => key.Id, value => value.Name!); // Use null-forgiving operator

            return roles;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AddRole")]
        public async Task AddRole(UserRolePairModel urp)
        {
            string? loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = _userData.GetUserById(loggedInUserId).First();

            var user = await _usermanager.FindByIdAsync(urp.UserId);

            logger.LogInformation("Admin {Admin} added user {User} to role {Role}",
                loggedInUserId, user.Id, urp.RoleName);

            await _usermanager.AddToRoleAsync(user, urp.RoleName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveRole")]
        public async Task RemoveRole(UserRolePairModel urp)
        {
            string? loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = _userData.GetUserById(loggedInUserId).First();

            var user = await _usermanager.FindByIdAsync(urp.UserId);

            logger.LogInformation("Admin {Admin} removed user {User} from role {Role}",
                loggedInUserId, user.Id, urp.RoleName);


            await _usermanager.RemoveFromRoleAsync(user, urp.RoleName);
        }
    }
}
