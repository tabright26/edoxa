// Filename: UpdateAddressCommandHandler.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Services;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class UpdateAddressCommandHandler : ICommandHandler<UpdateAddressCommand, IActionResult>
    {
        private readonly CustomerService _service;
        private readonly IUserInfoService _userInfoService;
        private readonly IUserRepository _userRepository;

        public UpdateAddressCommandHandler(IUserInfoService userInfoService, IUserRepository userRepository, CustomerService service)
        {
            _userInfoService = userInfoService;
            _userRepository = userRepository;
            _service = service;
        }

        [ItemCanBeNull]
        public async Task<IActionResult> Handle([NotNull] UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            var userId = _userInfoService.Subject.Select(UserId.FromGuid).SingleOrDefault();

            var user = await _userRepository.FindAsNoTrackingAsync(userId);

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