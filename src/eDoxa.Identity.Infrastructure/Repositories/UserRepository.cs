// Filename: UserRepository.cs
// Date Created: 2019-07-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Identity.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly IDictionary<Guid, User> _materializedIds = new Dictionary<Guid, User>();
        private readonly IDictionary<User, UserModel> _materializedObjects = new Dictionary<User, UserModel>();
        private readonly IdentityDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(IdentityDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(IEnumerable<User> users)
        {
            users.ForEach(this.Create);
        }

        public void Create(User user)
        {
            var userModel = _mapper.Map<UserModel>(user);

            _context.Users.Add(userModel);

            _materializedObjects[user] = userModel;
        }

        public async Task<User> FindUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
