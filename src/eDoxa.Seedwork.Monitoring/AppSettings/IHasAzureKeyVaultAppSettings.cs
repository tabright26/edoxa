// Filename: IHasAzureKeyVaultAppSettings.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.AppSettings.Options;

namespace eDoxa.Seedwork.Monitoring.AppSettings
{
    public interface IHasAzureKeyVaultAppSettings
    {
        AzureKeyVaultOptions AzureKeyVault { get; set; }
    }
}
