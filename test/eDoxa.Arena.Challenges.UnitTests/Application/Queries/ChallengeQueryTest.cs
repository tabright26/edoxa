// Filename: ChallengeQueryTest.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.UnitTests.Asserts;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class ChallengeQueryTest
    {
        [TestMethod]
        public void M()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            var challenge = challengeFaker.Generate();

            var mapper = CreateMapper();

            // Act
            var challengeViewModel = mapper.Map<ChallengeViewModel>(challenge);

            // Assert
            ChallengeQueryAssert.IsMapped(challengeViewModel);
        }

        private static IMapper CreateMapper()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var provider = services.BuildServiceProvider();

            return provider.GetService<IMapper>();
        }
    }
}
