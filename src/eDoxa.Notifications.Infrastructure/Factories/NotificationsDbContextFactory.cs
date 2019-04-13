using System;
using System.Reflection;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Moq;

namespace eDoxa.Notifications.Infrastructure.Factories
{
    public class NotificationsDbContextFactory : IDesignTimeDbContextFactory<NotificationsDbContext>
    {
        public NotificationsDbContext CreateDbContext(string[] args)
        {
            var mediator = new Mock<IMediator>();

            var builder = new DbContextOptionsBuilder<NotificationsDbContext>().UseSqlServer(
                "Server=127.0.0.1,1433;Database=eDoxa.Services.Notifications;User Id=sa;Password=fnU3Www9TnBDp3MA;",
                options =>
                {
                    options.MigrationsAssembly(Assembly.GetAssembly(typeof(NotificationsDbContextFactory)).GetName().Name);
                    options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                }
            );

            return new NotificationsDbContext(builder.Options, mediator.Object);
        }
    }
}
