using CCMS.DomainService.Models;
using System.Collections.Generic;

namespace CCMS.DomainService.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string id);
    }
}