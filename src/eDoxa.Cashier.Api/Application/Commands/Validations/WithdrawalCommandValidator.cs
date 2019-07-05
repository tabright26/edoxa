﻿// Filename: WithdrawCommandValidator.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Extensions;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Validations
{
    public sealed class WithdrawalCommandValidator : CommandValidator<WithdrawalCommand>
    {
        public WithdrawalCommandValidator(IHttpContextAccessor httpContextAccessor, IAccountQuery accountQuery)
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

                                    var account = await accountQuery.FindUserAccountAsync(userId);

                                    if (account == null)
                                    {
                                        context.AddFailure(new ValidationFailure(string.Empty, "User account not found.") { ErrorCode = StatusCodes.Status404NotFound.ToString() });
                                        
                                        return;
                                    }

                                    var accountMoney = new AccountMoney(account);

                                    new WithdrawalMoneyValidator(new Money(command.Amount)).Validate(accountMoney).Errors.ForEach(context.AddFailure);
                                }
                            );
                    }
                );
        }
    }
}