// Filename: NotificationsMapperFactory.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.AutoMapper.Factories;
using eDoxa.Notifications.DTO.Profiles;

namespace eDoxa.Notifications.DTO.Factories
{
    public sealed partial class NotificationsMapperFactory
    {
        private static readonly Lazy<NotificationsMapperFactory> _lazy = new Lazy<NotificationsMapperFactory>(() => new NotificationsMapperFactory());

        public static NotificationsMapperFactory Instance
        {
            get
            {
                return _lazy.Value;
            }
        }
    }

    public sealed partial class NotificationsMapperFactory : MapperFactory
    {
        protected override IEnumerable<Profile> CreateProfiles()
        {
            yield return new NotificationListProfile();
            yield return new NotificationProfile();
        }
    }
}