// Filename: IClanRepository.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;

namespace eDoxa.Organizations.Clans.Domain.Repositories
{
    public interface IClanRepository
    {
        void Create(ClanModel clanModel);

        Task<IReadOnlyCollection<ClanModel>> FetchClansAsync();

        Task<ClanModel> FindClanAsync(Guid clanId);

        Task SaveChangesAsync();
    }
}
