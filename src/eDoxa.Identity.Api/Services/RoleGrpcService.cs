// Filename: RoleGrpcService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Grpc.Protos;

using Grpc.Core;

namespace eDoxa.Identity.Api.Services
{
    public sealed class RoleGrpcService : RoleService.RoleServiceBase
    {
        public override async Task<CreateRoleResponse> CreateRole(CreateRoleRequest request, ServerCallContext context)
        {
            return await base.CreateRole(request, context);
        }

        public override async Task<DeleteRoleResponse> DeleteRole(DeleteRoleRequest request, ServerCallContext context)
        {
            return await base.DeleteRole(request, context);
        }

        public override async Task<AddRoleClaimResponse> AddRoleClaim(AddRoleClaimRequest request, ServerCallContext context)
        {
            return await base.AddRoleClaim(request, context);
        }

        public override async Task<RemoveRoleClaimResponse> RemoveRoleClaim(RemoveRoleClaimRequest request, ServerCallContext context)
        {
            return await base.RemoveRoleClaim(request, context);
        }
    }
}
