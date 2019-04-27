// Filename: IMapperFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper.Configuration;

namespace eDoxa.AutoMapper.Factories
{
    public interface IMapperFactory
    {
        MapperConfigurationExpression CreateConfiguration();
    }
}