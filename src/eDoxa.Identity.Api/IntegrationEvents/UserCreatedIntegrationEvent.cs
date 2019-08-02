// Filename: UserCreatedIntegrationEvent.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.ServiceBus;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public sealed class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public UserCreatedIntegrationEvent(
            Guid userId,
            string email,
            string firstName,
            string lastName,
            int year,
            int month,
            int day
        ) : base(Guid.NewGuid())
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Year = year;
            Month = month;
            Day = day;
        }

        public Guid UserId { get; private set; }

        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }
    }
}
