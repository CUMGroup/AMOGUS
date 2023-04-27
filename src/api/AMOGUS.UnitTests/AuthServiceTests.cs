using AMOGUS.Core.Centralization.User;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Common.Interfaces.Security;
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;
using AMOGUS.Infrastructure.Services.User;
using Antlr4.Runtime;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;
using ITokenFactory = AMOGUS.Core.Common.Interfaces.Security.ITokenFactory;

namespace AMOGUS.UnitTests {
    public class AuthServiceTests {

        private Mock<ITokenFactory> CreateTokenFactoryMock() => new();
        private Mock<IRoleManager> CreateRoleManagerMock() => new();
        private Mock<IUserManager> CreateUserManagerMock() => new();
        private Mock<IUserStatsRepository> CreateUserStatsRepositoryMock() => new();

        #region CreateRolesAsync
        [Fact]
        public async Task CreateRolesAsync_WhenGivenRoles_CreatesThem() {
            var createdIdentityRoles = new List<IdentityRole>();

            var roleManager = CreateRoleManagerMock();
            roleManager
                .Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            roleManager
                .Setup(x => x.CreateAsync(It.IsAny<IdentityRole>()))
                .Callback((IdentityRole ir) => createdIdentityRoles.Add(ir));

            var authService = new AuthService(roleManager.Object, CreateUserManagerMock().Object, CreateTokenFactoryMock().Object, CreateUserStatsRepositoryMock().Object);

            await authService.CreateRolesAsync<UserRoles>();

            Assert.Contains(createdIdentityRoles,  x => "Admin".Equals(x.Name));
            Assert.Contains(createdIdentityRoles, x => "Moderator".Equals(x.Name));
            Assert.Contains(createdIdentityRoles, x => "User".Equals(x.Name));
        }

        [Fact]
        public async Task CreateRolesAsync_WhenGivenRoles_AndRoleAlreadyExists_DoesNotCreateIt() {
            var createdIdentityRoles = new List<IdentityRole>();

            var roleManager = CreateRoleManagerMock();
            roleManager
                .Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            roleManager
                .Setup(x => x.CreateAsync(It.IsAny<IdentityRole>()))
                .Callback((IdentityRole ir) => createdIdentityRoles.Add(ir));

            var authService = new AuthService(roleManager.Object, CreateUserManagerMock().Object, CreateTokenFactoryMock().Object, CreateUserStatsRepositoryMock().Object);

            await authService.CreateRolesAsync<UserRoles>();

            Assert.DoesNotContain(createdIdentityRoles, x => "Admin".Equals(x.Name));
            Assert.DoesNotContain(createdIdentityRoles, x => "Moderator".Equals(x.Name));
            Assert.DoesNotContain(createdIdentityRoles, x => "User".Equals(x.Name));
        }
        #endregion

        #region LoginUserAsync
        [Fact]
        public async Task LoginUserAsync_WhenGivenUserData_AndEmailWrong_Exception() {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser) null);

            var authService = new AuthService(CreateRoleManagerMock().Object, userManagerMock.Object, CreateTokenFactoryMock().Object, CreateUserStatsRepositoryMock().Object);

            var loginForm = new LoginApiModel() {
                Email = "test@test.com",
                Password = "Password1!",
            };

