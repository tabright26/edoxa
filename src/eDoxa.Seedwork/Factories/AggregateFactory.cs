// Filename: AggregateFactory.cs
// Date Created: 2019-04-08
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Factories
{
    // TODO: Add to appsettings.json
    public abstract class AggregateFactory
    {
        protected UserData AdminData
        {
            get
            {
                return new UserData
                {
                    Id = Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"),
                    Email = "admin@edoxa.gg",
                    Password = "Pass@word1",
                    FirstName = "Admin",
                    LastName = "eDoxa",
                    BirthDate = new DateTime(1970, 1, 1),
                    PhoneNumber = "5555555555",
                    Username = "Administrator",
                    StripeCustomerId = "cus_Et3R5o75SIURKE"
                };
            }
        }

        protected UserData FrancisData
        {
            get
            {
                return new UserData
                {
                    Id = Guid.Parse("950c1974-cdac-43a6-adba-45c18f06437a"),
                    Email = "francis@edoxa.gg",
                    Password = "Pass@word1",
                    FirstName = "Francis",
                    LastName = "Quenneville",
                    BirthDate = new DateTime(1995, 5, 6),
                    PhoneNumber = "5147580313",
                    Username = "Franki320",
                    StripeCustomerId = "cus_Et3TSffi8eXU7d"
                };
            }
        }

        protected UserData RoyData
        {
            get
            {
                return new UserData
                {
                    Id = Guid.Parse("333c1dff-007c-4496-99e1-f3dcbb75d129"),
                    Email = "roy@edoxa.gg",
                    Password = "Pass@word1",
                    FirstName = "Roy",
                    LastName = "El-Khouri",
                    BirthDate = new DateTime(1993, 1, 1),
                    PhoneNumber = "4384924161",
                    Username = "03adro09",
                    StripeCustomerId = "cus_Et3TjhQNvVXrPU"
                };
            }
        }

        protected UserData RyanData
        {
            get
            {
                return new UserData
                {
                    Id = Guid.Parse("71ae1621-3a5b-4c8a-ad71-1171ec50725a"),
                    Email = "ryan@edoxa.gg",
                    Password = "Pass@word1",
                    FirstName = "Ryan",
                    LastName = "El-Khouri",
                    BirthDate = new DateTime(1995, 1, 1),
                    PhoneNumber = "5146094161",
                    Username = "DeadlyStrength",
                    StripeCustomerId = "cus_Et3Sq2DeDSa7Xj"
                };
            }
        }
    }
}