// Filename: ClanGrpcService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Clans.Requests;
using eDoxa.Grpc.Protos.Clans.Responses;
using eDoxa.Grpc.Protos.Clans.Services;

using Grpc.Core;

namespace eDoxa.Clans.Api.Services
{
    public sealed class ClanGrpcService : ClanService.ClanServiceBase
    {
        public override async Task<FetchClansResponse> FetchClans(FetchClansRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new FetchClansResponse());
        }
    }
}
