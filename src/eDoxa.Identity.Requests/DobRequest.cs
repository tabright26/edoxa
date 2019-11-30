// Filename: DobRequest.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Requests
{
    [JsonObject]
    public sealed class DobRequest
    {
        [JsonConstructor]
        public DobRequest(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public DobRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("year")]
        public int Year { get; private set; }

        [JsonProperty("month")]
        public int Month { get; private set; }

        [JsonProperty("day")]
        public int Day { get; private set; }
    }
}
