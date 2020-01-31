// Filename: ChallengeParticipantPayoutDomainEventHandler.cs
// Date Created: 2020-01-30
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Api.Application.DomainEvents.Handlers
{
    public sealed class ChallengeParticipantPayoutDomainEventHandler : IDomainEventHandler<ChallengeParticipantPayoutDomainEvent>
    {
        private readonly IAccountService _accountService;

        public ChallengeParticipantPayoutDomainEventHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(ChallengeParticipantPayoutDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var account = await _accountService.FindAccountAsync(domainEvent.UserId);

            await _accountService.CreateTransactionAsync(
                account,
                domainEvent.Currency,
                TransactionType.Payout,
                cancellationToken: cancellationToken);
        }
    }
}
