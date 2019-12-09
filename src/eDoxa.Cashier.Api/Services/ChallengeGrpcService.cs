using System.Threading.Tasks;

using eDoxa.Cashier.Grpc.Protos;

using Grpc.Core;

namespace eDoxa.Cashier.Api.Services
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
    }
}
