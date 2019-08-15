﻿// Filename: Address.cs
// Date Created: 2019-08-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

#nullable disable

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class UserAddress
    {
        public Guid Id { get; set; }

        public UserAddressType Type { get; set; }

        public string Country { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }

        public Guid UserId { get; set; }
    }
}