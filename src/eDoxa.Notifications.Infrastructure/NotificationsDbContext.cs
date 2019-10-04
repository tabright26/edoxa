// Filename: NotificationsDbContext.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Notifications.Infrastructure
{
    public class NotificationsDbContext : DbContext
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : base(options)
        {
        }
    }
}
