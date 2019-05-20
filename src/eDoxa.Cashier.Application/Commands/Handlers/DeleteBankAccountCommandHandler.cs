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
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand, Either<ValidationError, CommandResult>>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public DeleteBankAccountCommandHandler(ICashierHttpContext cashierHttpContext, IStripeService stripeService, IUserRepository userRepository)
        {
            _cashierHttpContext = cashierHttpContext;
            _stripeService = stripeService;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, CommandResult>> Handle([NotNull] DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindUserAsync(_cashierHttpContext.UserId);

            var result = user.CanRemoveBankAccount();

            if (result.Failure)
            {
                return result.ValidationError;
            }

            await _stripeService.DeleteBankAccountAsync(user.AccountId, user.BankAccountId, cancellationToken);

            user.RemoveBankAccount();

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return new CommandResult("The bank account has been removed.");
        }
    }
}
