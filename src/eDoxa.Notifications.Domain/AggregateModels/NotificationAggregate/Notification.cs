// Filename: Notification.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate
{
    public class Notification : Entity<NotificationId>, IAggregateRoot
    {
        private readonly NotificationProvider _provider = NotificationProvider.Instance;

        private DateTime _timestamp;
        private bool _isRead;
        private string _redirectUrl;
        private NotificationDescription _description;
        private INotificationMetadata _metadata;
        private User _user;

        public Notification(User user, string name, string redirectUrl, INotificationMetadata metadata) : this()
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            var description = _provider.FindDescriptionByName(name);
            description.Validate(metadata);            
            _description = description;
            _metadata = metadata;
            _redirectUrl = redirectUrl;
        }

        private Notification()
        {
            _isRead = false;
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
        }

        public string Title
        {
            get
            {
                return _description.Title;
            }
        }

        public string Message
        {
            get
            {
                return _description.FormatMessage(Metadata);
            }
        }

        public bool IsRead
        {
            get
            {
                return _isRead;
            }
        }

        public string RedirectUrl
        {
            get
            {
                return _redirectUrl;
            }
        }

        public INotificationMetadata Metadata
        {
            get
            {
                return _metadata;
            }
        }

        public User User
        {
            get
            {
                return _user;
            }
        }

        public void Read()
        {
            _isRead = true;
        }
    }
}