            var result = await authService.LoginUserAsync(loginForm);

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is AuthFailureException);
        }

        [Fact]
        public async Task LoginUserAsync_WhenGivenUserData_AndPasswordWrong_Exception() {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManagerMock
                .Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var authService = new AuthService(CreateRoleManagerMock().Object, userManagerMock.Object, CreateTokenFactoryMock().Object, CreateUserStatsRepositoryMock().Object);

            var loginForm = new LoginApiModel() {
                Email = "test@test.com",
                Password = "Password1!",
            };

            var result = await authService.LoginUserAsync(loginForm);

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is AuthFailureException);
        }

        [Fact]
        public async Task LoginUserAsync_WhenGivenUserData_AndAllCorrect_LoginUser() {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManagerMock
                .Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            userManagerMock
                .Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>() { "User" });

            var tokenFactoryMock = CreateTokenFactoryMock();
            tokenFactoryMock
                .Setup(x => x.GetUserAuthClaimsFromRoles(new List<string>() { "User" }, It.IsAny<ApplicationUser>()))
                .Returns(new List<Claim>());
            tokenFactoryMock
                .Setup(x => x.GenerateNewJwtSecurityToken(It.IsAny<List<Claim>>()))
                .Returns(new JwtSecurityToken());

            var authService = new AuthService(CreateRoleManagerMock().Object, userManagerMock.Object, tokenFactoryMock.Object, CreateUserStatsRepositoryMock().Object);

            var loginForm = new LoginApiModel() {
                Email = "test@test.com",
                Password = "Password1!",
            };

            var result = await authService.LoginUserAsync(loginForm);

            Assert.True(result.IsSuccess);
        }
        #endregion

        #region RegisterUserAsync
        [Fact]
        public async Task RegisterUserAsync_WhenGivenRegisterModel_AndUserAlreadyExists_Exception() {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var authService = new AuthService(CreateRoleManagerMock().Object, userManagerMock.Object, CreateTokenFactoryMock().Object, CreateUserStatsRepositoryMock().Object);

            var registerApiModel = new RegisterApiModel() {
                Email = "test@test.com",
                Password = "Password1!",
                UserName = "test"
            };

            var result = await authService.RegisterUserAsync(registerApiModel, "User");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is AuthFailureException);
        }
        [Fact]
        public async Task RegisterUserAsync_WhenGivenRegisterModel_AndFailedToCreate_Exception() {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var authService = new AuthService(CreateRoleManagerMock().Object, userManagerMock.Object, CreateTokenFactoryMock().Object, CreateUserStatsRepositoryMock().Object);

            var registerApiModel = new RegisterApiModel() {
                Email = "test@test.com",
                Password = "Password1!",
                UserName = "test"
            };

            var result = await authService.RegisterUserAsync(registerApiModel, "User");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is AuthFailureException);
        }

        [Fact]
        public async Task RegisterUserAsync_WhenGivenRegisterModel_AndRoleDoesNotExist_Exception() {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());
            userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var roleManager = CreateRoleManagerMock();
            roleManager
                .Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var authService = new AuthService(roleManager.Object, userManagerMock.Object, CreateTokenFactoryMock().Object, CreateUserStatsRepositoryMock().Object);

            var registerApiModel = new RegisterApiModel() {
                Email = "test@test.com",
                Password = "Password1!",
                UserName = "test"
            };

            var result = await authService.RegisterUserAsync(registerApiModel, "User");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is AuthFailureException);
        }

        [Fact]
        public async Task RegisterUserAsync_WhenGivenRegisterModel_AndEverythingIsFine_LoginUser() {
            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .SetupSequence(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser) null)
                .ReturnsAsync(new ApplicationUser());
            userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock
                .Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            userManagerMock
                .Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>() { "User" });

            var tokenFactoryMock = CreateTokenFactoryMock();
            tokenFactoryMock
                .Setup(x => x.GetUserAuthClaimsFromRoles(new List<string>() { "User" }, It.IsAny<ApplicationUser>()))
                .Returns(new List<Claim>());
            tokenFactoryMock
                .Setup(x => x.GenerateNewJwtSecurityToken(It.IsAny<List<Claim>>()))
                .Returns(new JwtSecurityToken());

            var roleManager = CreateRoleManagerMock();
            roleManager
                .Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var statsServiceMock = CreateUserStatsRepositoryMock();
            statsServiceMock.Setup(x => x.AddUserStatsAsync(It.IsAny<UserStats>()))
                .ReturnsAsync(1);

            var authService = new AuthService(roleManager.Object, userManagerMock.Object, tokenFactoryMock.Object, statsServiceMock.Object);

            var registerApiModel = new RegisterApiModel() {
                Email = "test@test.com",
                Password = "Password1!",
                UserName = "test"
            };

            var result = await authService.RegisterUserAsync(registerApiModel, "User");

            Assert.True(result.IsSuccess);
        }
        #endregion
    }
}
