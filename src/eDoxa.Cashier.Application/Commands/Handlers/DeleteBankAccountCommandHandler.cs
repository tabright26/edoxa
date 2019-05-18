// Filename: DeleteBankAccountCommandHandler.cs
// Date Created: 2019-05-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Security;
using eDoxa.ServiceBus;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand, Either>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly IStripeService _stripeService;

        public DeleteBankAccountCommandHandler(
            ICashierHttpContext cashierHttpContext,
            IStripeService stripeService,
            IIntegrationEventService integrationEventService)
        {
            _cashierHttpContext = cashierHttpContext;
            _stripeService = stripeService;
            _integrationEventService = integrationEventService;
        }

        public async Task<Either> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var userId = _cashierHttpContext.UserId;

            var accountId = _cashierHttpContext.StripeAccountId;

            var bankAccountId = _cashierHttpContext.StripeBankAccountId;

            await _stripeService.DeleteBankAccountAsync(accountId, bankAccountId, cancellationToken);

            await this.PropagateClaimAsync(userId, bankAccountId);

            return new Success("The bank account has been removed.");
        }

        private async Task PropagateClaimAsync(UserId userId, StripeBankAccountId bankAccountId)
        {
            await _integrationEventService.PublishAsync(
                new UserClaimRemovedIntegrationEvent(
                    userId.ToGuid(),
                    new Dictionary<string, string>
                    {
                        [CustomClaimTypes.StripeBankAccountId] = bankAccountId.ToString()
                    }
                )
            );
        }
    }
}