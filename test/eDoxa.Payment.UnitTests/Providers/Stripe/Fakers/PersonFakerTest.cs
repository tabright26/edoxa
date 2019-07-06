// Filename: PersonFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Payment.Api.Providers.Stripe.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Payment.UnitTests.Providers.Stripe.Fakers
{
    [TestClass]
    public sealed class PersonFakerTest
    {
        [TestMethod]
        public void FakePerson_ShouldNotThrow()
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
