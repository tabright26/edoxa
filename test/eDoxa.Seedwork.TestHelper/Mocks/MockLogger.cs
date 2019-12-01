// Filename: MockLogger.cs
// Date Created: 2019-08-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.Extensions.Logging;

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
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                )
            ).Verifiable();
        }

        public void Verify(Times times)
        {
            this.Verify(logger => logger.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), times);
        }
    }
}
