// Filename: CreateBankAccountCommandHandler.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security;
using eDoxa.Security.Abstractions;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateBankAccountCommandHandler : ICommandHandler<CreateBankAccountCommand, IActionResult>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly IStripeService _stripeService;
        private readonly IUserInfoService _userInfoService;

        public CreateBankAccountCommandHandler(IUserInfoService userInfoService, IStripeService stripeService, IIntegrationEventService integrationEventService)
        {
            _userInfoService = userInfoService;
            _stripeService = stripeService;
            _integrationEventService = integrationEventService;
        }

        public async Task<IActionResult> Handle(CreateBankAccountCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var customerId = new CustomerId(_userInfoService.CustomerId);

            var bankAccountId = await _stripeService.CreateBankAccountAsync(customerId, command.SourceToken, cancellationToken);

            await _integrationEventService.PublishAsync(new UserClaimAddedIntegrationEvent(userId.ToGuid(), CustomClaimTypes.BankAccountId,
                bankAccountId.ToString()));

            return new OkResult();
        }
    }
}