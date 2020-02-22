﻿// Filename: SwaggerOptions.cs
// Date Created: 2020-02-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Seedwork.Application.Options
{
    public class SwaggerOptions
    {
        public ServiceOptions Service { get; set; }

        public AggregatorOptions Aggregator { get; set; }

        public ClientOptions Client { get; set; }
    }
}
