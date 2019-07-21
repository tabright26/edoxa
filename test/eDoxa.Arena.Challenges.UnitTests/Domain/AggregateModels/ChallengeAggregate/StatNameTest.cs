// Filename: StatNameTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Reflection;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class StatNameTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<StatName>.ForParameters(typeof(PropertyInfo)).WithClassName("StatName").Assert();
        }
    }
}
