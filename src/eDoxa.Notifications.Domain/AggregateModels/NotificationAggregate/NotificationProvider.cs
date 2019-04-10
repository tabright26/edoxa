// Filename: NotificationProvider.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Notifications.Domain.Properties;

namespace eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate
{
    internal sealed partial class NotificationProvider
    {
        private static readonly Lazy<NotificationProvider> _lazy = new Lazy<NotificationProvider>(() => new NotificationProvider());

        public static NotificationProvider Instance
        {
            get
            {
                return _lazy.Value;
            }
        }
    }

    internal sealed partial class NotificationProvider
    {
        private static IEnumerable<NotificationDescription> Descriptions
        {
            get
            {
                yield return new NotificationDescription(
                    NotificationNames.UserEmailUpdated,
                    Resources.NotificationDescription_UserEmailUpdated_Title,
                    Resources.NotificationDescription_UserEmailUpdated_Template
                );

                yield return new NotificationDescription(
                    NotificationNames.ChallengePublished,
                    Resources.NotificationDescription_ChallengePublished_Title,
                    Resources.NotificationDescription_ChallengePublished_Template
                );

                yield return new NotificationDescription(
                    NotificationNames.ChallengeClosed,
                    Resources.NotificationDescription_ChallengeClosed_Title,
                    Resources.NotificationDescription_ChallengeClosed_Template
                );

                yield return new NotificationDescription(
                    NotificationNames.ChallengeParticipantRegistered,
                    Resources.NotificationDescription_ChallengeParticipantRegistered_Title,
                    Resources.NotificationDescription_ChallengeParticipantRegistered_Template
                );

                yield return new NotificationDescription(
                    NotificationNames.ChallengeParticipantPaid,
                    Resources.NotificationDescription_ChallengeParticipantPaid_Title,
                    Resources.NotificationDescription_ChallengeParticipantPaid_Template
                );
            }
        }

        public NotificationDescription FindDescriptionByName(string name)
        {
            var notificationDescription = Descriptions.SingleOrDefault(description => description.Name == name);

            if (notificationDescription == null)
            {
                throw new InvalidOperationException($"Cannot match a description for the notification name: {name}.");
            }

            return notificationDescription;
        }
    }
}