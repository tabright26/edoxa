// Filename: IdentityMapperFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.AutoMapper.Factories;
using eDoxa.Identity.DTO.Profiles;

namespace eDoxa.Identity.DTO.Factories
{
    public sealed partial class IdentityMapperFactory
    {
        private static readonly Lazy<IdentityMapperFactory> Lazy = new Lazy<IdentityMapperFactory>(() => new IdentityMapperFactory());

        public static IdentityMapperFactory Instance => Lazy.Value;
    }

    public sealed partial class IdentityMapperFactory : MapperFactory
    {
        protected override IEnumerable<Profile> CreateProfiles()
        {
            yield return new UserListProfile();
            yield return new UserProfile();
        }
    }
}