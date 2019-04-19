// Filename: NiceNumber.cs
// Date Created: 2019-04-18
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
    public sealed class NiceNumber
    {
        public static List<int> NiceNum(double maxNum)
        {
            var list = new List<int>();

            for (var index = 1; index < Convert.ToInt64(maxNum) + 1; index++)
            {
                if (IsNiceNum(index))
                {
                    list.Add(index);
                }
            }

            return list;
        }

        public static bool IsNiceNum(double num)
        {
            while (num > 1000)
            {
                num /= 10;
            }

            if (num >= 250)
            {
                return Math.Abs(num % 50) < 0.01;
            }

            if (num >= 100)
            {
                return Math.Abs(num % 25) < 0.01;
            }

            if (num >= 10)
            {
                return Math.Abs(num % 5) < 0.01;
            }

            if (num > 0)
            {
                return Math.Abs(num % 1) < 0.01;
            }

            return false;
        }

        public static int RoundToNice(double numToRound, List<int> niceNumbers)
        {
            if (niceNumbers.Count == 0)
            {
                return -1;
            }

            if (numToRound >= niceNumbers.Last())
            {
                return niceNumbers.Last();
            }

            if (numToRound < niceNumbers[0])
            {
                return -1;
            }

            var minIndex = 0;

            var maxIndex = niceNumbers.Count - 1;

            var index = Convert.ToInt32(Math.Floor((maxIndex + minIndex) / 2D));

            var currentValue = niceNumbers[index];

            var nextValue = niceNumbers[index + 1];

            while (currentValue > numToRound || numToRound >= nextValue)
            {
                if (currentValue < numToRound)
                {
                    minIndex = index;
                }
                else if (currentValue > numToRound)
                {
                    maxIndex = index;
                }

                index = Convert.ToInt32(Math.Floor((maxIndex + minIndex) / 2D));

                currentValue = niceNumbers[index];

                nextValue = niceNumbers[index + 1];
            }

            return currentValue;
        }
    }
}