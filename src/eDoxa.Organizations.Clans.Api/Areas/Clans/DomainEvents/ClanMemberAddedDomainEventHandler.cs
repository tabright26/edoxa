// Filename: ClanMemberAddedDomainEventHandler.cs
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
    public sealed class ClanMemberAddedDomainEventHandler : IDomainEventHandler<ClanMemberAddedDomainEvent>
    {
        private readonly IInvitationService _invitationService;
        private readonly ICandidatureService _candidatureService;

        public ClanMemberAddedDomainEventHandler(IInvitationService invitationService, ICandidatureService candidatureService)
        {
            _invitationService = invitationService;
            _candidatureService = candidatureService;
        }

        public async Task Handle(ClanMemberAddedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _invitationService.DeleteInvitationsAsync(domainEvent.UserId);

            await _candidatureService.DeleteCandidaturesAsync(domainEvent.UserId);
        }
    }
}
