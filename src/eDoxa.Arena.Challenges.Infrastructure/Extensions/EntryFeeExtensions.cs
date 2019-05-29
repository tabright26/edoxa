﻿// Filename: EntryFeeConverter.cs
// Date Created: 2019-05-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Infrastructure.Extensions
{
    internal static class EntryFeeExtensions
    {
        internal static EntryFee Deserialize(string entryFee)
        {
            var substrings = entryFee.Split(':');

            return new EntryFee(Convert.ToDecimal(substrings[0]), Enumeration.FromName<Currency>(substrings[1]));
        }

        internal static string Serialize(this EntryFee entryFee)
        {
            return $"{entryFee.Amount}:{entryFee.Currency}";
        }
    }
}