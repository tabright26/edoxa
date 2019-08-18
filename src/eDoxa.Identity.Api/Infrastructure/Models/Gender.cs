// Filename: Gender.cs
// Date Created: 2019-08-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class Gender : Enumeration<Gender>
    {
        public static readonly Gender Male = new Gender(1, nameof(Male));
        public static readonly Gender Female = new Gender(1 << 1, nameof(Female));
        public static readonly Gender Other = new Gender(1 << 2, nameof(Other));

        public Gender()
        {
        }

        private Gender(int value, string name) : base(value, name)
        {
        }
    }
}
