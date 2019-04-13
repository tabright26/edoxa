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
        private DateTime _timestamp;
        private bool _isRead;
        private string _title;
        private string _message;
        private string _redirectUrl;
        private User _user;

        public Notification(User user, string title, string message, string redirectUrl = null) : this()
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _title = title;
            _message = message;
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
                return _title;
            }
        }

        public string Message
        {
            get
            {
                return _message;
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