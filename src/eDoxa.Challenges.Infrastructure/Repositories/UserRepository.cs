// Filename: UserRepository.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Infrastructure.Repositories
{
    public sealed partial class UserRepository
    {
        private readonly ChallengesDbContext _context;

        public UserRepository(ChallengesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
    }

    public sealed partial class UserRepository : IUserRepository
    {
        public void Create(User user)
        {
            _context.Users.Add(user ?? throw new ArgumentNullException(nameof(user)));
        }
    }
}