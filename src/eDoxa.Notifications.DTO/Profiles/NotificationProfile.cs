// Filename: NotificationProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;

namespace eDoxa.Notifications.DTO.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            this.CreateMap<Notification, NotificationDTO>()
                .ForMember(notification => notification.Id, config => config.MapFrom(notification => notification.Id.ToGuid()))
                .ForMember(notification => notification.Timestamp, config => config.MapFrom(notification => notification.Timestamp))
                .ForMember(notification => notification.Title, config => config.MapFrom(notification => notification.Title))
                .ForMember(notification => notification.Message, config => config.MapFrom(notification => notification.Message))
                .ForMember(notification => notification.RedirectUrl, config => config.MapFrom(notification => notification.RedirectUrl))
                .ForMember(notification => notification.IsRead, config => config.MapFrom(notification => notification.IsRead));
        }
    }
}