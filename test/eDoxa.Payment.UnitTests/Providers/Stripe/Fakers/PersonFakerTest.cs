// Filename: PersonFakerTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Providers.Stripe.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Payment.UnitTests.Providers.Stripe.Fakers
{
    [TestClass]
    public sealed class PersonFakerTest
    {
        [TestMethod]
        public void FakePerson_ShouldNotBeNull()
        {
            // Arrange
            var personFaker = new PersonFaker();

            // Act
            var person = personFaker.FakePerson();

            // Assert
            person.Should().NotBeNull();
        }
    }
}
