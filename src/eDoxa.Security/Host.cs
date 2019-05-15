// Filename: Host.cs
// Date Created: 2019-05-14
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

namespace eDoxa.Security
{
    public static class Host
    {
        public static IPAddress GetIpAddress()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(address => address.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}