// Filename: IEmailService.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Notifications.Domain.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlContent);
    }
}
