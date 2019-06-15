// Filename: UserQuery.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Application.Abstractions;
using eDoxa.Identity.Api.ViewModels;
using eDoxa.Identity.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Application.Queries
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
        public async Task<IReadOnlyCollection<UserViewModel>> FindUsersAsync()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();

            return _mapper.Map<IReadOnlyCollection<UserViewModel>>(users);
        }
    }
}
