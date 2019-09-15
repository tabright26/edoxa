// Filename: ClanModel.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    public class ClanModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid LeaderId { get; set; }

        public ICollection<MemberModel> Members { get; set; }
    }
}
