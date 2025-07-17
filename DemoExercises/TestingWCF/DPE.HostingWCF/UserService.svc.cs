using System;
using System.Collections.Generic;
using System.Linq;

namespace DPE.HostingWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public List<UserInfo> GetUsers(string search_term)
        {
            var users = new List<UserInfo>();

            // Simulate fetching users from a data source
            foreach(var user in userList.OrderBy(u => u.UserName))
            {
                users.Add(new UserInfo
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                });
            }
            return users;
        }

        // Let's create a list of user data for demonstration purposes
        private readonly List<UserInfo> userList = new List<UserInfo>
        {
            new UserInfo { UserName = "Jamie Foxx", Email = "jamie.foxx@dvc.dev", PhoneNumber = "456-753-9515", Address = "456 somewhere cool dr." },
            new UserInfo { UserName = "Jamie Foxx", Email = "jamie.foxx@dvc.dev", PhoneNumber = "456-753-9515", Address = "456 somewhere cool dr." },
            new UserInfo { UserName = "Jamie Foxx", Email = "jamie.foxx@dvc.dev", PhoneNumber = "456-753-9515", Address = "456 somewhere cool dr." },
            new UserInfo { UserName = "Jamie Foxx", Email = "jamie.foxx@dvc.dev", PhoneNumber = "456-753-9515", Address = "456 somewhere cool dr." },
            new UserInfo { UserName = "Jamie Foxx", Email = "jamie.foxx@dvc.dev", PhoneNumber = "456-753-9515", Address = "456 somewhere cool dr." },
        };

    }
}
