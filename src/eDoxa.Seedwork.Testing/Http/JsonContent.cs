// Filename: JsonContent.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Testing.Http
{
    public sealed class JsonContent : StringContent
    {
        public static JsonContent EmptyBody = new JsonContent("{}");

        public JsonContent(object content) : base(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
        {
        }
    }
}
