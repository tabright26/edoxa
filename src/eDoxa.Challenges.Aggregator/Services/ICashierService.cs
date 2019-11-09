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
using TransactionResponseFromCashierService = eDoxa.Cashier.Responses.TransactionResponse;
using CreateChallengeRequestFromCashierService = eDoxa.Cashier.Requests.CreateChallengeRequest;
using CreateTransactionRequestFromCashierService = eDoxa.Cashier.Requests.CreateTransactionRequest;

namespace eDoxa.Challenges.Aggregator.Services
{
    public interface ICashierService
    {
        [Get("/api/challenges")]
        Task<IReadOnlyCollection<ChallengeResponseFromCashierService>> FetchChallengesAsync();

        [Post("/api/challenges")]
        Task<ChallengeResponseFromCashierService> CreateChallengeAsync([Body] CreateChallengeRequestFromCashierService request);

        [Get("/api/challenges/{challengeId}")]
        Task<ChallengeResponseFromCashierService> FindChallengeAsync(
            [AliasAs("challengeId")]
            Guid challengeId
        );

        [Post("/api/transactions")]
        Task<TransactionResponseFromCashierService> CreateTransactionAsync([Body] CreateTransactionRequestFromCashierService request);
    }
}
