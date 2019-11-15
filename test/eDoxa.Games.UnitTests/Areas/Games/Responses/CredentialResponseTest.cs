// Filename: CredentialResponseTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Api.Areas.Games.Responses;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Games.UnitTests.Areas.Games.Responses
{
    public sealed class CredentialResponseTest : UnitTest
    {
        public CredentialResponseTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToIntegrationEvent()
        {
            //Arrange
            var response = new CredentialResponse() { Game = Game.LeagueOfLegends, PlayerId = new PlayerId(), UserId = new UserId()};

            var serializedReponse = JsonConvert.SerializeObject(response);

            //Act
            var responseDeserialized = JsonConvert.DeserializeObject<CredentialResponse>(serializedReponse);

            //Assert
            responseDeserialized.Should().BeEquivalentTo(response);
        }
    }
}
