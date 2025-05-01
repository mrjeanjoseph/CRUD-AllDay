using DPE.DomainService.Models;
using System.Collections.Generic;

namespace DPE.DomainService.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string id);
    }
}