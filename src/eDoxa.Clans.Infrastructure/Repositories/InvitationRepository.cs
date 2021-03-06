﻿// Filename: InvitationRepository.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Clans.Infrastructure.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ClansDbContext _context;

        public InvitationRepository(ClansDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Create(Invitation invitation)
        {
            _context.Invitations.Add(invitation);
        }

        public void Delete(Invitation invitation)
        {
            _context.Invitations.Remove(invitation);
        }

        public async Task<IReadOnlyCollection<Invitation>> FetchAsync(UserId userId)
        {
            return await _context.Invitations.AsExpandable().Where(invitation => invitation.UserId == userId).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Invitation>> FetchAsync(ClanId clanId)
        {
            return await _context.Invitations.AsExpandable().Where(invitation => invitation.ClanId == clanId).ToListAsync();
        }

        public async Task<Invitation?> FindAsync(InvitationId invitationId)
        {
            return await _context.Invitations.AsExpandable().SingleOrDefaultAsync(invitation => invitation.Id == invitationId);
        }

        public async Task<bool> ExistsAsync(UserId userId, ClanId clanId)
        {
            return await _context.Invitations.AsExpandable().AnyAsync(invitation => invitation.UserId == userId && invitation.ClanId == clanId);
        }
    }
}
