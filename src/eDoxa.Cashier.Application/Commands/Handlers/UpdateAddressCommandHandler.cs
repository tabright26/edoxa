// Filename: UpdateAddressCommandHandler.cs
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
    public sealed class UpdateAddressCommandHandler : ICommandHandler<UpdateAddressCommand, IActionResult>
    {
        private readonly CustomerService _service;
        private readonly IUserRepository _userRepository;

        public UpdateAddressCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository;
            _service = service;
        }

        [ItemCanBeNull]
        public async Task<IActionResult> Handle([NotNull] UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsNoTrackingAsync(command.UserId);

            var options = new CustomerUpdateOptions
            {
                Shipping = new ShippingOptions
                {
                    Address = new AddressOptions
                    {
                        City = command.City,
                        Country = command.Country,
                        Line1 = command.Line1,
                        Line2 = command.Line2,
                        PostalCode = command.PostalCode,
                        State = command.State
                    },
                    Name = command.Name,
                    Phone = command.Phone
                }
            };

            var customer = await _service.UpdateAsync(user.CustomerId.ToString(), options, cancellationToken: cancellationToken);

            return new OkObjectResult(customer?.Shipping?.Address);
        }
    }
}