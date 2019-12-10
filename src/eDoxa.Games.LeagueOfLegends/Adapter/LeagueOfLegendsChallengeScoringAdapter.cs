// Filename: LeagueOfLegendsChallengeScoringAdapter.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain.Dtos;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Options;

namespace eDoxa.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsChallengeScoringAdapter : IChallengeScoringAdapter
    {
        public LeagueOfLegendsChallengeScoringAdapter(IOptions<LeagueOfLegendsOptions> options)
        {
            Options = options.Value;
        }

        private LeagueOfLegendsOptions Options { get; }

        public Game Game => Game.LeagueOfLegends;

        public async Task<Scoring> GetScoringAsync()
        {
            return await Task.FromResult(new Scoring(Options.Scoring));
        }
    }
}
