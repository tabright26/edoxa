// Filename: RegisterParticipantRequestValidatorTest.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Application.Requests;
using eDoxa.Arena.Challenges.Api.Application.Requests.Validations;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

using FluentAssertions;

using FluentValidation.TestHelper;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Newtonsoft.Json;

namespace eDoxa.Cashier.UnitTests.Application.Requests
{
    [TestClass]
    public sealed class CreateUserRequestTest
    {
        [TestMethod]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            //Arrange
            var request = new CreateUserRequest();

            var serializedEvent = JsonConvert.SerializeObject(request);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<CreateUserRequest>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(request);
        }
    }
}
1