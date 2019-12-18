// Filename: IDoxatagService.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Identity.Domain.Services
{
    public interface IDoxatagService
    {
        Task<IReadOnlyCollection<Doxatag>> FetchDoxatagHistoryAsync(User user);

        Task<IReadOnlyCollection<Doxatag>> FetchDoxatagsAsync();

        Task<Doxatag?> FindDoxatagAsync(User user);

        Task<IDomainValidationResult> ChangeDoxatagAsync(User user, string doxatagName);
    }
}
