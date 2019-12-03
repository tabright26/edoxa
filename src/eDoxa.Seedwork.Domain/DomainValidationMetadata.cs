// Filename: DomainValidationMetadata.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Seedwork.Domain
{
    public sealed class DomainValidationMetadata : Dictionary<string, object>
    {
        public const string Response = nameof(Response);

        public void AddResponse<TResponse>(TResponse value)
        where TResponse : class
        {
            this.Add(Response, value);
        }
    }
}
