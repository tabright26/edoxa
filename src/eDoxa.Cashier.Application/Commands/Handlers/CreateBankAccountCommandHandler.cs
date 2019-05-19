// Filename: CreateBankAccountCommandHandler.cs
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
    internal sealed class CreateBankAccountCommandHandler : ICommandHandler<CreateBankAccountCommand, Either<ValidationError, CommandResult>>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly IStripeService _stripeService;

        public CreateBankAccountCommandHandler(
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
        public async Task<Either<ValidationError, CommandResult>> Handle([NotNull] CreateBankAccountCommand command, CancellationToken cancellationToken)
        {
            var bankAccountId = await _stripeService.CreateBankAccountAsync(
                _cashierHttpContext.StripeAccountId,
                command.ExternalAccountTokenId,
                cancellationToken
            );

            await this.PropagateClaimAsync(bankAccountId);

            return new CommandResult("The bank account has been added.");
        }

        private async Task PropagateClaimAsync(StripeBankAccountId bankAccountId)
        {
            await _integrationEventService.PublishAsync(
                new UserClaimAddedIntegrationEvent(
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
