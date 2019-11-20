// Filename: IChallengesService.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Refit;

using ChallengeResponses = eDoxa.Challenges.Responses;
using ChallengeRequests = eDoxa.Challenges.Requests;

namespace eDoxa.Challenges.Web.Aggregator.Services
{
    public interface IChallengesService
    {
        [Get("/api/challenges")]
        Task<IReadOnlyCollection<ChallengeResponses.ChallengeResponse>> FetchChallengesAsync();

        [Post("/api/challenges")]
        Task<ChallengeResponses.ChallengeResponse> CreateChallengeAsync([Body] ChallengeRequests.CreateChallengeRequest request);

        [Get("/api/challenges/{challengeId}")]
        Task<ChallengeResponses.ChallengeResponse> FindChallengeAsync(
            [AliasAs("challengeId")]
            Guid challengeId
        );

        [Post("/api/challenges/{challengeId}")]
        Task<ChallengeResponses.ChallengeResponse> SynchronizeChallengeAsync(
            [AliasAs("challengeId")]
            Guid challengeId
        );

        [Post("/api/challenges/{challengeId}/participants")]
        Task<ChallengeResponses.ParticipantResponse> RegisterChallengeParticipantAsync(
            [AliasAs("challengeId")]
            Guid challengeId,
            [Body] ChallengeRequests.RegisterChallengeParticipantRequest request
        );
    }
}
