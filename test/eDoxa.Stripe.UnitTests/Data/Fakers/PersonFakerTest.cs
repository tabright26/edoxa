﻿// Filename: PersonFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Stripe.Data.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Stripe.UnitTests.Data.Fakers
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
            var action = new Action(
                () =>
                {
                    var person = personFaker.FakePerson();

                    Console.WriteLine(person.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
