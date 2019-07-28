// Filename: IntegrationEventSubscription.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public class IntegrationEventSubscription : ValueObject
    {
        public static readonly IntegrationEventSubscription Default = new IntegrationEventSubscription(typeof(IntegrationEventSubscription), default);

        public IntegrationEventSubscription(Type handlerType) : this(handlerType, false)
        {
            if (!typeof(IIntegrationEventHandler).IsAssignableFrom(handlerType))
            {
                throw new ArgumentException(
                    $"The {handlerType.Name} is not assignable form {nameof(IIntegrationEventHandler)}.",
                    nameof(handlerType)
                );
            }
        }

        protected IntegrationEventSubscription(Type handlerType, bool isDynamic)
        {
            HandlerType = handlerType;
            IsDynamic = isDynamic;
        }

        public Type HandlerType { get; }

        public bool IsDynamic { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return HandlerType;
        }

        public override string ToString()
        {
            return HandlerType.Name;
        }
    }
}
