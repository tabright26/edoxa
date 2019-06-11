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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class SetupFakerTest
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] {CurrencyType.Money, typeof(MoneyEntryFee)};
                yield return new object[] {CurrencyType.Token, typeof(TokenEntryFee)};
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(Data))]
        public void FakeSetup(CurrencyType currency, Type entryFeeType)
        {
            // Arrange
            var setupFaker = new SetupFaker();

            // Act
            var setup = setupFaker.FakeSetup(currency);

            // Assert
            setup.EntryFee.Should().BeOfType(entryFeeType);

            Console.WriteLine(setup.DumbAsJson());
        }
    }
}
