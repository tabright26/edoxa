// Filename: ChallengeService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Grpc.Protos;

using Grpc.Core;

namespace eDoxa.Challenges.Api.Services
{
    public sealed class ChallengeGrpcService : ChallengeService.ChallengeServiceBase
    {
        public override async Task<FetchChallengesResponse> FetchChallenges(FetchChallengesRequest request, ServerCallContext context)
        {
            return await base.FetchChallenges(request, context);
        }

        public override async Task<FindChallengeResponse> FindChallenge(FindChallengeRequest request, ServerCallContext context)
        {
            return await base.FindChallenge(request, context);
        }

        public override async Task<CreateChallengeResponse> CreateChallenge(CreateChallengeRequest request, ServerCallContext context)
        {
            return await base.CreateChallenge(request, context);
        }

        public override async Task<SynchronizeChallengeResponse> SynchronizeChallenge(SynchronizeChallengeRequest request, ServerCallContext context)
        {
            return await base.SynchronizeChallenge(request, context);
        }

        public override async Task<RegisterChallengeParticipantResponse> RegisterChallengeParticipant(
            RegisterChallengeParticipantRequest request,
            ServerCallContext context
        )
        {
            return await base.RegisterChallengeParticipant(request, context);
        }
    }
}
