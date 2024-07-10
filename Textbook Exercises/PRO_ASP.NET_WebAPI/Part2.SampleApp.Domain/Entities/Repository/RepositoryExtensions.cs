using System.Linq;

namespace PingYourPackage.Domain
{
    public static class UserRepositoryExtensions
    {
        public static User GetSingleByUsername(this IEntityRepository<User> userRepository, string username) =>
            userRepository.GetAll().FirstOrDefault(x => x.FullLegalName == username);
    }

    public static class RoleRepositoryExtensions
    {
        public static Role GetSingleByRoleName(this IEntityRepository<Role> roleRepository, string rolename) =>
            roleRepository.GetAll().FirstOrDefault(x => x.Name == rolename);
    }
}