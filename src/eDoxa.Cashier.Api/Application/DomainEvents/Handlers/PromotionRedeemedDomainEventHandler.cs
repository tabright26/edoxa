// Filename: PromotionRedeemedDomainEventHandler.cs
// Date Created: 2020-01-21
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Api.Application.DomainEvents.Handlers
{
    public sealed class PromotionRedeemedDomainEventHandler : IDomainEventHandler<PromotionRedeemedDomainEvent>
    {
        private readonly IAccountService _accountService;

        public PromotionRedeemedDomainEventHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Handle(PromotionRedeemedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var account = await _accountService.FindAccountAsync(domainEvent.UserId);

            await _accountService.CreateTransactionAsync(
                account,
                domainEvent.Amount,
                domainEvent.Currency,
                TransactionType.Promotion,
                new TransactionMetadata
                {
                    [nameof(PromotionId)] = domainEvent.PromotionId
                },
                cancellationToken);
        }
    }
}
