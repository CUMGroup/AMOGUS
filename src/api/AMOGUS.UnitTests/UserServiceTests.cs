using AMOGUS.Core.Centralization.User;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Infrastructure.Identity;
using AMOGUS.Infrastructure.Services.User;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.UnitTests {
    public class UserServiceTests {
        private Mock<IUserManager> CreateUserManagerMock() => new();
        private Mock<IUserMedalRepository> CreateUserMedalRepositoryMock() => new();
        private Mock<IGameSessionRepository> CreateGameSessionRepositoryMock() => new();
        private Mock<IUserStatsRepository> CreateUserStatsRepositoryMock() => new();
        
        #region GetUserAsync
        [Fact]
        public async Task GetUserAsync_WhenGivenAUserId_AndNoUserWithThatIdExists_Exception() {
            var userManager = CreateUserManagerMock();
            userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser) null);

            var userService = new UserService(userManager.Object, CreateUserMedalRepositoryMock().Object, CreateGameSessionRepositoryMock().Object, CreateUserStatsRepositoryMock().Object) { };

            var result = await userService.GetUserAsync("testID");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is RecordNotFoundException);
        }

        [Fact]
        public async Task GetUserAsync_WhenGivenAUserId_AndUserWithThatIdExists_ReturnsUser() {
            var userManager = CreateUserManagerMock();
            userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var userService = new UserService(userManager.Object, CreateUserMedalRepositoryMock().Object, CreateGameSessionRepositoryMock().Object, CreateUserStatsRepositoryMock().Object) { };

            var result = await userService.GetUserAsync("testID");

            Assert.True(result.IsSuccess);
        }
        #endregion

        #region IsInRoleAsync
        [Fact]
        public async Task IsInRoleAsync_WhenGivenAUserId_AndRole_AndUserNotInRole_ReturnsFalse() {
            var userManager = CreateUserManagerMock();
            userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManager
                .Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var userService = new UserService(userManager.Object, CreateUserMedalRepositoryMock().Object, CreateGameSessionRepositoryMock().Object, CreateUserStatsRepositoryMock().Object) { };

            var result = await userService.IsInRoleAsync("testID", "User");

            Assert.False(result);
        }

        [Fact]
        public async Task IsInRoleAsync_WhenGivenAUserId_AndRole_AndUserInRole_ReturnsTrue() {
            var userManager = CreateUserManagerMock();
            userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManager
                .Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var userService = new UserService(userManager.Object, CreateUserMedalRepositoryMock().Object, CreateGameSessionRepositoryMock().Object, CreateUserStatsRepositoryMock().Object) { };

            var result = await userService.IsInRoleAsync("testID", "User");

            Assert.True(result);
        }
        #endregion

        #region DeleteUserAsync
        [Fact]
        public async Task DeleteUserAsync_WhenGivenAUserId_AndUserNotFound_ReturnsFaulted() {
            var medalRepositoryMock = CreateUserMedalRepositoryMock();
            medalRepositoryMock
                .Setup(x => x.DeleteUserMedalsByUserIdAsync(It.IsAny<string>()));

            var gameSessionRepositoryMock = CreateGameSessionRepositoryMock();
            gameSessionRepositoryMock
                .Setup(x => x.DeleteGameSessionsByUserIdAsync(It.IsAny<string>()));

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.DeleteUserStatsAsync(It.IsAny<string>()));

            var userManager = CreateUserManagerMock();
            userManager.
                Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser) null);

            var userService = new UserService(userManager.Object, medalRepositoryMock.Object, gameSessionRepositoryMock.Object, userStatsRepositoryMock.Object) { };

            var result = await userService.DeleteUserAsync("testID");

            Assert.True(result.IsFaulted);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenGivenAUserId_AndUserManagerCantDelete_Exception() {
            var medalRepositoryMock = CreateUserMedalRepositoryMock();
            medalRepositoryMock
                .Setup(x => x.DeleteUserMedalsByUserIdAsync(It.IsAny<string>()));

            var gameSessionRepositoryMock = CreateGameSessionRepositoryMock();
            gameSessionRepositoryMock
                .Setup(x => x.DeleteGameSessionsByUserIdAsync(It.IsAny<string>()));

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.DeleteUserStatsAsync(It.IsAny<string>()));

            var userManager = CreateUserManagerMock();
            userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManager
                .Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed());

            var userService = new UserService(userManager.Object, medalRepositoryMock.Object, gameSessionRepositoryMock.Object, userStatsRepositoryMock.Object) { };

            var result = await userService.DeleteUserAsync("testID");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is UserOperationException);

        }

        [Fact]
        public async Task DeleteUserAsync_WhenGivenAUserId_AndEverythingIsFine_ReturnTrue() {
            var medalRepositoryMock = CreateUserMedalRepositoryMock();
            medalRepositoryMock
                .Setup(x => x.DeleteUserMedalsByUserIdAsync(It.IsAny<string>()));

            var gameSessionRepositoryMock = CreateGameSessionRepositoryMock();
            gameSessionRepositoryMock
                .Setup(x => x.DeleteGameSessionsByUserIdAsync(It.IsAny<string>()));

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.DeleteUserStatsAsync(It.IsAny<string>()));

            var userManager = CreateUserManagerMock();
            userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManager
                .Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var userService = new UserService(userManager.Object, medalRepositoryMock.Object, gameSessionRepositoryMock.Object, userStatsRepositoryMock.Object) { };

            var result = await userService.DeleteUserAsync("testID");

            Assert.True(result.IsSuccess);
        }
        #endregion
    }
}
