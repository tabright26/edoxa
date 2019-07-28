// Filename: DynamicIntegrationEventSubscription.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public sealed class DynamicIntegrationEventSubscription : IntegrationEventSubscription
    {
        public DynamicIntegrationEventSubscription(Type dynamicHandlerType) : base(dynamicHandlerType, true)
        {
            if (!typeof(IDynamicIntegrationEventHandler).IsAssignableFrom(dynamicHandlerType))
            {
                throw new ArgumentException(
                    $"The {dynamicHandlerType.Name} is not assignable form {nameof(IDynamicIntegrationEventHandler)}.",
                    nameof(dynamicHandlerType)
                );
            }
        }
    }
}
