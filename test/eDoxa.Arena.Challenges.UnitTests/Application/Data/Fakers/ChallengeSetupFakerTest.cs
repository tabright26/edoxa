// Filename: SetupFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class ChallengeSetupFakerTest
    {
        public static IEnumerable<object[]> Data => CurrencyType.GetAll().Select(currency => new object[] {currency});

        [DataTestMethod]
        [DynamicData(nameof(Data))]
        public void FakeSetup(CurrencyType currency)
        {
            // Arrange
            var setupFaker = new ChallengeSetupFaker();

            // Act
            var setup = setupFaker.FakeSetup(currency);

            // Assert
            setup.EntryFee.Type.Should().Be(currency);

            Console.WriteLine(setup.DumbAsJson());
        }
    }
}
