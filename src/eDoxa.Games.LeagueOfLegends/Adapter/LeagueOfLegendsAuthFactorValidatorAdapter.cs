// Filename: LeagueOfLegendsAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using RiotSharp.Misc;

namespace eDoxa.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthFactorValidatorAdapter : IAuthFactorValidatorAdapter
    {
        private readonly ILeagueOfLegendsService _leagueOfLegendsService;
        private readonly IAuthFactorRepository _authFactorRepository;

        public LeagueOfLegendsAuthFactorValidatorAdapter(ILeagueOfLegendsService leagueOfLegendsService, IAuthFactorRepository authFactorRepository)
        {
            _leagueOfLegendsService = leagueOfLegendsService;
            _authFactorRepository = authFactorRepository;
        }

        public Game Game => Game.LeagueOfLegends;

        public async Task<ValidationResult> ValidateAuthFactorAsync(UserId userId, AuthFactor authFactor)
        {
            await _authFactorRepository.RemoveAuthFactorAsync(userId, Game);

            var summoner = await _leagueOfLegendsService.Summoner.GetSummonerByAccountIdAsync(Region.Na, authFactor.PlayerId);

            if (summoner.ProfileIconId != Convert.ToInt32(authFactor.Key))
            {
                return new ValidationFailure("_error", $"{Game} authentication process failed.").ToResult();
            }

            return new ValidationResult();
        }
    }
}
