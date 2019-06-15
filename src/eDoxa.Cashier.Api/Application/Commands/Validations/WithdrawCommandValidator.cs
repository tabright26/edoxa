// Filename: WithdrawCommandValidator.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Security.Extensions;

using FluentValidation;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Validations
{
    public sealed class WithdrawCommandValidator : CommandValidator<WithdrawCommand>
    {
        public WithdrawCommandValidator(IHttpContextAccessor httpContextAccessor, IAccountRepository accountRepository)
        {
            var amounts = new[] {Money.Fifty, Money.OneHundred, Money.TwoHundred};

            this.RuleFor(command => command.Amount)
                .Must(amount => amounts.Any(money => money.Amount == amount))
                .WithMessage(
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}]."
                )
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(command => command)
                            .CustomAsync(
                                async (command, context, cancellationToken) =>
                                {
                                    var userId = httpContextAccessor.GetUserId();

                                    var account = await accountRepository.GetAccountAsNoTrackingAsync(userId);

                                    var accountMoney = new AccountMoney(account);

                                    new WithdrawMoneyValidator(new Money(command.Amount)).Validate(accountMoney).Errors.ForEach(context.AddFailure);
                                }
                            );
                    }
                );
        }
    }
}
