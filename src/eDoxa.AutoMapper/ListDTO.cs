// Filename: ListDTO.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.AutoMapper
{
    [JsonArray]
    public partial class ListDTO<T>
    {
        public ListDTO()
        {
            Items = new List<T>();
        }

        [JsonProperty("items")] public List<T> Items { get; set; }
    }

    public partial class ListDTO<T> : IEnumerable<T>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}