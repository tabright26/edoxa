// Filename: AccountDepositPostRequestValidator.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Domain.AggregateModels;

using FluentValidation;

namespace eDoxa.Cashier.Api.Areas.Accounts.Validators
{
    public sealed class AccountDepositPostRequestValidator : AbstractValidator<AccountDepositPostRequest>
    {
        public AccountDepositPostRequestValidator()
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
                                        $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}].");
                            });

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
                                        $"The amount of {nameof(Token)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}].");
                            });
                    });
        }
    }
}
