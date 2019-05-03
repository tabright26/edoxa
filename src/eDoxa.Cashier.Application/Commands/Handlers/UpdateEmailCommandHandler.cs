﻿// Filename: UpdateEmailCommandHandler.cs
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
    internal sealed class UpdateEmailCommandHandler : AsyncCommandHandler<UpdateEmailCommand>
    {
        private readonly CustomerService _service;

        public UpdateEmailCommandHandler(CustomerService service)
        {
            _service = service;
        }

        protected override async Task Handle([NotNull] UpdateEmailCommand command, CancellationToken cancellationToken)
        {
            var options = new CustomerUpdateOptions
            {
                Email = command.Email
            };

            await _service.UpdateAsync(command.CustomerId.ToString(), options, cancellationToken: cancellationToken);
        }
    }
}