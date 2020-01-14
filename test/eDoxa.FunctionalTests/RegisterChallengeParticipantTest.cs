// Filename: RegisterChallengeParticipantTest.cs
// Date Created: 2019-12-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.FunctionalTests.TestHelper.Aggregators.Web.Challenges;
using eDoxa.FunctionalTests.TestHelper.Services.Cashier;
using eDoxa.FunctionalTests.TestHelper.Services.Challenges;
using eDoxa.FunctionalTests.TestHelper.Services.Games;
using eDoxa.FunctionalTests.TestHelper.Services.Identity;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.ServiceBus.Abstractions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

using Challenge = eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge;
using ChallengePayout = eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate.Challenge;
using IChallengePayoutRepository = eDoxa.Cashier.Domain.Repositories.IChallengeRepository;
using IChallengeRepository = eDoxa.Challenges.Domain.Repositories.IChallengeRepository;

namespace eDoxa.FunctionalTests
{
    public sealed class RegisterChallengeParticipantTest
    {
        private static readonly IScoring Scoring = new Scoring(
            new Dictionary<string, float>
            {
                ["StatName1"] = 5,
                ["StatName2"] = 10,
                ["StatName3"] = 15,
                ["StatName4"] = 20,
                ["StatName5"] = 25
            });

        [Fact]
        public async Task Success()
        {
            // Arrange
            var mockServiceBusPubliser = new Mock<IServiceBusPublisher>();

            mockServiceBusPubliser.Setup(serviceBusPubliser => serviceBusPubliser.PublishAsync(It.IsAny<ChallengeParticipantRegisteredIntegrationEvent>()))
                .Verifiable();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@edoxa.gg",
                EmailConfirmed = true,
                UserName = "test@edoxa.gg",
                Country = Country.Canada
            };

            var account = new Account(user.Id.From<UserId>());

            var moneyAccount = new MoneyAccountDecorator(account);

            moneyAccount.Deposit(Money.TwoHundred).MarkAsSucceeded();

            var doxatag = new Doxatag(
                account.Id,
                "TestUser",
                1000,
                new UtcNowDateTimeProvider());

            var gamePlayerId = new PlayerId();

            var challenge = new Challenge(
                new ChallengeId(),
                new ChallengeName("TestChallenge"),
                Game.LeagueOfLegends,
                BestOf.One,
                Entries.Two,
                new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.OneDay),
                Scoring);

            var payout = new ChallengePayoutFactory();

            var challengePayout = new ChallengePayout(
                challenge.Id,
                MoneyEntryFee.OneHundred,
                payout.CreateInstance().GetPayout(PayoutEntries.One, MoneyEntryFee.OneHundred));

            using var gamesHost = new GamesHostFactory().WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, account.Id));

            gamesHost.Server.CleanupDbContext();

            await gamesHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var gameCredentialRepository = scope.GetRequiredService<IGameCredentialRepository>();

                    gameCredentialRepository.CreateCredential(
                        new Credential(
                            account.Id,
                            challenge.Game,
                            gamePlayerId,
                            new UtcNowDateTimeProvider()));

                    await gameCredentialRepository.UnitOfWork.CommitAsync(false);
                });

            var gameServiceClient = new GameService.GameServiceClient(gamesHost.CreateChannel());

            using var challengesHost = new ChallengesHostFactory().WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, account.Id)).WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            container.RegisterInstance(mockServiceBusPubliser.Object).As<IServiceBusPublisher>().SingleInstance();
                        });
                });

            challengesHost.Server.CleanupDbContext();

            await challengesHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();

                    challengeRepository.Create(challenge);

                    await challengeRepository.CommitAsync(false);
                });

            var challengeServiceClient = new ChallengeService.ChallengeServiceClient(challengesHost.CreateChannel());

            using var cashierHost = new CashierHostFactory().WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, account.Id));

            cashierHost.Server.CleanupDbContext();

            await cashierHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();

                    accountRepository.Create(account);

                    await accountRepository.CommitAsync();
                });

            await cashierHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengePayoutRepository>();

                    challengeRepository.Create(challengePayout);

                    await challengeRepository.CommitAsync();
                });

            var cashierServiceClient = new CashierService.CashierServiceClient(cashierHost.CreateChannel());

            using var identityHost = new IdentityHostFactory();

            identityHost.Server.CleanupDbContext();

            await identityHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var doxatagRepository = scope.GetRequiredService<IUserService>();

                    await doxatagRepository.CreateAsync(user, "Pass@word1");
                });

            await identityHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var doxatagRepository = scope.GetRequiredService<IDoxatagRepository>();

                    doxatagRepository.Create(doxatag);

                    await doxatagRepository.UnitOfWork.CommitAsync(false);
                });

            var identityServiceClient = new IdentityService.IdentityServiceClient(identityHost.CreateChannel());

            using var challengesAggregatorHost = new ChallengesWebAggregatorHostFactory().WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, account.Id))
                .WithWebHostBuilder(
                    builder =>
                    {
                        builder.ConfigureTestContainer<ContainerBuilder>(
                            container =>
                            {
                                container.RegisterInstance(challengeServiceClient).SingleInstance();

                                container.RegisterInstance(cashierServiceClient).SingleInstance();

                                container.RegisterInstance(identityServiceClient).SingleInstance();

                                container.RegisterInstance(gameServiceClient).SingleInstance();
                            });
                    });

            var client = challengesAggregatorHost.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync(
                $"api/challenges/{challenge.Id}/participants",
                new
                {
                });

            // Assert
            response.EnsureSuccessStatusCode();

            mockServiceBusPubliser.Verify(serviceBusPubliser => serviceBusPubliser.PublishAsync(It.IsAny<ChallengeParticipantRegisteredIntegrationEvent>()), Times.Once);
        }
    }
}
