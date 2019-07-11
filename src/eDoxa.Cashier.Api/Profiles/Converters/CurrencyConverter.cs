// Filename: CurrencyConverter.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Api.Profiles.Converters
{
    internal sealed class CurrencyConverter : ITypeConverter<CurrencyViewModel, ICurrency>
    {
        [NotNull]
        public ICurrency Convert([NotNull] CurrencyViewModel source, [NotNull] ICurrency destination, [NotNull] ResolutionContext context)
        {
            if (source.Type == Currency.Money)
            {
                return new Money(source.Amount);
            }

            if (source.Type == Currency.Token)
            {
                return new Token(source.Amount);
            }

            throw new NotSupportedException();
        }
    }
}
