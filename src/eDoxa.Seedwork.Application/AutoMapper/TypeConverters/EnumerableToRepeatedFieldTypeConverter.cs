// Filename: EnumerableToRepeatedFieldTypeConverter.cs
// Date Created: 2019-12-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using Google.Protobuf.Collections;

namespace eDoxa.Seedwork.Application.AutoMapper.TypeConverters
{
    internal sealed class
        EnumerableToRepeatedFieldTypeConverter<TSourceItem, TDestinationItem> : ITypeConverter<IEnumerable<TSourceItem>, RepeatedField<TDestinationItem>>
    {
        public RepeatedField<TDestinationItem> Convert(IEnumerable<TSourceItem> source, RepeatedField<TDestinationItem> destination, ResolutionContext context)
        {
            destination ??= new RepeatedField<TDestinationItem>();

            foreach (var sourceItem in source)
            {
                var destinationItem = context.Mapper.Map<TDestinationItem>(sourceItem);

                destination.Add(destinationItem);
            }

            return destination;
        }
    }
}
