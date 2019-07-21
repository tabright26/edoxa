// Filename: StatWeightingTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class StatWeightingTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "StatWeighting validation failed.";

            TestConstructor<StatWeighting>.ForParameters(typeof(float))
                .WithClassName("StatWeighting")
                .Success(new object[] {byte.MinValue}, message)
                .Success(new object[] {byte.MaxValue}, message)
                .Success(new object[] {short.MinValue}, message)
                .Success(new object[] {short.MaxValue}, message)
                .Success(new object[] {int.MinValue}, message)
                .Success(new object[] {int.MaxValue}, message)
                .Success(new object[] {float.MinValue}, message)
                .Success(new object[] {float.MaxValue}, message)
                .Assert();
        }
    }
}
