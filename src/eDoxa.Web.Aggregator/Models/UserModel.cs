// Filename: UserModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Web.Aggregator.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public DoxatagModel Doxatag { get; set; }
    }
}
