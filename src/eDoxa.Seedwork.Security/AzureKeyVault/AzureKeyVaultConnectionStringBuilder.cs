// Filename: AzureKeyVaultConnectionStringBuilder.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.Security.AzureKeyVault
{
    public sealed class AzureKeyVaultConnectionStringBuilder
    {
        public AzureKeyVaultConnectionStringBuilder(string connectionString) : this()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return;
            }

            this.ParseConnectionString(connectionString);
        }

        private AzureKeyVaultConnectionStringBuilder()
        {
            Name = string.Empty;
            ClientId = string.Empty;
            ClientSecret = string.Empty;
        }

        public string Name { get; private set; }

        public string ClientId { get; private set; }

        public string ClientSecret { get; private set; }

        private void ParseConnectionString(string connectionString)
        {
            foreach (var properties in connectionString.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries))
            {
                var property = properties.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);

                var key = property[0];

                if (property.Length != 2)
                {
                    throw new ArgumentException(nameof(connectionString), $"Value for the connection string parameter name '{key}' was not found.");
                }

                var value = property[1].Trim();

                if (key.Equals(nameof(Name), StringComparison.OrdinalIgnoreCase))
                {
                    Name = value;
                }
                else if (key.Equals(nameof(ClientId), StringComparison.OrdinalIgnoreCase))
                {
                    ClientId = value;
                }
                else if (key.Equals(nameof(ClientSecret), StringComparison.OrdinalIgnoreCase))
                {
                    ClientSecret = value;
                }
            }
        }
    }
}
