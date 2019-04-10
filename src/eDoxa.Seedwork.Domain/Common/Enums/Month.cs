// Filename: Month.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel.DataAnnotations;

namespace eDoxa.Seedwork.Domain.Common.Enums
{
    public enum Month
    {
        [Display(Name = "January")]
        January = 1,

        [Display(Name = "February")]
        February = 2,

        [Display(Name = "March")]
        March = 3,

        [Display(Name = "April")]
        April = 4,

        [Display(Name = "May")]
        May = 5,

        [Display(Name = "June")]
        June = 6,

        [Display(Name = "July")]
        July = 7,

        [Display(Name = "August")]
        August = 8,

        [Display(Name = "September")]
        September = 9,

        [Display(Name = "October")]
        October = 10,

        [Display(Name = "November")]
        November = 11,

        [Display(Name = "December")]
        December = 12
    }
}