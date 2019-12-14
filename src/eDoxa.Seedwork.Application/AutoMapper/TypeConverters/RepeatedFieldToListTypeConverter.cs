//// Filename: RepeatedFieldToListTypeConverter.cs
//// Date Created: 2019-12-14
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Collections.Generic;
//using System.Linq;

//using AutoMapper;

//using Google.Protobuf.Collections;

//namespace eDoxa.Seedwork.Application.AutoMapper.TypeConverters
//{
//    internal sealed class RepeatedFieldToListTypeConverter<TSourceItem, TDestinationItem> : ITypeConverter<RepeatedField<TSourceItem>, List<TDestinationItem>>
//    {
//        public List<TDestinationItem> Convert(RepeatedField<TSourceItem> source, List<TDestinationItem> destination, ResolutionContext context)
//        {
//            destination ??= new List<TDestinationItem>();

//            destination.AddRange(source.Select(sourceItem => context.Mapper.Map<TDestinationItem>(sourceItem)));

//            return destination;
//        }
//    }
//}
