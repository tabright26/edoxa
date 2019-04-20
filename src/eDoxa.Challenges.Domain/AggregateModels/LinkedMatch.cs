// Filename: LinkedMatch.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public sealed class LinkedMatch : ValueObject
    {
        private string _ref;

        private LinkedMatch(string input)
        {
            Ref = input;
        }

        public string Ref
        {
            get
            {
                return _ref;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Ref));
                }

                if (!value.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
                {
                    throw new FormatException(nameof(Ref));
                }

                _ref = value;
            }
        }

        public static LinkedMatch Parse(string input)
        {
            return new LinkedMatch(input);
        }

        public static LinkedMatch FromLong(long input)
        {
            return Parse(input.ToString());
        }

        public static LinkedMatch FromGuid(Guid input)
        {
            return Parse(input.ToString());
        }

        public override string ToString()
        {
            return _ref;
        }
    }
}