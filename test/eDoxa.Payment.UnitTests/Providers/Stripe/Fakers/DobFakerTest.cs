// Filename: DobFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Payment.Api.Providers.Stripe.Fakers;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Payment.UnitTests.Providers.Stripe.Fakers
{
    [TestClass]
    public sealed class DobFakerTest
    {
        [TestMethod]
        public void FakeDob_ShouldNotThrow()
        {
            // Arrange
            var dobFaker = new DobFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var dob = dobFaker.FakeDob();

                    Console.WriteLine(dob.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
