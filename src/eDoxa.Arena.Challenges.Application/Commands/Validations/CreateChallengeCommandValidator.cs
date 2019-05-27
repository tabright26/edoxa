// Filename: CreateChallengeCommandValidator.cs
// Date Created: 2019-05-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;

using FluentValidation;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Application.Commands.Validations
{
    public sealed class CreateChallengeCommandValidator : AbstractValidator<CreateChallengeCommand>
    {
        public CreateChallengeCommandValidator(IHostingEnvironment environment)
        {
            this.RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage($"The {nameof(CreateChallengeCommand.Name)} property is required.");

            this.RuleForEnumeration(command => command.Game);

            this.RuleFor(command => command.Duration)
                .Must(ChallengeDuration.HasValue)
                .WithMessage(
                    $"The {nameof(CreateChallengeCommand.Duration)} property is invalid. These are valid input values: {ChallengeDuration.DisplayNames()}."
                );

            this.RuleFor(command => command.BestOf)
                .Must(BestOf.HasValue)
                .WithMessage($"The {nameof(CreateChallengeCommand.BestOf)} property is invalid. These are valid input values: {BestOf.DisplayNames()}.");

            this.RuleFor(command => command.PayoutEntries)
                .Must(PayoutEntries.HasValue)
                .WithMessage(
                    $"The {nameof(CreateChallengeCommand.PayoutEntries)} property is invalid. These are valid input values: {PayoutEntries.DisplayNames()}."
                );

            this.RuleForEnumeration(command => command.EntryFee.Currency);

            this.RuleFor(command => command.EntryFee)
                .Must(entryFee => EntryFeeHasValue(entryFee.Amount, entryFee.Currency))
                .WithMessage(
                    command =>
                        $"The {nameof(CreateChallengeCommand.EntryFee)} property is invalid. These are valid input values: {EntryFeeDisplayNames(command.EntryFee.Currency)}."
                );

            this.RuleFor(command => command.IsFake)
                .Must(isFake => !environment.IsProduction() || !isFake)
                .WithMessage($"The {nameof(CreateChallengeCommand.IsFake)} property must be false in the production environment.");
        }

        public static bool EntryFeeHasValue(decimal amount, Currency currency)
        {
            return currency.Equals(Currency.Money) ? MoneyEntryFee.HasValue<MoneyEntryFee>(amount) : currency.Equals(Currency.Token) && TokenEntryFee.HasValue<TokenEntryFee>(amount);
        }

        public static string EntryFeeDisplayNames(Currency currency)
        {
            return currency.Equals(Currency.Money) ? MoneyEntryFee.DisplayNames<MoneyEntryFee>() :
                currency.Equals(Currency.Token) ? TokenEntryFee.DisplayNames<TokenEntryFee>() : $"no valid inputs for {currency}";
        }
    }
}
