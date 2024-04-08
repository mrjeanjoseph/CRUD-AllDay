using Chapter2.TheMVCPattern.Models;

namespace Chapter2.TheMVCPattern {
    public class User {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }

    public class DefaultUserRepository : IUserRepository {
        public void Add(User user) {
            throw new System.NotImplementedException();
        }

        public User FetchByLoginName(string loginName) {
            return new User() { LoginName = loginName };
        }

        public void SubmitChanges() {
            throw new System.NotImplementedException();
        }
    }
}