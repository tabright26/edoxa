// Filename: CreateBankAccountCommandHandler.cs
// Date Created: 2019-05-29
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
using eDoxa.Cashier.Services.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Application.Commands.Abstractions.Handlers;
using eDoxa.Stripe.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class CreateBankAccountCommandHandler : AsyncCommandHandler<CreateBankAccountCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public CreateBankAccountCommandHandler(IHttpContextAccessor httpContextAccessor, IStripeService stripeService, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _stripeService = stripeService;
            _userRepository = userRepository;
        }

        protected override async Task Handle([NotNull] CreateBankAccountCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsync(userId);

            var bankAccountId = await _stripeService.CreateBankAccountAsync(user.GetConnectAccountId(), command.ExternalAccountTokenId, cancellationToken);

            user.AddBankAccount(bankAccountId.ToString());

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
