// Filename: ChallengeService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Grpc.Protos;

using Grpc.Core;

namespace eDoxa.Games.Api.Services
{
    public sealed class ChallengeGrpcService : ChallengeService.ChallengeServiceBase
    {
        public override async Task<FetchChallengeScoringResponse> FetchChallengeScoring(FetchChallengeScoringRequest request, ServerCallContext context)
        {
            return await base.FetchChallengeScoring(request, context);
        }

        public override async Task<FetchChallengeMatchesResponse> FetchChallengeMatches(FetchChallengeMatchesRequest request, ServerCallContext context)
        {
            return await base.FetchChallengeMatches(request, context);
        }
    }
}
