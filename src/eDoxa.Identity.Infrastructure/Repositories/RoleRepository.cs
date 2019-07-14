// Filename: RoleRepository.cs
// Date Created: 2019-07-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Identity.Infrastructure.Repositories
{
    public sealed class RoleRepository : IRoleRepository
    {
        private readonly IDictionary<Guid, Role> _materializedIds = new Dictionary<Guid, Role>();
        private readonly IDictionary<Role, RoleModel> _materializedObjects = new Dictionary<Role, RoleModel>();
        private readonly IdentityDbContext _context;
        private readonly IMapper _mapper;

        public RoleRepository(IdentityDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Create(IEnumerable<Role> roles)
        {
            roles.ForEach(this.Create);
        }

        public void Create(Role role)
        {
            var roleModel = _mapper.Map<RoleModel>(role);

            _context.Roles.Add(roleModel);

            _materializedObjects[role] = roleModel;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
