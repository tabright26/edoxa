// Filename: JsonContent.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Testing
{
    public sealed class JsonContent : StringContent
    {
        public JsonContent(object content) : base(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
        {
        }
    }
}
