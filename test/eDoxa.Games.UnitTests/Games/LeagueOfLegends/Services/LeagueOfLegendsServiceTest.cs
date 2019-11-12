// Filename: LeagueOfLegendsServiceTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Factories;
using eDoxa.Games.Api.IntegrationEvents;
using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Services;
using eDoxa.Games.Services;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Security;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Moq;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Games.UnitTests.Games.LeagueOfLegends.Services
{
    public sealed class LeagueOfLegendsServiceTest : UnitTest
    {
        public LeagueOfLegendsServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void Constructor()
        {
            //Arrange
            var leagueOptions = new LeagueOfLegendsOptions
            {
                ApiKey = "testKey"
            };

            var leagueService = new LeagueOfLegendsService(
                new OptionsWrapper<LeagueOfLegendsOptions>(leagueOptions));

            //Assert
            leagueService.Should().BeOfType<LeagueOfLegendsService>();
        }
    }
}
