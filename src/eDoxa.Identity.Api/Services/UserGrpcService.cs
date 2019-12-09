// Filename: UserGrpcService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Grpc.Protos;

using Grpc.Core;

namespace eDoxa.Identity.Api.Services
{
    public sealed class UserGrpcService : UserService.UserServiceBase
    {
        public override async Task<AddUserClaimResponse> AddUserClaim(AddUserClaimRequest request, ServerCallContext context)
        {
            return await base.AddUserClaim(request, context);
        }

        public override async Task<RemoveUserClaimResponse> RemoveUserClaim(RemoveUserClaimRequest request, ServerCallContext context)
        {
            return await base.RemoveUserClaim(request, context);
        }

        public override async Task<ReplaceUserClaimResponse> ReplaceUserClaim(ReplaceUserClaimRequest request, ServerCallContext context)
        {
            return await base.ReplaceUserClaim(request, context);
        }

        public override async Task<AddUserToRoleResponse> AddUserToRole(AddUserToRoleRequest request, ServerCallContext context)
        {
            return await base.AddUserToRole(request, context);
        }

        public override async Task<RemoveUserFromRoleResponse> RemoveUserFromRole(RemoveUserFromRoleRequest request, ServerCallContext context)
        {
            return await base.RemoveUserFromRole(request, context);
        }
    }
}
