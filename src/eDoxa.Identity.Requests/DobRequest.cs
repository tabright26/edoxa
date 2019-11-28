// Filename: DobRequest.cs
// Date Created: 2019-10-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Requests
{
    [DataContract]
    public sealed class DobRequest
    {
        public DobRequest(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        [DataMember(Name = "year")]
        public int Year { get; }

        [DataMember(Name = "month")]
        public int Month { get; }

        [DataMember(Name = "day")]
        public int Day { get; }
    }
}
