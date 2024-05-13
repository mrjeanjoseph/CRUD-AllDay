using Chapter2.TheMVCPattern;
using Chapter2.TheMVCPattern.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChapterThree.UnitTests {
    [TestClass]
    public partial class AdminControllerTests {
        [TestMethod]
        public void CanChangeLoginName() {
            //Arrange (set up a scenario)
            User user = new User() { LoginName = "Elvila" };
            FakeRepository repo = new FakeRepository();
            repo.Add(user);

            AdminController target = new AdminController(repo);
            string oldLoginParam = user.LoginName;
            string newLoginParam = "LalaBliss";

            //Act (Attempt the operation)
            target.ChangeLoginName(oldLoginParam, newLoginParam);

            //Assert (Verify the result)
            Assert.AreEqual(newLoginParam, user.LoginName);
            Assert.IsTrue(repo.DidSubmitChanges);
        }
    }
}
