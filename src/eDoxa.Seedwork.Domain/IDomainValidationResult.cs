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

        TEntity GetEntityFromMetadata<TEntity>()
        where TEntity : class;

        void AddEntityToMetadata<TEntity>(TEntity entity)
        where TEntity : class;

        IDomainValidationResult AddInvalidArgumentError(string propertyName, string errorMessage);

        IDomainValidationResult AddFailedPreconditionError(string errorMessage);

        IDomainValidationResult AddDebugError(string errorMessage);

        string ToJsonErrors();
    }
}
