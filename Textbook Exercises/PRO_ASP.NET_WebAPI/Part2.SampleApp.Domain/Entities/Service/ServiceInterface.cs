using System;
using System.Collections.Generic;

namespace PingYourPackage.Domain
{ // Housing all service interfaces here
    public interface ICryptoService
    {
        string GenerateSalt();
        string EncryptPassword(string password, string salt);
    }

    public interface IMembershipService
    {
        ValidUserContext ValidateUser(string username, string password);

        OperationResult<UserWithRoles> CreateUser(string username, string email, string password);
        OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string role);
        OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string[] roles);

        UserWithRoles UpdateUser(User user, string username, string email);

        bool ChangePassword(string username, string oldPassword, string newPassword);

        bool AddToRole(Guid userKey, string role);
        bool AddToRole(string username, string role);
        bool RemoveFromRole(string username, string role);

        IEnumerable<Role> GetRoles();
        Role GetRole(Guid userkey);
        Role GetRole(string username);

        PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize);
        UserWithRoles GetUser(Guid userKey);
        UserWithRoles GetUser(string username);
    }

    public interface IShipmentService
    {

    }
}