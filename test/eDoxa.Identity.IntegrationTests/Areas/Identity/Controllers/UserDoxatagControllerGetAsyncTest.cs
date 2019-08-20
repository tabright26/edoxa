//// Filename: UserDoxatagControllerGetAsyncTest.cs
//// Date Created: 2019-08-10
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;

//using AutoMapper;

//using eDoxa.Identity.Api.Areas.Identity.Responses;
//using eDoxa.Identity.Api.Areas.Identity.Services;
//using eDoxa.Identity.Api.Infrastructure.Data.Storage;
//using eDoxa.Identity.Api.Infrastructure.Models;
//using eDoxa.Seedwork.Application.Extensions;
//using eDoxa.Seedwork.Testing.Extensions;
//using eDoxa.Seedwork.Testing.Http.Extensions;

//using FluentAssertions;

//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.TestHost;

//using Xunit;

//namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
//{
//    public sealed class UserDoxaTagControllerGetAsyncTest : IClassFixture<IdentityWebApiFactory>
//    {
//        public UserDoxaTagControllerGetAsyncTest(IdentityWebApiFactory identityWebApiFactory)
//        {
//            User = new HashSet<User>(IdentityStorage.TestUsers).First();
//            _httpClient = identityWebApiFactory.CreateClient();
//            _testServer = identityWebApiFactory.Server;
//            _testServer.CleanupDbContext();
//        }

//        private readonly TestServer _testServer;
//        private readonly HttpClient _httpClient;

//        private User User { get; }

//        private async Task<HttpResponseMessage> ExecuteAsync(Guid userId)
//        {
//            return await _httpClient.GetAsync($"api/users/{userId}/doxa-tag");
//        }

//        [Fact]
//        public async Task GetAsync_ShouldBeStatus200OK()
//        {
//            var doxaTag = new UserDoxaTag
//            {
//                Id = Guid.NewGuid(),
//                UserId = User.Id,
//                Name = "Test",
//                Code = 12345,
//                Timestamp = DateTime.UtcNow
//            };

//            User.DoxaTagHistory = new Collection<UserDoxaTag>
//            {
//                doxaTag
//            };

//            await _testServer.UsingScopeAsync(
//                async scope =>
//                {
//                    var userManager = scope.GetRequiredService<UserManager>();

//                    var result = await userManager.CreateAsync(User);

//                    result.Succeeded.Should().BeTrue();
//                }
//            );

//            // Act
//            using var response = await this.ExecuteAsync(User.Id);

//            // Assert
//            response.EnsureSuccessStatusCode();

//            response.StatusCode.Should().Be(StatusCodes.Status200OK);

//            await _testServer.UsingScopeAsync(
//                async scope =>
//                {
//                    var mapper = scope.GetRequiredService<IMapper>();

//                    var doxaTagResponse = await response.DeserializeAsync<UserDoxaTagResponse>();

//                    var expectedDoxaTagResponse = mapper.Map<UserDoxaTagResponse>(doxaTag);

//                    doxaTagResponse.Name.Should().Be(expectedDoxaTagResponse.Name);

//                    doxaTagResponse.Code.Should().Be(expectedDoxaTagResponse.Code);
//                }
//            );
//        }

//        [Fact]
//        public async Task GetAsync_ShouldBeStatus204NoContent()
//        {
//            User.DoxaTagHistory = null;

//            await _testServer.UsingScopeAsync(
//                async scope =>
//                {
//                    var userManager = scope.GetRequiredService<UserManager>();

//                    var result = await userManager.CreateAsync(User);

//                    result.Succeeded.Should().BeTrue();
//                }
//            );

//            // Act
//            using var response = await this.ExecuteAsync(User.Id);

//            // Assert
//            response.EnsureSuccessStatusCode();

//            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
//        }
//    }
//}
