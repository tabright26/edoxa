// Filename: UpdateDefaultCardCommandHandler.cs
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

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class UpdateCardDefaultCommandHandler : ICommandHandler<UpdateCardDefaultCommand, IActionResult>
    {
        private readonly CustomerService _service;
        private readonly IUserProfile _userProfile;

        public UpdateCardDefaultCommandHandler(IUserProfile userProfile, CustomerService service)
        {
            _userProfile = userProfile;
            _service = service;
        }

        [ItemCanBeNull]
        public async Task<IActionResult> Handle([NotNull] UpdateCardDefaultCommand command, CancellationToken cancellationToken)
        {
            var customerId = CustomerId.Parse(_userProfile.CustomerId);

            var options = new CustomerUpdateOptions
            {
                DefaultSource = command.CardId.ToString()
            };

            var customer = await _service.UpdateAsync(customerId.ToString(), options, cancellationToken: cancellationToken);

            return new OkObjectResult(customer);
        }
    }
}