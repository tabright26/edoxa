// Filename: IAggregateRoot.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.Domain
{
    /// <summary>
    ///     Having an aggregate root means that most of the code related to consistency and business rules of the aggregate’s
    ///     entities should be implemented as methods in the aggregate root class.
    /// </summary>
    public interface IAggregateRoot
    {
        // Marker interface
    }
}