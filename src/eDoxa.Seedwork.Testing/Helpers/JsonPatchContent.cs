// Filename: JsonPatchContent.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Testing.Helpers
{
    public sealed class JsonPatchContent : StringContent
    {
        public JsonPatchContent(object content) : base(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json-patch+json")
        {
        }
    }
}
