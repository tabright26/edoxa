// Filename: UpdatePhoneCommandHandler.cs
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
    public sealed class UpdatePhoneCommandHandler : AsyncCommandHandler<UpdatePhoneCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly CustomerService _service;

        public UpdatePhoneCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        protected override async Task Handle([NotNull] UpdatePhoneCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsNoTrackingAsync(command.UserId);

            var customer = await _service.GetAsync(user.CustomerId.ToString(), cancellationToken: cancellationToken);

            if (customer.Shipping != null)
            {
                var options = new CustomerUpdateOptions
                {
                    Shipping = new ShippingOptions
                    {
                        Phone = command.Phone
                    }
                };

                await _service.UpdateAsync(user.CustomerId.ToString(), options, cancellationToken: cancellationToken);
            }
        }
    }
}