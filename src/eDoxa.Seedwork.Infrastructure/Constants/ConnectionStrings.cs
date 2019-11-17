// Filename: ConnectionStrings.cs
// Date Created: 2019-11-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Infrastructure.Constants
{
    public static class ConnectionStrings
    {
        public const string AzureKeyVault = nameof(AzureKeyVault);
        public const string AzureBlobStorage = nameof(AzureBlobStorage);
        public const string AzureServiceBus = nameof(AzureServiceBus);
        public const string RabbitMq = nameof(RabbitMq);
        public const string Redis = nameof(Redis);
        public const string SqlServer = nameof(SqlServer);
    }
}
