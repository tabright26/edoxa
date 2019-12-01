// Filename: DobResponse.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Responses
{
    [JsonObject]
    public sealed class DobResponse
    {
        [JsonConstructor]
        public DobResponse(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        [JsonProperty("year")]
        public int Year { get; }

        [JsonProperty("month")]
        public int Month { get; }

        [JsonProperty("day")]
        public int Day { get; }
    }
}
