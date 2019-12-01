// Filename: DomainValidationResultExtensions.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class DomainValidationResultExtensions
    {
        public static DomainValidationResult AddDomainValidationError(this DomainValidationResult result, string propertyName, string errorMessage)
        {
            result.AddDomainValidationError(propertyName, errorMessage);

            return result;
        }

        public static DomainValidationResult AddDomainValidationError(this DomainValidationResult result, string errorMessage)
        {
            result.AddDomainValidationError(errorMessage);

            return result;
        }
    }
}
