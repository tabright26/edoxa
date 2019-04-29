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

namespace eDoxa.Seedwork.Domain.Common
{
    public static class MathUtils
    {
        public static int FloorDiv(double dividend, double divisor)
        {
            return Convert.ToInt32(Math.Floor(dividend / divisor));
        }

        public static int RoundMultiplier(decimal value, int multiplier)
        {
            return (int)Math.Round(value / multiplier);
        }
        
        public static double Bisection(Func<double, double> func, double a, double b)
        {
            while (func(a) * func(b) > 0)
            {
                b += 1;
            }

            var c = (a + b) / 2.0;

            while (Math.Abs(func(c)) > 0.01)
            {
                if (func(a) * func(c) < 0)
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