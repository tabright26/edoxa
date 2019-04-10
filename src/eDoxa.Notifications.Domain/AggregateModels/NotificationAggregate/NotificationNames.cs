// Filename: NotificationNames.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate
{
    public static class NotificationNames
    {
        // Identity
        public const string UserEmailUpdated = "user.email.updated";

        // Challenges
        public const string ChallengePublished = "challenge.published";
        public const string ChallengeClosed = "challenge.closed";
        public const string ChallengeParticipantRegistered = "challenge.participant.registered";
        public const string ChallengeParticipantPaid = "challenge.participant.paid";

        public static IEnumerable<string> GetValues()
        {
            var type = typeof(NotificationNames);

            var hashSet = new HashSet<string>();

            foreach (var fieldInfo in type.GetFields())
            {
                hashSet.Add(fieldInfo.GetValue(type).ToString());
            }

            return hashSet;
        }

        public static bool IsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return GetValues().Any(value => value == name);
        }
    }
}