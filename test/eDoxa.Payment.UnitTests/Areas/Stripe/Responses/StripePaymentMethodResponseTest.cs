﻿// Filename: StripePaymentMethodResponseTest.cs
// Date Created: 2019-10-24
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Responses;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Payment.UnitTests.Areas.Stripe.Responses
{
    public sealed class StripePaymentMethodResponseTest : UnitTest
    {
        public StripePaymentMethodResponseTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            //Arrange
            var request = new StripePaymentMethodResponse();

            var requestSerialized = JsonConvert.SerializeObject(request);

            //Act
            var requestDeserialized = JsonConvert.DeserializeObject<StripePaymentMethodResponse>(requestSerialized);

            //Assert
            requestDeserialized.Should().BeEquivalentTo(request);
        }
    }
}
