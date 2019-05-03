// Filename: UpdatePhoneCommandHandler.cs
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

using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class UpdatePhoneCommandHandler : AsyncCommandHandler<UpdatePhoneCommand>
    {
        private readonly CustomerService _service;

        public UpdatePhoneCommandHandler(CustomerService service)
        {
            _service = service;
        }

        protected override async Task Handle([NotNull] UpdatePhoneCommand command, CancellationToken cancellationToken)
        {
            var customer = await _service.GetAsync(command.CustomerId.ToString(), cancellationToken: cancellationToken);

            if (customer.Shipping != null)
            {
                var options = new CustomerUpdateOptions
                {
                    Shipping = new ShippingOptions
                    {
                        Phone = command.Phone
                    }
                };

                await _service.UpdateAsync(customer.Id, options, cancellationToken: cancellationToken);
            }
        }
    }
}