// Filename: DomainValidationError.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Domain
{
    public sealed class DomainValidationError
    {
        public DomainValidationError(string propertyName, string errorMessage) : this(errorMessage)
        {
            PropertyName = propertyName;
        }

        public DomainValidationError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            PropertyName = string.Empty;
        }

        public string PropertyName { get; }

        public string ErrorMessage { get; }
    }
}
