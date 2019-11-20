// Filename: ICashierService.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;

using CashierResponses = eDoxa.Cashier.Responses;
using CashierRequests = eDoxa.Cashier.Requests;

namespace eDoxa.Challenges.Web.Aggregator.Services
{
    public interface ICashierService
    {
        [Get("/api/challenges")]
        Task<IReadOnlyCollection<CashierResponses.ChallengeResponse>> FetchChallengesAsync();

        [Post("/api/challenges")]
        Task<CashierResponses.ChallengeResponse> CreateChallengeAsync([Body] CashierRequests.CreateChallengeRequest request);

        [Get("/api/challenges/{challengeId}")]
        Task<CashierResponses.ChallengeResponse> FindChallengeAsync(
            [AliasAs("challengeId")]
            Guid challengeId
        );

        [Post("/api/transactions")]
        Task<CashierResponses.TransactionResponse> CreateTransactionAsync([Body] CashierRequests.CreateTransactionRequest request);
    }
}
