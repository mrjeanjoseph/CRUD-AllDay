namespace Chapter2.TheMVCPattern.Models {
    public interface IUserRepository {
        void Add(User user);

        User FetchByLoginName(string loginName);

        void SubmitChanges();
    }
}
