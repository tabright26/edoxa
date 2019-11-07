// Filename: ICashierService.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;

using ChallengeResponseFromCashierService = eDoxa.Cashier.Responses.ChallengeResponse;

#nullable disable

namespace eDoxa.Web.Aggregator.Services
{
    public interface ICashierService
    {
        [Get("/api/challenges")]
        Task<IReadOnlyCollection<ChallengeResponseFromCashierService>> FetchChallengesAsync();

        [Post("/api/challenges")]
        Task<ChallengeResponseFromCashierService> CreateChallengeAsync([Body] object body);

        [Get("/api/challenges/{challengeId}")]
        Task<ChallengeResponseFromCashierService> FindChallengeAsync(
            [AliasAs("challengeId")]
            Guid challengeId
        );

        [Delete("/api/challenges/{challengeId}")]
        Task<ChallengeResponseFromCashierService> DeleteChallengeAsync(
            [AliasAs("challengeId")]
            Guid challengeId
        );
    }
}
