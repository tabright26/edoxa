// Filename: IDomainEvent.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using MediatR;

namespace eDoxa.Seedwork.Domain
{
    public interface IDomainEvent : INotification
    {
    }
}
