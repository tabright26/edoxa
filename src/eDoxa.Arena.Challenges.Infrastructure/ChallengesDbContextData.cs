// Filename: ChallengesDbContextData.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Infrastructure
{
    public sealed class ChallengesDbContextData : IDbContextData
    {
        private readonly ChallengesDbContext _context;
        private readonly IChallengeService _challengeService;
        private readonly IHostingEnvironment _environment;

        public ChallengesDbContextData(IHostingEnvironment environment, ChallengesDbContext context, IChallengeService challengeService)
        {
            _environment = environment;
            _context = context;
            _challengeService = challengeService;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Challenges.Any())
                {
                    // TODO: Temporary implementation.
                    await _challengeService.CreateChallengeAsync(
                        $"Challenge ({ChallengeState.Inscription})",
                        Game.LeagueOfLegends,
                        1,
                        3,
                        10,
                        new EntryFee(CurrencyType.Money, 20M),
                        new TestMode(ChallengeState.Inscription, TestModeMatchQuantity.Exact, TestModeParticipantQuantity.HalfFull)
                    );

                    // TODO: Temporary implementation.
                    await _challengeService.CreateChallengeAsync(
                        $"Challenge ({ChallengeState.InProgress})",
                        Game.LeagueOfLegends,
                        1,
                        5,
                        100,
                        new EntryFee(CurrencyType.Money, 2.5M),
                        new TestMode(ChallengeState.InProgress, TestModeMatchQuantity.Under, TestModeParticipantQuantity.Fulfilled)
                    );

                    // TODO: Temporary implementation.
                    await _challengeService.CreateChallengeAsync(
                        $"Challenge ({ChallengeState.InProgress})",
                        Game.LeagueOfLegends,
                        1,
                        3,
                        25,
                        new EntryFee(CurrencyType.Money, 10M),
                        new TestMode(ChallengeState.InProgress, TestModeMatchQuantity.Under, TestModeParticipantQuantity.Fulfilled)
                    );

                    // TODO: Temporary implementation.
                    await _challengeService.CreateChallengeAsync(
                        $"Challenge ({ChallengeState.Ended})",
                        Game.LeagueOfLegends,
                        1,
                        3,
                        20,
                        new EntryFee(CurrencyType.Money, 100M),
                        new TestMode(ChallengeState.Ended, TestModeMatchQuantity.Exact, TestModeParticipantQuantity.Fulfilled)
                    );

                    // TODO: Temporary implementation.
                    await _challengeService.CreateChallengeAsync(
                        $"Challenge ({ChallengeState.Closed})",
                        Game.LeagueOfLegends,
                        1,
                        3,
                        25,
                        new EntryFee(CurrencyType.Money, 50M),
                        new TestMode(ChallengeState.Closed, TestModeMatchQuantity.Exact, TestModeParticipantQuantity.Fulfilled)
                    );
                }
            }
        }
    }
}
