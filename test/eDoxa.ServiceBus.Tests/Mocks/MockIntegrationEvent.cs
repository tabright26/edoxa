﻿// Filename: MockIntegrationEvent.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace eDoxa.ServiceBus.Tests.Mocks
{
    internal class MockIntegrationEvent : IntegrationEvent, IEquatable<IntegrationEvent>
    {
        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public MockIntegrationEvent()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockIntegrationEvent" /> class.
        /// </summary>
        /// <param name="id">The <see cref="MockIntegrationEvent" /> identifier.</param>
        /// <param name="created">The <see cref="MockIntegrationEvent" /> time stamp.</param>
        private MockIntegrationEvent(Guid id, DateTime created)
        {
            Id = id;
            Created = created;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="IntegrationEvent" /> is equal to the current object.
        /// </summary>
        /// <param name="other">The <see cref="IntegrationEvent" /> to compare with the current object.</param>
        /// <returns>
        ///     true if the specified <see cref="IntegrationEvent" /> is equal to the current <see cref="IntegrationEvent" />;
        ///     otherwise, false.
        /// </returns>
        public bool Equals(IntegrationEvent other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return other.Id == Id;
        }

        /// <summary>
        ///     Deserializes the specified serialized <see cref="MockIntegrationEvent" />.
        /// </summary>
        /// <param name="serializedIntegrationEvent">The serialized <see cref="MockIntegrationEvent" />.</param>
        /// <returns>The <see cref="MockIntegrationEvent" />.</returns>
        public static MockIntegrationEvent Deserialize(string serializedIntegrationEvent)
        {
            var jsonObject = JObject.Parse(serializedIntegrationEvent);

            var id = jsonObject.GetValue(nameof(Id)).ToObject<Guid>();

            var created = jsonObject.GetValue(nameof(Created)).ToObject<DateTime>();

            return new MockIntegrationEvent(id, created);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="IntegrationEvent" /> instances are considered equal.
        /// </summary>
        /// <param name="left">The left <see cref="IntegrationEvent" /> to compare.</param>
        /// <param name="right">The right <see cref="IntegrationEvent" /> to compare.</param>
        /// <returns>
        ///     true if the <see cref="IntegrationEvent" />s are considered equal; otherwise, false. If both
        ///     <see cref="IntegrationEvent" /> left and <see cref="IntegrationEvent" /> right are null, the method returns true.
        /// </returns>
        public static bool operator ==(MockIntegrationEvent left, MockIntegrationEvent right)
        {
            return EqualityComparer<MockIntegrationEvent>.Default.Equals(left, right);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="IntegrationEvent" /> instances are considered not equal.
        /// </summary>
        /// <param name="left">The left <see cref="IntegrationEvent" /> to compare.</param>
        /// <param name="right">The right <see cref="IntegrationEvent" /> to compare.</param>
        /// <returns>
        ///     true if the <see cref="IntegrationEvent" />s are considered not equal; otherwise, false. If both
        ///     <see cref="IntegrationEvent" /> left and <see cref="IntegrationEvent" /> right are null, the
        ///     method returns false.
        /// </returns>
        public static bool operator !=(MockIntegrationEvent left, MockIntegrationEvent right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as IntegrationEvent);
        }

        /// <summary>
        ///     Serves as the default hash function.
        /// </summary>
        /// <returns>Hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * 397 ^ Created.GetHashCode();
            }
        }
    }
}