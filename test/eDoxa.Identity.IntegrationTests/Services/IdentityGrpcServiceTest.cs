// Filename: IdentityGrpcServiceTest.cs
// Date Created: 2020-01-13
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Responses;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Grpc.Core;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Services
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
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var user = new User
                    {
                        Id = userId
                    };

                    await userService.CreateAsync(user, "test_123");

                    var userTest = await userService.FindByIdAsync(userId);
                });

            var request = new AddUserClaimRequest
            {
                UserId = userId,
                Claim = new UserClaimDto
                {
                    Type = "sub",
                    Value = userId
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var result = await client.AddUserClaimAsync(request);

            // Assert
            result.Should().BeOfType<AddUserClaimResponse>();
        }

        [Fact]
        public async Task AddUserClaim_ShouldBeOfTypeAddUserClaimResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var user = new User
                    {
                        Id = userId
                    };

                    await userService.CreateAsync(user, "test_123");

                    var userTest = await userService.FindByIdAsync(userId);
                });

            var request = new AddUserClaimRequest
            {
                UserId = userId,
                Claim = new UserClaimDto
                {
                    Type = "sub",
                    Value = userId
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var result = await client.AddUserClaimAsync(request);

            // Assert
            result.Should().BeOfType<AddUserClaimResponse>();
        }

        [Fact]
        public void AddUserClaim_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new AddUserClaimRequest();

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.AddUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void AddUserClaim_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new AddUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = "email",
                    Value = "test@edoxa.gg"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.AddUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task AddUserToRole_ShouldAlreadyExists()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var user = new User
                    {
                        Id = userId
                    };

                    await userService.CreateAsync(user, "test_123");

                    var userTest = await userService.FindByIdAsync(userId);
                });

            var request = new AddUserToRoleRequest
            {
                UserId = userId,
                RoleName = "test"
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var result = await client.AddUserToRoleAsync(request);

            // Assert
            result.Should().BeOfType<AddUserToRoleResponse>();
        }

        [Fact]
        public async Task AddUserToRole_ShouldBeOfTypeAddUserToRoleResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var user = new User
                    {
                        Id = userId
                    };

                    await userService.CreateAsync(user, "test_123");

                    var userTest = await userService.FindByIdAsync(userId);
                });

            var request = new AddUserToRoleRequest
            {
                UserId = userId,
                RoleName = "test"
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var result = await client.AddUserToRoleAsync(request);

            // Assert
            result.Should().BeOfType<AddUserToRoleResponse>();
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
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var user = new User
                    {
                        Id = userId
                    };

                    await userService.CreateAsync(user, "test_123");

                    var userTest = await userService.FindByIdAsync(userId);
                });

            var request = new RemoveUserClaimRequest
            {
                UserId = userId,
                Claim = new UserClaimDto
                {
                    Type = "sub",
                    Value = userId
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var result = await client.RemoveUserClaimAsync(request);

            // Assert
            result.Should().BeOfType<RemoveUserClaimResponse>();
        }

        [Fact]
        public void RemoveUserClaim_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new RemoveUserClaimRequest
            {
                UserId = userId,
                Claim = new UserClaimDto
                {
                    Type = "sub",
                    Value = userId
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.RemoveUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void RemoveUserClaim_WithInvalidClaim_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new RemoveUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = "email",
                    Value = "test@edoxa.gg"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.RemoveUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void RemoveUserClaim_WithUserNull_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new RemoveUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = "email",
                    Value = "test@edoxa.gg"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.RemoveUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task RemoveUserFromRole_ShouldBeOfTypeRemoveUserFromRoleResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var user = new User
                    {
                        Id = userId
                    };

                    await userService.CreateAsync(user, "test_123");
                    var userTest = await userService.FindByIdAsync(userId);
                });

            var request = new RemoveUserFromRoleRequest
            {
                UserId = userId,
                RoleName = "test"
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var result = await client.RemoveUserFromRoleAsync(request);

            // Assert
            result.Should().BeOfType<RemoveUserFromRoleResponse>();
        }

        [Fact]
        public void RemoveUserFromRole_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new RemoveUserFromRoleRequest
            {
                UserId = userId,
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
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var user = new User
                    {
                        Id = userId
                    };

                    await userService.CreateAsync(user, "test_123");
                    var userTest = await userService.FindByIdAsync(userId);
                });

            var request = new RemoveUserFromRoleRequest
            {
                UserId = userId,
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
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new RemoveUserFromRoleRequest
            {
                UserId = userId,
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
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var userService = scope.GetRequiredService<IUserService>();

                    var user = new User
                    {
                        Id = userId
                    };

                    await userService.CreateAsync(user, "test_123");

                    var userTest = await userService.FindByIdAsync(userId);
                });

            var request = new ReplaceUserClaimRequest
            {
                UserId = userId,
                Claim = new UserClaimDto
                {
                    Type = "sub",
                    Value = userId
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act
            var result = await client.ReplaceUserClaimAsync(request);

            // Assert
            result.Should().BeOfType<ReplaceUserClaimResponse>();
        }

        [Fact]
        public void ReplaceUserClaim_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new ReplaceUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = "email",
                    Value = "test@edoxa.gg"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.ReplaceUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public void ReplaceUserClaim_WithInvalidClaim_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new ReplaceUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = "email",
                    Value = "test@edoxa.gg"
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
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new ReplaceUserClaimRequest
            {
                UserId = new UserId(),
                Claim = new UserClaimDto
                {
                    Type = "email",
                    Value = "test@edoxa.gg"
                }
            };

            var client = new IdentityService.IdentityServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.ReplaceUserClaimAsync(request));
            func.Should().Throw<RpcException>();
        }
    }
}
