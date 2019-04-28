// Filename: IdempotencyException.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Commands.Exceptions
{
    public class IdempotencyException : Exception
    {
        public IdempotencyException([CanBeNull] string idempotencyKey) : base($"The HTTP request with IdempotencyKey '{idempotencyKey}' was already executed.")
        {
        }
    }
}