// Filename: MockLogger.cs
// Date Created: 2019-08-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

using Moq;

namespace eDoxa.Seedwork.TestHelper.Mocks
{
    public sealed class MockLogger<T> : Mock<ILogger<T>>
    {
        public MockLogger()
        {
            this.Setup(
                logger => logger.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<FormattedLogValues>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            ).Verifiable();
        }

        public void Verify(Times times)
        {
            this.Verify(logger => logger.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<FormattedLogValues>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()), times);
        }
    }
}
