// Filename: UpdateDefaultCardCommandHandler.cs
// Date Created: 2019-04-30
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

using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Services;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class UpdateCardDefaultCommandHandler : ICommandHandler<UpdateCardDefaultCommand, IActionResult>
    {
        private readonly CustomerService _service;
        private readonly IUserInfoService _userInfoService;

        public UpdateCardDefaultCommandHandler(IUserInfoService userInfoService, CustomerService service)
        {
            _userInfoService = userInfoService;
            _service = service;
        }

        [ItemCanBeNull]
        public async Task<IActionResult> Handle([NotNull] UpdateCardDefaultCommand command, CancellationToken cancellationToken)
        {
            var customerId = _userInfoService.CustomerId.SingleOrDefault();

            if (customerId == null)
            {
                return new NotFoundObjectResult("Stripe CustomerId not found.");
            }

            var options = new CustomerUpdateOptions
            {
                DefaultSource = command.CardId.ToString()
            };

            var customer = await _service.UpdateAsync(customerId, options, cancellationToken: cancellationToken);

            return new OkObjectResult(customer);
        }
    }
}