// Filename: RegisterParticipantRequestValidatorTest.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Domain.AggregateModels;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Cashier.UnitTests.Areas.Account.Requests
{
    [TestClass]
    public sealed class AccountWithdrawalPostRequestTest
    {
        [TestMethod]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            //Arrange
            var request = new AccountWithdrawalPostRequest(Money.Fifty);

            var serializedEvent = JsonConvert.SerializeObject(request);

            //Act
            var deserializedEvent = JsonConvert.DeserializeObject<AccountWithdrawalPostRequest>(serializedEvent);

            //Assert
            deserializedEvent.Should().BeEquivalentTo(request);
        }
    }
}

