// Filename: UpdateEmailCommandHandler.cs
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

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class UpdateEmailCommandHandler : AsyncCommandHandler<UpdateEmailCommand>
    {
        private readonly CustomerService _service;
        private readonly IUserRepository _userRepository;

        public UpdateEmailCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository;
            _service = service;
        }

        protected override async Task Handle([NotNull] UpdateEmailCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsNoTrackingAsync(command.UserId);

            var options = new CustomerUpdateOptions
            {
                Email = command.Email
            };

            await _service.UpdateAsync(user.CustomerId.ToString(), options, cancellationToken: cancellationToken);
        }
    }
}