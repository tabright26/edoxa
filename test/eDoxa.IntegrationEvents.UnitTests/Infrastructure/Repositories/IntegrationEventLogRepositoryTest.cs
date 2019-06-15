// Filename: IntegrationEventLogRepositoryTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.IntegrationEvents.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class IntegrationEventLogRepositoryTest
    {
        [TestMethod]
        public void SaveIntegrationEventAsync()
        {
        }

        [TestMethod]
        public void MarkIntegrationEventAsPublishedAsync()
        {
            //// Arrange
            //var mockDbConnection = new Mock<DbConnection>();
            //mockDbConnection.SetupGet(x => x.ConnectionString).Returns("Server=mssql;Database=services.idsrv;User Id=sa;Password=fnU3Www9TnBDp3MA");
            //mockDbConnection.SetupGet(x => x.DataSource).Returns("edoxa.mssql.data");
            //mockDbConnection.SetupGet(x => x.Database).Returns("eDoxa.Services.Identity");

            //var integrationEventLoggerRepository = new IntegrationEventLoggerRepository(mockDbConnection.Object);

            //// Act
            //integrationEventLoggerRepository.MarkIntegrationEventAsPublishedAsync(new MockIntegrationEvent());
        }
    }
}
