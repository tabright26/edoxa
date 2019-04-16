// Filename: IdempotentRequestException.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using JetBrains.Annotations;

namespace eDoxa.Seedwork.Application.Exceptions
{
    public class IdempotentRequestException : Exception
    {
        public IdempotentRequestException([CanBeNull] string idempotencyKey) : base($"The HTTP request with IdempotencyKey '{idempotencyKey}' was already executed.")
        {
        }
    }
}