// Filename: DepositCommandValidator.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Validators;

using FluentValidation;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Validations
{
    public sealed class DepositCommandValidator : CommandValidator<DepositCommand>
    {
        public DepositCommandValidator(
            IHttpContextAccessor httpContextAccessor,
            IAccountRepository accountRepository,
            IUserRepository userRepository,
            IStripeService stripeService
        )
        {
            this.RuleFor(command => command.Currency)
                .NotNull()
                .SetValidator(new CurrencyDepositValidator())
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(command => command)
                            .CustomAsync(
                                async (command, context, cancellationToken) =>
                                {
                                    var userId = httpContextAccessor.GetUserId();

                                    var account = await accountRepository.GetAccountAsNoTrackingAsync(userId);

                                    if (command.Currency.Type == CurrencyType.Money)
                                    {
                                        var moneyAccount = new AccountMoney(account);

                                        var errors = new DepositMoneyValidator().Validate(moneyAccount).Errors;

                                        if (errors.Any())
                                        {
                                            errors.ForEach(context.AddFailure);

                                            return;
                                        }
                                    }

                                    if (command.Currency.Type == CurrencyType.Token)
                                    {
                                        var tokenAccount = new AccountToken(account);

                                        var errors = new DepositTokenValidator().Validate(tokenAccount).Errors;

                                        if (errors.Any())
                                        {
                                            errors.ForEach(context.AddFailure);

                                            return;
                                        }
                                    }

                                    var user = await userRepository.GetUserAsNoTrackingAsync(userId);

                                    var customer = await stripeService.GetCustomerAsync(user.GetCustomerId(), cancellationToken);

                                    new StripeCustomerValidator().Validate(customer).Errors.ForEach(context.AddFailure);
                                }
                            );
                    }
                );
        }

        private sealed class CurrencyDepositValidator : AbstractValidator<CurrencyViewModel>
        {
            public CurrencyDepositValidator()
            {
                this.Enumeration(currency => currency.Type)
                    .DependentRules(
                        () =>
                        {
                            this.When(
                                currency => currency.Type == CurrencyType.Money,
                                () =>
                                {
                                    var amounts = new[]
                                    {
                                        Money.Ten,
                                        Money.Twenty,
                                        Money.Fifty,
                                        Money.OneHundred,
                                        Money.FiveHundred
                                    };

                                    this.RuleFor(command => command.Amount)
                                        .Must(amount => amounts.Any(money => money.Amount == amount))
                                        .WithMessage(
                                            $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}]."
                                        );
                                }
                            );

                            this.When(
                                currency => currency.Type == CurrencyType.Token,
                                () =>
                                {
                                    var amounts = new[]
                                    {
                                        Token.FiftyThousand,
                                        Token.OneHundredThousand,
                                        Token.TwoHundredFiftyThousand,
                                        Token.FiveHundredThousand,
                                        Token.OneMillion,
                                        Token.FiveMillions
                                    };

                                    this.RuleFor(command => command.Amount)
                                        .Must(amount => amounts.Any(token => token.Amount == amount))
                                        .WithMessage(
                                            $"The amount of {nameof(Token)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}]."
                                        );
                                }
                            );
                        }
                    );
            }
        }
    }
}
