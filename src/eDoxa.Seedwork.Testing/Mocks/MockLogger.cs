// Filename: MockLogger.cs
// Date Created: 2019-07-01
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

namespace eDoxa.Seedwork.Testing.Mocks
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
            );
        }
    }
}
