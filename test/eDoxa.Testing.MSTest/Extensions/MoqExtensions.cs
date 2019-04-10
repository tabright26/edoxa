// Filename: MoqExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

using Moq;

namespace eDoxa.Testing.MSTest.Extensions
{
    public static class MoqExtensions
    {
        public static void SetupLoggerWithLogInformationVerifiable<T>(this Mock<ILogger<T>> mockLogger)
        {
            mockLogger.Setup(
                          logger => logger.Log(
                              LogLevel.Information,
                              0,
                              It.IsAny<FormattedLogValues>(),
                              It.IsAny<Exception>(),
                              It.IsAny<Func<object, Exception, string>>()
                          )
                      )
                      .Verifiable();
        }

        public static void SetupLoggerWithLogWarningVerifiable<T>(this Mock<ILogger<T>> mockLogger)
        {
            mockLogger.Setup(
                          logger => logger.Log(
                              LogLevel.Warning,
                              0,
                              It.IsAny<FormattedLogValues>(),
                              It.IsAny<Exception>(),
                              It.IsAny<Func<object, Exception, string>>()
                          )
                      )
                      .Verifiable();
        }

        public static void SetupLoggerWithLogLevelErrorVerifiable<T>(this Mock<ILogger<T>> mockLogger)
        {
            mockLogger.Setup(
                          logger => logger.Log(
                              LogLevel.Error,
                              0,
                              It.IsAny<FormattedLogValues>(),
                              It.IsAny<Exception>(),
                              It.IsAny<Func<object, Exception, string>>()
                          )
                      )
                      .Verifiable();
        }

        public static void SetupLoggerWithLogLevelCriticalVerifiable<T>(this Mock<ILogger<T>> mockLogger)
        {
            mockLogger.Setup(
                          logger => logger.Log(
                              LogLevel.Critical,
                              0,
                              It.IsAny<FormattedLogValues>(),
                              It.IsAny<Exception>(),
                              It.IsAny<Func<object, Exception, string>>()
                          )
                      )
                      .Verifiable();
        }
    }
}