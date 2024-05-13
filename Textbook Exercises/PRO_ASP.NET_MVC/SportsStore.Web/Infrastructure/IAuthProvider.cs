namespace SportsStore.Web.Infrastructure
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);

    }
}