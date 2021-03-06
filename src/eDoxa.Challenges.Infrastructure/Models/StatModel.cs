﻿// Filename: StatModel.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class StatModel
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public float Weighting { get; set; }
    }
}
