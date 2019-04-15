// Filename: CreateCardCommandHandler.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class CreateCardCommandHandler : ICommandHandler<CreateCardCommand, Card>
    {
        private readonly IUserRepository _userRepository;
        private readonly CustomerService _customerService;
        private readonly CardService _service;

        public CreateCardCommandHandler(IUserRepository userRepository, CustomerService customerService, CardService cardService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _service = cardService ?? throw new ArgumentNullException(nameof(cardService));
        }

        public async Task<Card> Handle(CreateCardCommand command, CancellationToken cancellationToken)
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

            return card;
        }
    }
}