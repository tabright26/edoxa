// Filename: IntegrationEvent.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.ServiceBus
{
    //TODO: Add a property for the name of an event to publish the integration event in an adapter format for Javascript. (Version v.3)
    public abstract class IntegrationEvent
    {
        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }

        /// <summary>
        ///     The <see cref="IntegrationEvent" /> id.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        ///     The <see cref="IntegrationEvent" /> created.
        /// </summary>
        public DateTime Created { get; protected set; }
    }
}