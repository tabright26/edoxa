// Filename: LeagueOfLegendsChallengeScoringAdapter.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.Adapters;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Options;

namespace eDoxa.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsChallengeScoringAdapter : IChallengeScoringAdapter
    {
        private readonly IOptions<LeagueOfLegendsOptions> _optionsSnapshot;

        public LeagueOfLegendsChallengeScoringAdapter(IOptionsSnapshot<LeagueOfLegendsOptions> optionsSnapshot)
        {
            _optionsSnapshot = optionsSnapshot;
        }

        private LeagueOfLegendsOptions Options => _optionsSnapshot.Value;

        public Game Game => Game.LeagueOfLegends;

        public async Task<ChallengeScoringDto> GetScoringAsync()
        {
            return await Task.FromResult(Options.Scoring);
        }
    }
}
