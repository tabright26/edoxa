// Filename: MockLoggerExtensions.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

using Moq;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class MockLoggerExtensions
    {
        public static void SetupLog<T>(this Mock<ILogger<T>> mockLogger)
        {
            mockLogger.Setup(
                mock => mock.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<FormattedLogValues>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
        }
    }
}
