// Filename: ClanDeletedDomainEventHandler.cs
// Date Created: 2019-10-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.DomainEvents;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.DomainEvents
{
    public sealed class ClanDeletedDomainEventHandler : IDomainEventHandler<ClanDeletedDomainEvent>
    {
        private readonly IClanService _clanService;
        private readonly ICandidatureService _candidatureService;
        private readonly IInvitationService _invitationService;

        public ClanDeletedDomainEventHandler(IClanService clanService, ICandidatureService candidatureService, IInvitationService invitationService)
        {
            _clanService = clanService;
            _candidatureService = candidatureService;
            _invitationService = invitationService;
        }

        public async Task Handle(ClanDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _clanService.DeleteLogoAsync(domainEvent.ClanId);

            await _candidatureService.DeleteCandidaturesAsync(domainEvent.ClanId);

            await _invitationService.DeleteInvitationsAsync(domainEvent.ClanId);
        }
    }
}
