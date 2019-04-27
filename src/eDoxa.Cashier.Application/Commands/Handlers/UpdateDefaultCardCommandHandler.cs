// Filename: UpdateDefaultCardCommandHandler.cs
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
    public sealed class UpdateDefaultCardCommandHandler : ICommandHandler<UpdateDefaultCardCommand, IActionResult>
    {
        private readonly CustomerService _service;
        private readonly IUserRepository _userRepository;

        public UpdateDefaultCardCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository;
            _service = service;
        }

        [ItemCanBeNull]
        public async Task<IActionResult> Handle([NotNull] UpdateDefaultCardCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsNoTrackingAsync(command.UserId);

            var options = new CustomerUpdateOptions
            {
                DefaultSource = command.CardId.ToString()
            };

            var customer = await _service.UpdateAsync(user.CustomerId.ToString(), options, cancellationToken: cancellationToken);

            return new OkObjectResult(customer);
        }
    }
}