﻿// Filename: ChallengesDbContextData.cs
// Date Created: 2019-06-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data
{
    public sealed class ChallengesDbContextData : IDbContextData
    {
        private readonly ChallengesDbContext _context;
        private readonly IHostingEnvironment _environment;

        public ChallengesDbContextData(ChallengesDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Challenges.Any())
                {
                    var challengeFaker = new ChallengeFaker();

                    _context.Challenges.AddRange(challengeFaker.Generate(10).ToModels());

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Challenges.RemoveRange(_context.Challenges);

                await _context.SaveChangesAsync();
            }
        }
    }
}