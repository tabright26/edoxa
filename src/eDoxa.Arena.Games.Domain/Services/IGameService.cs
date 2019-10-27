﻿// Filename: IGameService.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Domain.Services
{
    public interface IGameService
    {
        IImmutableSet<GameInfo> FetchGameInfos();

        Task<IImmutableSet<GameInfo>> FetchGameInfosAsync(UserId userId);
    }
}
