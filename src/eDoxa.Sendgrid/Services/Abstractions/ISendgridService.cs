// Filename: IEmailService.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Sendgrid.Services.Abstractions
{
    public interface ISendgridService
    {
        Task SendEmailAsync(string email, string templateId, object templateData);
    }
}
