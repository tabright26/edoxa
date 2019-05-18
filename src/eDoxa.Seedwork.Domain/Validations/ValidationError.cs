// Filename: Failure.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.Domain.Validations
{
    public sealed class ValidationError
    {
        public static readonly ValidationError Empty = new ValidationError(string.Empty);

        private readonly string _message;

        public ValidationError(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return _message;
        }
    }
}