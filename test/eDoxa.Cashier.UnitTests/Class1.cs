// Filename: Class1.cs
// Date Created: 2019-12-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Seedwork.Application.Converters;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Cashier.UnitTests
{
    public class Class1 : UnitTest
    {
        public Class1(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
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

            var settings = new JsonSerializerSettings();
            
            settings.Converters.Add(new DecimalValueConverter());

            var serialize = JsonConvert.SerializeObject(dto, settings);

            var deserializet = JsonConvert.DeserializeObject<TransactionDto>(serialize, settings);

            dto.Should().BeEquivalentTo(deserializet);
        }

        [Fact]
        public void Test2()
        {
            var challenge1 = this.CreateChallenge();

            var challenge2 = this.CreateChallenge();

            var challenges = new List<IChallenge>
            {
                challenge1,
                challenge2
            };

            var challengeDto = TestMapper.Map<IEnumerable<ChallengePayoutDto>>(challenges);
        }

        public IChallenge CreateChallenge()
        {
            var factory = new ChallengePayoutFactory();

            var entryFee = new EntryFee(Money.Fifty, Currency.Money);

            var payout = factory.CreateInstance().GetPayout(PayoutEntries.Five, entryFee);

            var challenge = new Challenge(new ChallengeId(), entryFee, payout);

            return challenge;
        }
    }
}
