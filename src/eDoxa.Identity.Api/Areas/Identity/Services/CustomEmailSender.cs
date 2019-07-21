// Filename: EmailSender.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity.UI.Services;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public class CustomEmailSender : IEmailSender
    {
        [NotNull]
        public Task SendEmailAsync([NotNull] string email, [NotNull] string subject, [NotNull] string htmlMessage)
        {
            return Task.CompletedTask; // TODO: Add SendGrid implementation.
        }
    }
}
