// Filename: MapperFactory.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using AutoMapper;
using AutoMapper.Configuration;

namespace eDoxa.AutoMapper.Factories
{
    public abstract class MapperFactory : IMapperFactory
    {
        public MapperConfigurationExpression CreateConfiguration()
        {
            var config = new MapperConfigurationExpression
            {
                AllowNullCollections = null
            };

            foreach (var profile in this.CreateProfiles())
            {
                config.AddProfile(profile);
            }

            return config;
        }

        public IMapper CreateMapper()
        {
            var provider = new MapperConfiguration(this.CreateConfiguration());

            provider.AssertConfigurationIsValid();

            return new Mapper(provider);
        }

        protected abstract IEnumerable<Profile> CreateProfiles();
    }
}