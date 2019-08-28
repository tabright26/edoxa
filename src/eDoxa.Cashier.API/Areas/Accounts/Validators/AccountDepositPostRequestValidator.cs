// Filename: AccountDepositPostRequestValidator.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Validators;

using FluentValidation;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Areas.Accounts.Validators
{
    public sealed class AccountDepositPostRequestValidator : AbstractValidator<AccountDepositPostRequest>
    {
        public AccountDepositPostRequestValidator(IHttpContextAccessor httpContextAccessor, IAccountQuery accountQuery)
        {
            this.RuleFor(request => request.Currency)
                .NotNull()
                .DependentRules(
                    () =>
                    {
                        this.When(
                            request => Currency.FromName(request.Currency) == Currency.Money,
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

                                this.RuleFor(request => request.Amount)
                                    .Must(amount => amounts.Any(money => money.Amount == amount))
                                    .WithMessage(
                                        $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}]."
                                    );
                            }
                        );

                        this.When(
                            request => Currency.FromName(request.Currency) == Currency.Token,
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

                                this.RuleFor(request => request.Amount)
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
                        this.RuleFor(request => request)
                            .CustomAsync(
                                async (request, context, cancellationToken) =>
                                {
                                    var userId = httpContextAccessor.GetUserId();

                                    var account = await accountQuery.FindUserAccountAsync(userId);

                                    if (account == null)
                                    {
                                        context.AddFailure("User account not found.");

                                        return;
                                    }

                                    if (request.Currency == Currency.Money.Name)
                                    {
                                        var moneyAccount = new MoneyAccount(account);

                                        var errors = new DepositMoneyValidator().Validate(moneyAccount).Errors;

                                        if (errors.Any())
                                        {
                                            foreach (var error in errors)
                                            {
                                                context.AddFailure(error);
                                            }

                                            return;
                                        }
                                    }

                                    if (request.Currency == Currency.Token.Name)
                                    {
                                        var tokenAccount = new TokenAccount(account);

                                        var errors = new DepositTokenValidator().Validate(tokenAccount).Errors;

                                        if (errors.Any())
                                        {
                                            foreach (var error in errors)
                                            {
                                                context.AddFailure(error);
                                            }
                                        }
                                    }
                                }
                            );
                    }
                );
        }
    }
}
