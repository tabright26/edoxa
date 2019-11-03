// Filename: StripeBankAccountPostRequestTest.cs
// Date Created: 2019-10-24
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Areas.Stripe.Requests;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Payment.UnitTests.Areas.Stripe.Requests
{
    public sealed class StripeBankAccountPostRequestTest : UnitTest
    {
        public StripeBankAccountPostRequestTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            //Arrange
            var request = new StripeBankAccountPostRequest("stripeToken");

            var requestSerialized = JsonConvert.SerializeObject(request);

            //Act
            var requestDeserialized = JsonConvert.DeserializeObject<StripeBankAccountPostRequest>(requestSerialized);

            //Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
