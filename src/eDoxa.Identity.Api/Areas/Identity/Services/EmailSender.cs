// Filename: CustomEmailSender.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.UI.Services;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        
        public Task SendEmailAsync( string email,  string subject,  string htmlMessage)
        {
            return Task.CompletedTask; // TODO: Add SendGrid implementation.
        }
    }
}
