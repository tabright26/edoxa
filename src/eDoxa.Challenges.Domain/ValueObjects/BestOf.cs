// Filename: BestOf.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public class BestOf : ValueObject
    {
        internal const int MinBestOf = 1;
        internal const int MaxBestOf = 7;

        public static readonly BestOf Default = new BestOf(3);

        private readonly int _bestOf;

        public BestOf(int bestOf)
        {
            if (bestOf < MinBestOf ||
                bestOf > MaxBestOf)
            {
                throw new ArgumentException(nameof(bestOf));
            }

            _bestOf = bestOf;
        }

        public int ToInt32()
        {
            return _bestOf;
        }
    }
}