// Filename: AddFundsCommandValidator.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Domain.Aggregate;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    internal sealed class AddFundsCommandValidator : CommandValidator<AddFundsCommand>
    {
        public AddFundsCommandValidator()
        {
            this.RuleFor(command => command.BundleType).Must(bundle => Enumeration.IsDefined(bundle.GetType(), bundle.Name));
        }
    }
}