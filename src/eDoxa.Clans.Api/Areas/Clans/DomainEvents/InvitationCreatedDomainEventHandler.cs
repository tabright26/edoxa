// Filename: InvitationCreatedDomainEventHandler.cs
// Date Created: 2019-10-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Api.IntegrationEvents.Extensions;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Clans.Api.Areas.Clans.DomainEvents
{
    public sealed class InvitationCreatedDomainEventHandler : IDomainEventHandler<InvitationCreatedDomainEvent>
    {
        private readonly IClanService _clanService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public InvitationCreatedDomainEventHandler(IClanService clanService, IServiceBusPublisher serviceBusPublisher)
        {
            _clanService = clanService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(InvitationCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var clan = await _clanService.FindClanAsync(domainEvent.Invitation.ClanId);

            await this.PublishInvitationEmailSentIntegrationEventAsync(clan!, domainEvent.Invitation);
        }

        private async Task PublishInvitationEmailSentIntegrationEventAsync(Clan clan, Invitation invitation)
        {
            const string subject = "eDoxa - Clan invitation";

            var htmlMessage = $@"The clan '{clan.Name}' sent you an invitation to became a member.";

            await _serviceBusPublisher.PublishUserEmailSentIntegrationEventAsync(invitation.UserId, subject, htmlMessage);
        }
    }
}
