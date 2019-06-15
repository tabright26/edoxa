// Filename: ILeagueOfLegendsMatchService.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Services.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.Services.LeagueOfLegends.Abstractions
{
    /// <summary>
    ///     API version 4
    /// </summary>
    public interface ILeagueOfLegendsMatchService
    {
        Task<LeagueOfLegendsMatchReferenceDto[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime);

        Task<LeagueOfLegendsMatchDto> GetMatchAsync(string gameId);
    }
}
