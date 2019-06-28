﻿// Filename: DeleteBankAccountCommandHandler.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Extensions;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Stripe.Abstractions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class DeleteBankAccountCommandHandler : AsyncCommandHandler<DeleteBankAccountCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public DeleteBankAccountCommandHandler(IHttpContextAccessor httpContextAccessor, IStripeService stripeService, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _stripeService = stripeService;
            _userRepository = userRepository;
        }

        protected override async Task Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsync(userId);

            await _stripeService.DeleteBankAccountAsync(user.GetConnectAccountId(), user.GetBankAccountId(), cancellationToken);

            user.RemoveBankAccount();

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
