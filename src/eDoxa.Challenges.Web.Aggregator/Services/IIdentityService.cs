// Filename: IIdentityService.cs
// Date Created: 2019-11-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;

using IdentityResponses = eDoxa.Identity.Responses;

namespace eDoxa.Challenges.Web.Aggregator.Services
{
    public interface IIdentityService
    {
        [Get("/api/doxatags")]
        Task<IReadOnlyCollection<IdentityResponses.DoxatagResponse>> FetchDoxatagsAsync();
    }
}
