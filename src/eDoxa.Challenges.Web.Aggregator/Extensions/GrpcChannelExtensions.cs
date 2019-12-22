// Filename: GrpcChannelExtensions.cs
// Date Created: 2019-12-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Grpc.Protos.Identity.Services;

using Grpc.Net.Client;

namespace eDoxa.Challenges.Web.Aggregator.Extensions
{
    public static class GrpcChannelExtensions
    {
        public static CashierService.CashierServiceClient CreateCashierServiceClient(this GrpcChannel channel)
        {
            return new CashierService.CashierServiceClient(channel);
        }

        public static IdentityService.IdentityServiceClient CreateIdentityServiceClient(this GrpcChannel channel)
        {
            return new IdentityService.IdentityServiceClient(channel);
        }

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
