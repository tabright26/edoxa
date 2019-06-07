// Filename: ILeagueOfLegendsMatchService.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Services.LeagueOfLegends.DTO;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Api.Abstractions
{
    /// <summary>
    ///     API version 4
    /// </summary>
    public interface ILeagueOfLegendsMatchService
    {
        Task<LeagueOfLegendsMatchReferenceDTO[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime);

        Task<LeagueOfLegendsMatchDTO> GetMatchAsync(string gameId);
    }
}
