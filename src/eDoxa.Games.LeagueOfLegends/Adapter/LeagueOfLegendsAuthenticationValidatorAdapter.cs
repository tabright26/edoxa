// Filename: LeagueOfLegendsAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using RiotSharp.Misc;

namespace eDoxa.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthenticationValidatorAdapter : AuthenticationValidatorAdapter<LeagueOfLegendsAuthentication>
    {
        private readonly ILeagueOfLegendsService _leagueOfLegendsService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public LeagueOfLegendsAuthenticationValidatorAdapter(ILeagueOfLegendsService leagueOfLegendsService, IAuthenticationRepository authenticationRepository)
        {
            _leagueOfLegendsService = leagueOfLegendsService;
            _authenticationRepository = authenticationRepository;
        }

        public override Game Game => Game.LeagueOfLegends;

        public override async Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, LeagueOfLegendsAuthentication authentication)
        {
            await _authenticationRepository.RemoveAuthenticationAsync(userId, Game);

            var summoner = await _leagueOfLegendsService.Summoner.GetSummonerByAccountIdAsync(Region.Na, authentication.PlayerId);
            
            if (summoner.ProfileIconId != authentication.Factor.ExpectedSummonerProfileIconId)
            {
                return new ValidationFailure("_error", $"{Game} authentication process failed.").ToResult();
            }

            return new ValidationResult();
        }
    }
}
