// Filename: Optimization.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers
{
    public static class Optimization
    {
        public static double Bisection(Func<double, double> optimize, double a, double b)
        {
            while (optimize(a) * optimize(b) > 0)
            {
                b += 1;
            }

            var c = (a + b) / 2.0;

            while (Math.Abs(optimize(c)) > 0.01)
            {
                if (optimize(a) * optimize(c) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }

                c = (a + b) / 2.0;
            }

            return c;
        }
    }
}