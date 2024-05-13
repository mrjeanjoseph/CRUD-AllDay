using Chapter2.TheMVCPattern;
using Chapter2.TheMVCPattern.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChapterThree.UnitTests {
    public partial class AdminControllerTests {
        class FakeRepository : IUserRepository {
            public List<User> Users = new List<User>();
            public bool DidSubmitChanges = false;

            public void Add(User user) {
                Users.Add(user);
            }

            public User FetchByLoginName(string loginName) {
                return Users.First(m => m.LoginName == loginName);
            }

            public void SubmitChanges() {
                DidSubmitChanges |= true;
            }
        }
    }
}
