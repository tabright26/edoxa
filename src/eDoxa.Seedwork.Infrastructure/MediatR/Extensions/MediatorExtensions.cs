﻿// Filename: MediatorExtensions.cs
// Date Created: 2019-10-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;

using MediatR;

namespace eDoxa.Seedwork.Infrastructure.MediatR.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task PublishDomainEventAsync<TDomainEvent>(this IMediator mediator, TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
        {
            await mediator.Publish(domainEvent);
        }
    }
}