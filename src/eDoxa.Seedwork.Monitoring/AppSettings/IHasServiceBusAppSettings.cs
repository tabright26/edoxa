﻿// Filename: IHasServiceBusAppSettings.cs
// Date Created: 2019-07-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.AppSettings.Options;

namespace eDoxa.Seedwork.Monitoring.AppSettings
{
    public interface IHasServiceBusAppSettings
    {
        bool AzureServiceBusEnabled { get; set; }

        ServiceBusOptions ServiceBus { get; set; }
    }
}