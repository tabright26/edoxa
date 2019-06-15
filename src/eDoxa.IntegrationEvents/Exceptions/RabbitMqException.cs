// Filename: RabbitMqException.cs
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
    public class RabbitMqException : Exception
    {
        public RabbitMqException()
        {
        }

        public RabbitMqException(string message) : base(message)
        {
        }

        public RabbitMqException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}