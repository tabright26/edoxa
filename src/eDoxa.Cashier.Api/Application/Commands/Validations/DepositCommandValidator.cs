// Filename: DepositCommandValidator.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Seedwork.Application.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Domain.Extensions;

using FluentValidation;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Validations
{
    public sealed class DepositCommandValidator : CommandValidator<DepositCommand>
    {
        public DepositCommandValidator(IHttpContextAccessor httpContextAccessor, IAccountQuery accountQuery)
        {
            this.RuleFor(command => command.Currency)
                .NotNull()
                .DependentRules(
                    () =>
                    {
                        this.When(
                            command => Currency.FromName(command.Currency) == Currency.Money,
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
                            command => Currency.FromName(command.Currency) == Currency.Token,
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

                                    if (command.Currency == Currency.Money.Name)
                                    {
                                        var moneyAccount = new MoneyAccount(account);

                                        var errors = new DepositMoneyValidator().Validate(moneyAccount).Errors;

                                        if (errors.Any())
                                        {
                                            errors.ForEach(context.AddFailure);

                                            return;
                                        }
                                    }

                                    if (command.Currency == Currency.Token.Name)
                                    {
                                        var tokenAccount = new TokenAccount(account);

                                        var errors = new DepositTokenValidator().Validate(tokenAccount).Errors;

                                        if (errors.Any())
                                        {
                                            errors.ForEach(context.AddFailure);
                                        }
                                    }
                                }
                            );
                    }
                );
        }
    }
}
