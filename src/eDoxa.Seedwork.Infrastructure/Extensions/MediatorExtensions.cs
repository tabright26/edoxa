﻿// Filename: MediatorExtensions.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;

using MediatR;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    internal static class MediatorExtensions
    {
        public static Task PublishDomainEventsAsync<TDomainEvent>(this IMediator mediator, TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
        {
            return mediator.Publish(domainEvent);
        }
    }
}
