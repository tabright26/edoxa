// Filename: SubscriptionException.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.IntegrationEvents.Exceptions
{
    public class SubscriptionException : Exception
    {
        public SubscriptionException()
        {
        }

        public SubscriptionException(string message) : base(message)
        {
        }

        public SubscriptionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}