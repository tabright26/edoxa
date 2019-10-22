// Filename: Claims.cs
// Date Created: 2019-10-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Domain.Miscs
{
    [JsonArray]
    public sealed class Claims : HashSet<Claim>
    {
        [JsonConstructor]
        public Claims(IEnumerable<Claim> claims) : base(claims)
        {
        }

        public Claims(params Claim[] claims) : base(claims)
        {
            
        }

        public void Add(string type, string value)
        {
            this.Add(new Claim(type, value));
        }
    }
}
