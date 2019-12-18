// Filename: LeagueOfLegendsAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using RiotSharp.Misc;

namespace eDoxa.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthenticationValidatorAdapter : AuthenticationValidatorAdapter<LeagueOfLegendsGameAuthentication>
    {
        private readonly ILeagueOfLegendsService _leagueOfLegendsService;
        private readonly IGameAuthenticationRepository _gameAuthenticationRepository;

        public LeagueOfLegendsAuthenticationValidatorAdapter(ILeagueOfLegendsService leagueOfLegendsService, IGameAuthenticationRepository gameAuthenticationRepository)
        {
            _leagueOfLegendsService = leagueOfLegendsService;
            _gameAuthenticationRepository = gameAuthenticationRepository;
        }

        public override Game Game => Game.LeagueOfLegends;

        public override async Task<IDomainValidationResult> ValidateAuthenticationAsync(UserId userId, LeagueOfLegendsGameAuthentication gameAuthentication)
        {
            await _gameAuthenticationRepository.RemoveAuthenticationAsync(userId, Game);

            var summoner = await _leagueOfLegendsService.Summoner.GetSummonerByAccountIdAsync(Region.Na, gameAuthentication.PlayerId);
            
            if (summoner.ProfileIconId != gameAuthentication.Factor.ExpectedSummonerProfileIconId)
            {
                return DomainValidationResult.Failure("_error", $"{Game} authentication process failed.");
            }

            return new DomainValidationResult();
        }
    }
}
