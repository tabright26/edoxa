// Filename: UpdateDefaultCardCommandHandler.cs
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
using JetBrains.Annotations;
using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class UpdateDefaultCardCommandHandler : ICommandHandler<UpdateDefaultCardCommand, Customer>
    {
        private readonly IUserRepository _userRepository;
        private readonly CustomerService _service;

        public UpdateDefaultCardCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [ItemCanBeNull]
        public async Task<Customer> Handle([NotNull] UpdateDefaultCardCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsNoTrackingAsync(command.UserId);

            var options = new CustomerUpdateOptions
            {
                DefaultSource = command.CardId.ToString()
            };

            return await _service.UpdateAsync(user.CustomerId.ToString(), options, cancellationToken: cancellationToken);
        }
    }
}