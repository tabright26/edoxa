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
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.DTO;
using eDoxa.Identity.DTO.Queries;
using eDoxa.Identity.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Application.Queries
{
    public sealed partial class UserQuery
    {
        private readonly IdentityDbContext _context;
        private readonly IMapper _mapper;

        public UserQuery(IdentityDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }

    public sealed partial class UserQuery : IUserQuery
    {
        public async Task<IReadOnlyCollection<UserDTO>> FindUsersAsync()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();

            return _mapper.Map<IReadOnlyCollection<UserDTO>>(users);
        }
    }
}