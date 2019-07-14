// Filename: Role.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.RoleAggregate
{
    public sealed class Role : Entity<RoleId>
    {
        private readonly HashSet<Permission> _permissions = new HashSet<Permission>();

        public Role(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public IReadOnlyCollection<Permission> Permissions => _permissions;

        public static implicit operator string(Role role)
        {
            return role.Name;
        }

        public void AddClaim(Permission permission)
        {
            _permissions.Add(permission);
        }

        public void RemoveClaim(Permission permission)
        {
            _permissions.Remove(permission);
        }
    }
}
