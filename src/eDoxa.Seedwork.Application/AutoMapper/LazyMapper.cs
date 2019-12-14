// Filename: LazyMapper.cs
// Date Created: 2019-12-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using AutoMapper;

using eDoxa.Seedwork.Application.AutoMapper.TypeConverters;

namespace eDoxa.Seedwork.Application.AutoMapper
{
    public sealed class LazyMapper : Lazy<IMapper>
    {
        public LazyMapper(params Type[] types) : base(
            () =>
            {
                types.Prepend(typeof(EnumerableToRepeatedFieldTypeConverter<,>));

                var configuration = new MapperConfiguration(options => options.AddMaps(types));

                configuration.AssertConfigurationIsValid();

                return new Mapper(configuration);
            })
        {
        }
    }
}
