// Filename: IDoxatagRepository.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Identity.Domain.Repositories
{
    public interface IDoxatagRepository : IRepository<Doxatag>
    {
        void Create(Doxatag doxatag);

        Task<IReadOnlyCollection<Doxatag>> FetchDoxatagHistoryAsync(UserId userId);

        Task<IReadOnlyCollection<Doxatag>> FetchDoxatagsAsync();

        Task<IImmutableSet<int>> FetchDoxatagCodesByNameAsync(string name);

        Task<Doxatag?> FindDoxatagAsync(UserId userId);
    }
}
