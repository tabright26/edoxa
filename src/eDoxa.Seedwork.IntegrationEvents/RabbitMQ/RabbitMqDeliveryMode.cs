// Filename: RabbitMqDeliveryMode.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.IntegrationEvents.RabbitMQ
{
    /// <summary>
    ///     RabbitMQ channel delivery modes.
    /// </summary>
    public enum RabbitMqDeliveryMode
    {
        /// <summary>
        ///     Non-persistent (1).
        /// </summary>
        NonPersistent = 1,

        /// <summary>
        ///     Persistent (2).
        /// </summary>
        Persistent = 2
    }
}