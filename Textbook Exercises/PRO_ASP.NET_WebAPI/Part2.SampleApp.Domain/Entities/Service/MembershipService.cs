using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;
using System.Runtime.CompilerServices;

namespace PingYourPackage.Domain
{
    public class MembershipService : IMembershipService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Role> _roleRepository;
        private readonly IEntityRepository<UserInRole> _userinRoleRepo;
        private readonly ICryptoService _cryptoService;

        public MembershipService(
            IEntityRepository<User> userRepository,
            IEntityRepository<Role> roleRepository,
            IEntityRepository<UserInRole> userinroleRepo,
            ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _cryptoService = cryptoService;
            _userinRoleRepo = userinroleRepo;
        }
        public bool AddToRole(Guid userKey, string role)
        {
            throw new NotImplementedException();
        }

        public bool AddToRole(string username, string role)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password) =>
            CreateUser(username, email, password);

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string role) => 
            CreateUser(username, email, password, roles: new[] {role });

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string[] roles)
        {
            var existingUser = _userRepository.GetAll().Any(x => x.FullLegalName == username);

            if(existingUser) return new OperationResult<UserWithRoles>(false);

            var passwordSalted = _cryptoService.GenerateSalt();

            var user = new User()
            {
                FullLegalName = username,
                Salt = passwordSalted,
                EmailAddress = email,
                IsLocked = false,
                HashedPassword = _cryptoService.EncryptPassword(password, passwordSalted),
                CreatedOn = DateTime.Now
            };

            _userRepository.Add(user);
            _userRepository.Save();

            if(roles != null || roles.Length > 0)             
                foreach (var roleName in roles) AddUserToRole(user, roleName);

            return new OperationResult<UserWithRoles>(true)
            {
                Entity = GetUserWithRoles(user),
            };
        }

        private UserWithRoles GetUserWithRoles(User user)
        {
            throw new NotImplementedException();
        }

        private void AddUserToRole(User user, string roleName)
        {
            var role = _roleRepository.GetSingleByRoleName(roleName);
            if (role == null)
            {
                var tempRole = new Role()
                {
                    Name = roleName,
                };

                _roleRepository.Add(tempRole);
                _roleRepository.Save();
                role = tempRole;
            }

            var userInRole = new UserInRole()
            {
                RoleKey = role.Key,
                UserKey = user.Key,
            };
            _userinRoleRepo.Add(userInRole);
            _userinRoleRepo.Save();
        }

        public Role GetRole(Guid userkey)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public UserWithRoles GetUser(Guid userKey)
        {
            throw new NotImplementedException();
        }

        public UserWithRoles GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromRole(string username, string role)
        {
            throw new NotImplementedException();
        }

        public UserWithRoles UpdateUser(User user, string username, string email)
        {
            throw new NotImplementedException();
        }

        public ValidUserContext ValidateUser(string username, string password)
        {
            var userCtx = new ValidUserContext(); //This valid user context is missing some components
            var user = _userRepository.GetSingleByUsername(username);
            if (user != null && IsUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Key);
                userCtx.User = new UserWithRoles()
                {                   
                    User = user,
                    Roles = userRoles
                };

                var identity = new GenericIdentity(user.FullLegalName);
                userCtx.Principal = new GenericPrincipal(identity, userRoles
                    .Select(x => x.Name).ToArray());
            }
            return userCtx;
        }

        private IEnumerable<Role> GetUserRoles(Guid userKey)
        {
            var userInRoles = _userinRoleRepo.FindBy(x => x.UserKey == userKey).ToList();

            if(userInRoles != null && userInRoles.Count > 0)
            {
                var userRoleKeys = userInRoles.Select(x => x.RoleKey).ToArray();
                var userRoles = _roleRepository.FindBy(x => userRoleKeys.Contains(x.Key));

                return userRoles;
            }
            return Enumerable.Empty<Role>();
        }

        private bool IsUserValid(User user, string password)
        {
            if (isPassworldValid(user, password)) return !user.IsLocked;
            return false;
        }

        private bool isPassworldValid(User user, string password) =>
            string.Equals(_cryptoService.EncryptPassword(password, user.Salt), user.HashedPassword);
    }
}