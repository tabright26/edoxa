// Filename: FirstPrizeTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Domain.Tests
{
    [TestClass]
    public sealed class FirstPrizeTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<FirstPrize>.For(typeof(PrizePool), typeof(PrizePoolRatio))
                .WithName("FirstPrize")
                .Assert();

            ConstructorTests<FirstPrize>.For(typeof(PrizePool))
                .WithName("FirstPrize")
                .Assert();
        }

        //[DataRow()]
        //[DataRow()]
        //[DataRow()]
        //[DataTestMethod]
        //public void Constructor_M_Tests(int prizePool, float prizePoolRatio)
        //{
        //    // Arrange
        //    var prizePool = new PrizePool();

        //}

        //[DataRow()]
        //[DataRow()]
        //[DataRow()]
        //[DataTestMethod]
        //public void Constructor_M1_Tests(int prizePool)
        //{
        //}
    }
}