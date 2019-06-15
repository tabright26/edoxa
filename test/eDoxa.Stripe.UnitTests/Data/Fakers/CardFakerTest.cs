// Filename: CardFakerTest.cs
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
    public sealed class CardFakerTest
    {
        [TestMethod]
        public void FakeCard_ShouldNotThrow()
        {
            // Arrange
            var cardFaker = new CardFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var card = cardFaker.FakeCard();

                    Console.WriteLine(card.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeCards_ShouldNotThrow()
        {
            // Arrange
            var cardFaker = new CardFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var cards = cardFaker.FakeCards(5);

                    Console.WriteLine(cards.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
