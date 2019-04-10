// Filename: NotificationListProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;

namespace eDoxa.Notifications.DTO.Profiles
{
    public sealed class NotificationListProfile : Profile
    {
        public NotificationListProfile()
        {
            this.CreateMap<IEnumerable<Notification>, NotificationListDTO>()
                .ForMember(list => list.Items, config => config.MapFrom(notifications => notifications.ToList()));
        }
    }
}