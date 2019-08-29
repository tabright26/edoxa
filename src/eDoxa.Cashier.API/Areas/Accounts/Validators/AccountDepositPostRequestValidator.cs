// Filename: AccountDepositPostRequestValidator.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation;

namespace eDoxa.Cashier.Api.Areas.Accounts.Validators
{
    public sealed class AccountDepositPostRequestValidator : AbstractValidator<AccountDepositPostRequest>
    {
        public AccountDepositPostRequestValidator()
        {
            this.Enumeration(request => request.Currency)
                .DependentRules(
                    () =>
                    {
                        this.When(
                            request => request.Currency == Currency.Money,
                            () => this.RuleFor(request => request.Amount)
                                .Must(amount => Money.DepositAmounts().Any(money => money.Amount == amount))
                                .WithMessage(
                                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", Money.DepositAmounts().Select(money => money.Amount))}]."));

                        this.When(
                            request => request.Currency == Currency.Token,
                            () => this.RuleFor(request => request.Amount)
                                .Must(amount => Token.DepositAmounts().Any(token => token.Amount == amount))
                                .WithMessage(
                                    $"The amount of {nameof(Token)} is invalid. These are valid amounts: [{string.Join(", ", Token.DepositAmounts().Select(token => token.Amount))}]."));
                    });
        }
    }
}
