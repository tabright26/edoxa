// Filename: UserCreatedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IntegrationEvents;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    public sealed class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }
    }
}
