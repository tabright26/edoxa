// Filename: VerifyAccountCommand.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Commands.Abstractions;
using eDoxa.Functional;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class VerifyAccountCommand : Command<Either>
    {
        public VerifyAccountCommand(string line1, string city, string state, string postalCode, bool termsOfService)
        {
            Line1 = line1;
            City = city;
            State = state;
            PostalCode = postalCode;
            TermsOfService = termsOfService;
        }

        [DataMember(Name = "line1")] public string Line1 { get; private set; }

        [DataMember(Name = "line2", IsRequired = false)]
        public string Line2 { get; private set; }

        [DataMember(Name = "city")] public string City { get; private set; }

        [DataMember(Name = "state")] public string State { get; private set; }

        [DataMember(Name = "postalCode")] public string PostalCode { get; private set; }

        [DataMember(Name = "termsOfService")] public bool TermsOfService { get; private set; }
    }
}