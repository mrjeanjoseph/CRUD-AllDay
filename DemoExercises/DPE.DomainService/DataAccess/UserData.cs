﻿using DPE.DomainService.Models;
using DPE.DomainService.UserData;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DPE.DomainService.DataAccess
{
    public class UserData : IUserData
    {
        private readonly IConfiguration _config;
        private readonly ISqlDataAccess _sqlData;

        public UserData(ISqlDataAccess sqlData)
        {
            _sqlData = sqlData;
        }

        public List<UserModel> GetUserById(string id)
        {
            return _sqlData.LoadData<UserModel, dynamic>("dbo.spUserLookup", new { id }, "CCMSConn");
        }
    }
}
