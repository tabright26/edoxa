// Filename: DeleteBankAccountCommandHandler.cs
// Date Created: 2019-05-19
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
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Cashier.Services.Stripe.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Security.Extensions;

using FluentValidation.Results;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand, Either<ValidationResult, CommandResult>>
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

        [ItemNotNull]
        public async Task<Either<ValidationResult, CommandResult>> Handle([NotNull] DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsync(userId);

            var validator = new RemoveBankAccountValidator();

            var result = validator.Validate(user);

            if (!result.IsValid)
            {
                return result;
            }

            await _stripeService.DeleteBankAccountAsync(user.AccountId, user.BankAccountId, cancellationToken);

            user.RemoveBankAccount();

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return new CommandResult("The bank account has been removed.");
        }
    }
}
