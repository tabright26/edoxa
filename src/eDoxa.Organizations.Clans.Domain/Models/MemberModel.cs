// Filename: MemberModel.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public class MemberModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ClanModel Clan { get; set; }
    }
}
