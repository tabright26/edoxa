﻿// Filename: UserQueries.cs
// Date Created: 2019-04-01
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

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
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private async Task<IEnumerable<User>> FindUsersAsNoTrackingAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
    }

    public sealed partial class UserQueries : IUserQueries
    {
        public async Task<UserListDTO> FindUsersAsync()
        {
            var users = await this.FindUsersAsNoTrackingAsync();

            return _mapper.Map<UserListDTO>(users);
        }
    }
}