// Filename: StatNameTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Domain.Tests.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class StatNameTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<StatName>.For(typeof(PropertyInfo))
                .WithName("StatName")
                .Assert();
        }
    }
}