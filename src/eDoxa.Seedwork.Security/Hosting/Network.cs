// Filename: Network.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace eDoxa.Seedwork.Security.Hosting
{
    public static class Network
    {
        public static IPAddress GetIpAddress()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(address => address.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
