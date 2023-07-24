using NUnit.Framework;
using Moq;
using WeightliftingTrackerGraphQLAPI.Resolvers;
using WeightliftingTrackerGraphQLAPI.Repositories;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Tests
{
    internal class UserResolversTests
    {
        private Mock<IUserRepository>? _mockUserRepository;
        private UserResolvers? _userResolvers;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userResolvers = new UserResolvers(_mockUserRepository.Object);
        }

        [Test]
        public void Login_Returns_JWT_Token()
        {
            var user = new User { Id = 1 };
            var result = _userResolvers.Login(user);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        public async Task CreateUser_Returns_New_User()
        {
            var newUser = new User { Id = 1, Username = "Test User" };
            _mockUserRepository.Setup(r => r.CreateUser(It.IsAny<User>())).ReturnsAsync(newUser);

            var result = await _userResolvers.CreateUser(newUser);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<User>(result);
            Assert.AreEqual(newUser.Id, result.Id);
            Assert.AreEqual(newUser.Username, result.Username);
        }

    }
}
