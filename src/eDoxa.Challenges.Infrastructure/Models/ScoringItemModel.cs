// Filename: ScoringItemModel.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

namespace eDoxa.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class ScoringItemModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Weighting { get; set; }
    }
}
