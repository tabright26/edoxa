// Filename: MockEmailSender.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.UI.Services;

using Moq;

namespace eDoxa.Identity.TestHelpers.Mocks
{
    internal sealed class MockEmailSender : Mock<IEmailSender>
    {
        public MockEmailSender()
        {
            this.Setup(emailSender => emailSender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
        }
    }
}
