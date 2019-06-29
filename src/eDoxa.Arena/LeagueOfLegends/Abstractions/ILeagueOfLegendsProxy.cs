﻿// Filename: ILeagueOfLegendsProxy.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.LeagueOfLegends.Abstractions
{
    public interface ILeagueOfLegendsProxy
    {
        Task<LeagueOfLegendsMatchReferenceDto[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime);

        Task<LeagueOfLegendsMatchDto> GetMatchAsync(string gameId);
    }
}
