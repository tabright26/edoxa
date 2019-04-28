// Filename: UpdatePhoneCommandHandler.cs
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
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class UpdatePhoneCommandHandler : AsyncCommandHandler<UpdatePhoneCommand>
    {
        private readonly CustomerService _service;
        private readonly IUserRepository _userRepository;

        public UpdatePhoneCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository;
            _service = service;
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