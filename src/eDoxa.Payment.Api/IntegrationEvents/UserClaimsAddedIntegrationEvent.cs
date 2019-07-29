// Filename: UserClaimAddedIntegrationEvent.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.ServiceBus;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    public sealed class UserClaimsAddedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }

        public IDictionary<string, string> Claims { get; set; }
    }
}
