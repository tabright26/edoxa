// Filename: Class1.cs
// Date Created: 2019-12-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

using Microsoft.AspNetCore.Routing;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests
{
    public class Class1 : IntegrationTest
    {
        public Class1(
            TestHostFixture testHost,
            TestDataFixture testData,
            TestMapperFixture testMapper,
            Func<HttpClient, LinkGenerator, object, Task<HttpResponseMessage>>? executeAsync = null
        ) : base(
            testHost,
            testData,
            testMapper,
            executeAsync)
        {
        }

        public IChallenge CreateChallenge()
        {
            var factory = new ChallengePayoutFactory();

            var entryFee = new EntryFee(Money.Fifty, Currency.Money);

            var payout = factory.CreateInstance().GetPayout(PayoutEntries.Five, entryFee);

            var challenge = new Challenge(new ChallengeId(), entryFee, payout);

            return challenge;
        }

        [Fact]
        public void Test()
        {
            var dto = new TransactionDto
            {
                Id = new TransactionId(),
                Type = TransactionTypeDto.Charge,
                Status = TransactionStatusDto.Pending,
                Description = "Test",
                Timestamp = DateTime.UtcNow.ToTimestamp(),
                Currency = CurrencyDto.Money,
                Amount = 5000
            };

            var serialize = JsonConvert.SerializeObject(dto);

            var deserializet = JsonConvert.DeserializeObject<TransactionDto>(serialize);

            dto.Should().BeEquivalentTo(deserializet);
        }

        [Fact]
        public async Task Test2()
        {
            var server = TestHost.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IChallengeService>();
                    await accountRepository.CreateChallengeAsync(new ChallengeId(), PayoutEntries.Five, new EntryFee(Money.Fifty, Currency.Money));
                    await accountRepository.CreateChallengeAsync(new ChallengeId(), PayoutEntries.Five, new EntryFee(Money.Fifty, Currency.Money));
                });

            await server.UsingScopeAsync(
                async scope =>
                {
                    var challengeQuery = scope.GetRequiredService<IChallengeQuery>();
                    var mapper = scope.GetRequiredService<IMapper>();
                    var challenges = await challengeQuery.FetchChallengesAsync();
                    var challengeDtos = mapper.Map<RepeatedField<ChallengePayoutDto>>(challenges);
                    challengeDtos.Should().HaveCount(2);
                });
        }
    }
}
