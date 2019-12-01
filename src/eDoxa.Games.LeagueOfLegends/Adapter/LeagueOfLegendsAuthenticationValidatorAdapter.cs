// Filename: LeagueOfLegendsAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Seedwork.Application.FluentValidation.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using FluentValidation.Results;

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

        public override async Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, LeagueOfLegendsGameAuthentication gameAuthentication)
        {
            await _gameAuthenticationRepository.RemoveAuthenticationAsync(userId, Game);

            var summoner = await _leagueOfLegendsService.Summoner.GetSummonerByAccountIdAsync(Region.Na, gameAuthentication.PlayerId);
            
            if (summoner.ProfileIconId != gameAuthentication.Factor.ExpectedSummonerProfileIconId)
            {
                return new ValidationFailure("_error", $"{Game} authentication process failed.").ToResult();
            }

            return new ValidationResult();
        }
    }
}
