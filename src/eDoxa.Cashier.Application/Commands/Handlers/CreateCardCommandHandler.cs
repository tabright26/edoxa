// Filename: CreateCardCommandHandler.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class CreateCardCommandHandler : ICommandHandler<CreateCardCommand, IActionResult>
    {
        private readonly CustomerService _customerService;
        private readonly CardService _service;
        private readonly IUserRepository _userRepository;

        public CreateCardCommandHandler(IUserRepository userRepository, CustomerService customerService, CardService cardService)
        {
            _userRepository = userRepository;
            _customerService = customerService;
            _service = cardService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] CreateCardCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsNoTrackingAsync(command.UserId);

            var customerId = user.CustomerId.ToString();

            var card = await _service.CreateAsync(
                customerId,
                new CardCreateOptions
                {
                    SourceToken = command.SourceToken
                },
                cancellationToken: cancellationToken
            );

            if (command.DefaultCard)
            {
                await _customerService.UpdateAsync(
                    customerId,
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