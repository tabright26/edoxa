// Filename: IIdentityService.cs
// Date Created: 2019-11-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;

using UserDoxatagResponseFromIdentityService = eDoxa.Identity.Responses.UserDoxatagResponse;

namespace eDoxa.Challenges.Aggregator.Services
{
    public interface IIdentityService
    {
        [Get("/api/doxatags")]
        Task<IReadOnlyCollection<UserDoxatagResponseFromIdentityService>> FetchDoxatagsAsync();
    }
}
