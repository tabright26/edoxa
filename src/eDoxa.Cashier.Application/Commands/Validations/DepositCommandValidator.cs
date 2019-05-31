// Filename: DepositCommandValidator.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    public sealed class DepositCommandValidator : CommandValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            this.RuleFor(command => command.Currency).NotNull().SetValidator(new CurrencyDepositValidator());
        }

        private sealed class CurrencyDepositValidator : AbstractValidator<CurrencyDTO>
        {
            public CurrencyDepositValidator()
            {
                this.Enumeration(command => command.Type)
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
