// Filename: UserQueries.cs
// Date Created: 2019-04-01
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Functional;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.DTO;
using eDoxa.Identity.DTO.Queries;
using eDoxa.Identity.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Application.Queries
{
    public sealed partial class UserQueries
    {
        private readonly IdentityDbContext _context;
        private readonly IMapper _mapper;

        public UserQueries(IdentityDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<User>> FindUsersAsNoTrackingAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }
    }

    public sealed partial class UserQueries : IUserQueries
    {
        public async Task<Option<UserListDTO>> FindUsersAsync()
        {
            var users = await this.FindUsersAsNoTrackingAsync();

            var list = _mapper.Map<UserListDTO>(users);

            return list.Any() ? new Option<UserListDTO>(list) : new Option<UserListDTO>();
        }
    }
}