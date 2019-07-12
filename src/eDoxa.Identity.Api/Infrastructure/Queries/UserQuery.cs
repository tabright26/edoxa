// Filename: UserQuery.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Domain.Queries;
using eDoxa.Identity.Domain.ViewModels;
using eDoxa.Identity.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Infrastructure.Queries
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
