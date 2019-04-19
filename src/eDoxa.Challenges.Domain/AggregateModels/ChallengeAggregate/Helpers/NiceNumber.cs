// Filename: NiceNumber.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers
{
    public static class NiceNumber
    {
        // TODO: This need to be optimized for large number like 5000000 (very slow).
        public static List<int> PossibleNiceNumbers(int value)
        {
            var niceNumbers = new List<int>();

            for (var number = 1; number < Convert.ToInt64(value) + 1; number++)
            {
                if (IsNice(number))
                {
                    niceNumbers.Add(number);
                }
            }

            return niceNumbers;
        }

        public static bool IsNice(double number)
        {
            while (number > 1000)
            {
                number /= 10;
            }

            if (number >= 250)
            {
                return Math.Abs(number % 50) < 0.01;
            }

            if (number >= 100)
            {
                return Math.Abs(number % 25) < 0.01;
            }

            if (number >= 10)
            {
                return Math.Abs(number % 5) < 0.01;
            }

            if (number > 0)
            {
                return Math.Abs(number % 1) < 0.01;
            }

            return false;
        }

        public static int Round(double number, List<int> niceNumbers)
        {
            // TODO: Refactor to be defensive by design (replace exception with a non-null default).
            if (niceNumbers.Count <= 0)
            {
                throw new ArgumentException("The nice number list should be at least 1.");
            }

            // TODO: Refactor to be defensive by design (replace exception with a non-null default).
            if (number < niceNumbers.First())
            {
                throw new ArgumentException("The number should be greater than first nice number.");
            }

            if (number >= niceNumbers.Last())
            {
                return niceNumbers.Last();
            }

            var minIndex = 0;

            var maxIndex = niceNumbers.Count - 1;

            // TODO: Rename Optimization.cs to something more descriptive.
            var index = Optimization.FloorDiv(maxIndex + minIndex, 2);

            var currentValue = niceNumbers[index];

            var nextValue = niceNumbers[index + 1];

            while (currentValue > number || number >= nextValue)
            {
                if (currentValue < number)
                {
                    minIndex = index;
                }
                else if (currentValue > number)
                {
                    maxIndex = index;
                }

                // TODO: Rename Optimization.cs to something more descriptive.
                index = Optimization.FloorDiv(maxIndex + minIndex, 2);

                currentValue = niceNumbers[index];

                nextValue = niceNumbers[index + 1];
            }

            return currentValue;
        }
    }
}