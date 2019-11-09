// Filename: IChallengesService.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Requests;
using eDoxa.Challenges.Responses;

using Refit;

using ChallengeResponseFromChallengesService = eDoxa.Challenges.Responses.ChallengeResponse;
using RegisterChallengeParticipantRequestFromChallengesService = eDoxa.Challenges.Requests.RegisterChallengeParticipantRequest;

namespace eDoxa.Challenges.Aggregator.Services
{
    public interface IChallengesService
    {
        [Get("/api/challenges")]
        Task<IReadOnlyCollection<ChallengeResponseFromChallengesService>> FetchChallengesAsync();

        [Post("/api/challenges")]
        Task<ChallengeResponseFromChallengesService> CreateChallengeAsync([Body] CreateChallengeRequest request);

        [Get("/api/challenges/{challengeId}")]
        Task<ChallengeResponseFromChallengesService> FindChallengeAsync(
            [AliasAs("challengeId")]
            Guid challengeId
        );

        [Post("/api/challenges/{challengeId}")]
        Task<ChallengeResponseFromChallengesService> SynchronizeChallengeAsync(
            [AliasAs("challengeId")]
            Guid challengeId
        );

        [Post("/api/challenges/{challengeId}/participants")]
        Task<ParticipantResponse> RegisterChallengeParticipantAsync(
            [AliasAs("challengeId")]
            Guid challengeId,
            [Body] RegisterChallengeParticipantRequestFromChallengesService request
        );
    }
}
