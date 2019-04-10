// Filename: UpdateAddressCommandHandler.cs
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
    public sealed class UpdateAddressCommandHandler : ICommandHandler<UpdateAddressCommand, Address>
    {
        private readonly IUserRepository _userRepository;
        private readonly CustomerService _service;

        public UpdateAddressCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<Address> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
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

            return customer?.Shipping?.Address;
        }
    }
}