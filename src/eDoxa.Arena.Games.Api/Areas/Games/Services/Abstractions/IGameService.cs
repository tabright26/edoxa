// Filename: IGameService.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Api.Areas.Games.Services.Abstractions
{
    public interface IGameService
    {
        Task<IReadOnlyCollection<Game>> FetchGamesWithCredentialAsync(UserId userId);
    }
}
