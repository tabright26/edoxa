// Filename: IntegrationEventState.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.ServiceBus
{
    /// <summary>
    ///     The <see cref="IntegrationEvent" /> states.
    /// </summary>
    public enum IntegrationEventState
    {
        /// <summary>
        ///     Not published (0)
        /// </summary>
        NotPublished = 0,

        /// <summary>
        ///     Published (1)
        /// </summary>
        Published = 1,

        /// <summary>
        ///     Published failed (2)
        /// </summary>
        PublishedFailed = 2
    }
}