// Filename: ServiceCollectionExtensions.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Sendgrid.Services;
using eDoxa.Sendgrid.Services.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SendGrid;

namespace eDoxa.Sendgrid.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSendgrid(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SendgridOptions>(configuration.GetSection("Sendgrid"));

            services.AddTransient<ISendgridService>(
                provider =>
                {
                    var client = new SendGridClient(configuration["Sendgrid:ApiKey"]);

                    return new SendgridService(
                        client,
                        provider.GetRequiredService<ILogger<SendgridService>>(),
                        provider.GetRequiredService<IOptionsSnapshot<SendgridOptions>>());
                });
        }
    }
}
