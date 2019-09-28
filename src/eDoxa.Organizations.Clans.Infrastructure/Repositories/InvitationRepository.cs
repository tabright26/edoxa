// Filename: ClanRepository.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> ExistsAsync(UserId userId, ClanId clanId)
        {
            return await _dbContext.Invitations.AnyAsync(invitation => invitation.UserId == userId && invitation.ClanId == clanId);
        }

        public async Task DeleteAllWith(UserId userId)
        {
            var invitations = await _dbContext.Invitations.Where(invitation => invitation.UserId == userId).ToListAsync();
            invitations.Clear();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
