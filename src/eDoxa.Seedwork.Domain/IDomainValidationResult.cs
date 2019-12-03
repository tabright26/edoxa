// Filename: IDomainValidationResult.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Seedwork.Domain
{
    public interface IDomainValidationResult
    {
        bool IsValid { get; }

        IReadOnlyCollection<DomainValidationError> Errors { get; }

        object GetMetadataResponse();
    }
}
