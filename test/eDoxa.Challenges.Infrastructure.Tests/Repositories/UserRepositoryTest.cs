// Filename: UserRepositoryTest.cs
// Date Created: 2019-03-19
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Infrastructure.Tests.Repositories
{
    [TestClass]
    public sealed class UserRepositoryTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Create_User_ShouldNotBeEmpty()
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new UserRepository(context);

                    // Act
                    repository.Create(_userAggregateFactory.CreateUser());

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Assert
                    context.Users.Should().NotBeEmpty();
                }
            }
        }
    }
}