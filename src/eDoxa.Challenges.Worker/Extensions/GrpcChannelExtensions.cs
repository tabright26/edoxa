// Filename: TestHelper.cs
// Date Created: 2019-12-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Services;

using Grpc.Net.Client;

namespace eDoxa.Challenges.Worker.Extensions
{
    public static class GrpcChannelExtensions
    {
        public static ChallengeService.ChallengeServiceClient CreateChallengeServiceClient(this GrpcChannel channel)
        {
            return new ChallengeService.ChallengeServiceClient(channel);
        }

        public static GameService.GameServiceClient CreateGameServiceClient(this GrpcChannel channel)
        {
            return new GameService.GameServiceClient(channel);
        }
    }
}
