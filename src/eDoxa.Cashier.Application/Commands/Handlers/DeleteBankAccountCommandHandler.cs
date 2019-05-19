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
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Security;
using eDoxa.Seedwork.Domain.Validations;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand, Either<ValidationError, CommandResult>>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly IStripeService _stripeService;

        public DeleteBankAccountCommandHandler(
            ICashierHttpContext cashierHttpContext,
            IStripeService stripeService,
            IIntegrationEventService integrationEventService
        )
        {
            _cashierHttpContext = cashierHttpContext;
            _stripeService = stripeService;
            _integrationEventService = integrationEventService;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, CommandResult>> Handle([NotNull] DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccountId = _cashierHttpContext.StripeBankAccountId;

            await _stripeService.DeleteBankAccountAsync(_cashierHttpContext.StripeAccountId, bankAccountId, cancellationToken);

            await this.PropagateClaimAsync(bankAccountId);

            return new CommandResult("The bank account has been removed.");
        }

        private async Task PropagateClaimAsync(StripeBankAccountId bankAccountId)
        {
            await _integrationEventService.PublishAsync(
                new UserClaimRemovedIntegrationEvent(
                    _cashierHttpContext.UserId.ToGuid(),
                    new Dictionary<string, string>
                    {
                        [CustomClaimTypes.StripeBankAccountId] = bankAccountId.ToString()
                    }
                )
            );
        }
    }
}
