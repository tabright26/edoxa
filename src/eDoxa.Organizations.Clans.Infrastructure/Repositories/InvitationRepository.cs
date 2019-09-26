// Filename: ClanRepository.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;

namespace eDoxa.Organizations.Clans.Infrastructure.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ClansDbContext _dbContext;

        public InvitationRepository(ClansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Invitation candidature)
        {
            throw new NotImplementedException();
        }

        public void Delete(Invitation candidature)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Invitation>> FetchAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Invitation?> FindAsync(InvitationId invitationId)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
