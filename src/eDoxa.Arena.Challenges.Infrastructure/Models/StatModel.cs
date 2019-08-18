﻿// Filename: StatModel.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class StatModel
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public float Weighting { get; set; }
    }
}
