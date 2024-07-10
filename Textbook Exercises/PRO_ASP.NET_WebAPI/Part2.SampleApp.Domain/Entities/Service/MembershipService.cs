using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;

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

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string role)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string[] roles)
        {
            throw new NotImplementedException();
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
            var userCtx = new ValidUserContext();
            var user = _userRepository.GetSingleUsername(username);
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
                    .Select(x => x.FullLegalName).ToArray());
            }
            return userCtx;
        }

        private object GetUserRoles(Guid key)
        {
            throw new NotImplementedException();
        }

        private bool IsUserValid(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}