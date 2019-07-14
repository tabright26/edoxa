// Filename: Permission.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.RoleAggregate
{
    public sealed class Permission : Entity<PermissionId>
    {
        private readonly string _permission;

        public Permission(string permission)
        {
            _permission = permission;
        }

        public static implicit operator string(Permission permission)
        {
            return permission._permission;
        }
    }
}
