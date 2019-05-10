// Filename: CreateCardCommandHandler.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateCardCommandHandler : ICommandHandler<CreateCardCommand, IActionResult>
    {
        private readonly CustomerService _customerService;
        private readonly CardService _service;
        private readonly IUserInfoService _userInfoService;

        public CreateCardCommandHandler(IUserInfoService userInfoService, CustomerService customerService, CardService cardService)
        {
            _userInfoService = userInfoService;
            _customerService = customerService;
            _service = cardService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] CreateCardCommand command, CancellationToken cancellationToken)
        {
            var customerId = CustomerId.Parse(_userInfoService.CustomerId);

            var card = await _service.CreateAsync(
                customerId.ToString(),
                new CardCreateOptions
                {
                    SourceToken = command.SourceToken
                },
                cancellationToken: cancellationToken
            );

            if (command.DefaultCard)
            {
                await _customerService.UpdateAsync(
                    customerId.ToString(),
                    new CustomerUpdateOptions
                    {
                        DefaultSource = card.Id
                    },
                    cancellationToken: cancellationToken
                );
            }

            return new OkObjectResult(card);
        }
    }
}