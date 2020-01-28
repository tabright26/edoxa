// Filename: IdentityGrpcServiceTest.cs
// Date Created: 2020-01-17
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Responses;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Grpc.Core;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Grpc.Services
{
    public sealed class IdentityGrpcServiceTest : IntegrationTest
    {
        public IdentityGrpcServiceTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        [Fact]
        public async Task AddUserClaim_ShouldAlreadyExists()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();
                });

            var request = new AddUserClaimRequest
            {
                UserId = user.Id.ToString(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var response = await client.AddUserClaimAsync(request);

            // Assert
            response.Should().BeOfType<AddUserClaimResponse>();
        }

        [Fact]
        public async Task AddUserClaim_ShouldBeOfTypeAddUserClaimResponse()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();
                });

            var request = new AddUserClaimRequest
            {
                UserId = user.Id.ToString(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var response = await client.AddUserClaimAsync(request);

            // Assert
            response.Should().BeOfType<AddUserClaimResponse>();
        }

        [Fact]
        public void AddUserClaim_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            var request = new AddUserClaimRequest();

            // Act
            var func = new Func<Task>(async () => await client.AddUserClaimAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void AddUserClaim_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            var request = new AddUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var func = new Func<Task>(async () => await client.AddUserClaimAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task AddUserToRole_ShouldAlreadyExists()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();
                });

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var roleService = scope.GetRequiredService<IRoleService>();

                    var result = await roleService.CreateAsync(
                        new Role
                        {
                            Name = AppRoles.Admin
                        });

                    result.Succeeded.Should().BeTrue();
                });

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.AddToRoleAsync(await userService.FindByIdAsync(user.Id.ToString()), AppRoles.Admin);

                    result.Succeeded.Should().BeTrue();
                });

            var request = new AddUserToRoleRequest
            {
                UserId = user.Id.ToString(),
                RoleName = AppRoles.Admin
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var func = new Func<Task>(async () => await client.AddUserToRoleAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task AddUserToRole_ShouldBeOfTypeAddUserToRoleResponse()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();
                });

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var roleService = scope.GetRequiredService<IRoleService>();

                    var result = await roleService.CreateAsync(
                        new Role
                        {
                            Name = AppRoles.Admin
                        });

                    result.Succeeded.Should().BeTrue();
                });

            var request = new AddUserToRoleRequest
            {
                UserId = user.Id.ToString(),
                RoleName = AppRoles.Admin
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var response = await client.AddUserToRoleAsync(request);

            // Assert
            response.Should().BeOfType<AddUserToRoleResponse>();
        }

        [Fact]
        public void AddUserToRole_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new AddUserToRoleRequest
            {
                UserId = userId,
                RoleName = "test"
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.AddUserToRoleAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void AddUserToRole_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new AddUserToRoleRequest
            {
                UserId = userId,
                RoleName = "test"
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.AddUserToRoleAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task RemoveUserClaim_ShouldBeOfTypeRemoveUserClaimResponse()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();

                    await userService.AddClaimAsync(user, new Claim(CustomClaimTypes.StripeAccount, "accountId"));
                });

            var request = new RemoveUserClaimRequest
            {
                UserId = user.Id.ToString(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var reponse = await client.RemoveUserClaimAsync(request);

            // Assert
            reponse.Should().BeOfType<RemoveUserClaimResponse>();
        }

        [Fact]
        public async Task RemoveUserClaim_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();
                });

            var request = new RemoveUserClaimRequest
            {
                UserId = user.Id.ToString(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var func = new Func<Task>(async () => await client.RemoveUserClaimAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void RemoveUserClaim_WithInvalidClaim_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            var request = new RemoveUserClaimRequest
            {
                UserId = user.Id.ToString(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var func = new Func<Task>(async () => await client.RemoveUserClaimAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task RemoveUserFromRole_ShouldBeOfTypeRemoveUserFromRoleResponse()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();
                });

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var roleService = scope.GetRequiredService<IRoleService>();

                    var result = await roleService.CreateAsync(
                        new Role
                        {
                            Name = AppRoles.Admin
                        });

                    result.Succeeded.Should().BeTrue();
                });

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.AddToRoleAsync(await userService.FindByIdAsync(user.Id.ToString()), AppRoles.Admin);

                    result.Succeeded.Should().BeTrue();
                });

            var request = new RemoveUserFromRoleRequest
            {
                UserId = user.Id.ToString(),
                RoleName = AppRoles.Admin
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var response = await client.RemoveUserFromRoleAsync(request);

            // Assert
            response.Should().BeOfType<RemoveUserFromRoleResponse>();
        }

        [Fact]
        public void RemoveUserFromRole_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            var request = new RemoveUserFromRoleRequest
            {
                UserId = user.Id.ToString(),
                RoleName = "test"
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.RemoveUserFromRoleAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task RemoveUserFromRole_WithInvalidRole_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();
                });

            var request = new RemoveUserFromRoleRequest
            {
                UserId = user.Id.ToString(),
                RoleName = "test"
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.RemoveUserFromRoleAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void RemoveUserFromRole_WithUserNull_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            var request = new RemoveUserFromRoleRequest
            {
                UserId = user.Id.ToString(),
                RoleName = "test"
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.RemoveUserFromRoleAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task ReplaceUserClaim_ShouldBeOfTypeReplaceUserClaimResponse()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.CreateAsync(user, "Pass@word1");

                    result.Succeeded.Should().BeTrue();
                });

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var result = await userService.AddClaimAsync(await userService.FindByIdAsync(user.Id.ToString()), new Claim(CustomClaimTypes.StripeAccount, "accountId"));

                    result.Succeeded.Should().BeTrue();
                });

            var request = new ReplaceUserClaimRequest
            {
                UserId = user.Id.ToString(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "stripeAccountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var response = await client.ReplaceUserClaimAsync(request);

            // Assert
            response.Should().BeOfType<ReplaceUserClaimResponse>();
        }

        [Fact]
        public void ReplaceUserClaim_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            var request = new ReplaceUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var func = new Func<Task>(async () => await client.ReplaceUserClaimAsync(request));

            // Assert
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void ReplaceUserClaim_WithInvalidClaim_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            var request = new ReplaceUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.ReplaceUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void ReplaceUserClaim_WithUserNull_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var users = TestData.FileStorage.GetUsers();

            var user = users.First();

            var host = TestHost.WithClaimsFromBearerAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));

            host.Server.CleanupDbContext();

            var request = new ReplaceUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = CustomClaimTypes.StripeAccount,
                    Value = "accountId"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.ReplaceUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }
    }
}
