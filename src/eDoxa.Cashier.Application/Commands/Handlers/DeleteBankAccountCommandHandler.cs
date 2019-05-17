// Filename: DeleteBankAccountCommandHandler.cs
// Date Created: 2019-05-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Security;
using eDoxa.ServiceBus;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand, Either>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly IStripeService _stripeService;
        private readonly ICashierSecurity _cashierSecurity;

        public DeleteBankAccountCommandHandler(ICashierSecurity cashierSecurity, IStripeService stripeService, IIntegrationEventService integrationEventService)
        {
            _cashierSecurity = cashierSecurity;
            _stripeService = stripeService;
            _integrationEventService = integrationEventService;
        }

        public async Task<Either> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            if (!_cashierSecurity.HasStripeBankAccount())
            {
                return new Failure("No bank account is associated with this account.");
            }

            var userId = _cashierSecurity.UserId;

            var accountId = _cashierSecurity.StripeAccountId;

            var bankAccountId = _cashierSecurity.StripeBankAccountId;

            await _stripeService.DeleteBankAccountAsync(accountId, bankAccountId, cancellationToken);

            await _integrationEventService.PublishAsync(new UserClaimAddedIntegrationEvent(userId.ToGuid(), CustomClaimTypes.StripeBankAccountId,
                bankAccountId.ToString()));

            return new Success("The bank account has been removed.");
        }
    }
}