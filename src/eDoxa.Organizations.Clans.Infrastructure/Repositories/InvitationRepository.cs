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

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Organizations.Clans.Infrastructure.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ClansDbContext _dbContext;

        public InvitationRepository(ClansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Invitation invitation)
        {
            _dbContext.Invitations.Add(invitation);
        }

        public void Delete(Invitation invitation)
        {
            _dbContext.Invitations.Remove(invitation);
        }

        public async Task<IReadOnlyCollection<Invitation>> FetchAsync()
        {
            return await _dbContext.Invitations.ToListAsync();
        }

        public async Task<Invitation?> FindAsync(InvitationId invitationId)
        {
            return await _dbContext.Invitations.SingleOrDefaultAsync(invitation => invitation.Id == invitationId);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
