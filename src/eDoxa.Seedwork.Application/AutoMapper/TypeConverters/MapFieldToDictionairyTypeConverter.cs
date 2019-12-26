//// Filename: MapFieldToDictionairyTypeConverter.cs
//// Date Created: 2019-12-14
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Collections.Generic;

//using AutoMapper;

//using Google.Protobuf.Collections;

//namespace eDoxa.Seedwork.Application.AutoMapper.TypeConverters
//{
//    public sealed class
//        MapFieldToDictionairyTypeConverter<TSourceKey, TSourceValue, TDestinationKey, TDestinationValue> : ITypeConverter<MapField<TSourceKey, TSourceValue>,
//            Dictionary<TDestinationKey, TDestinationValue>>
//    where TSourceKey : notnull
//    where TDestinationKey : notnull
//    {
//        public Dictionary<TDestinationKey, TDestinationValue> Convert(
//            MapField<TSourceKey, TSourceValue> source,
//            Dictionary<TDestinationKey, TDestinationValue> destination,
//            ResolutionContext context
//        )
//        {
//            destination ??= new Dictionary<TDestinationKey, TDestinationValue>();

//            foreach (var (key, value) in source)
//            {
//                var destinationKey = context.Mapper.Map<TDestinationKey>(key);

//                var destinationValue = context.Mapper.Map<TDestinationValue>(value);

//                destination.Add(destinationKey, destinationValue);
//            }

//            return destination;
//        }
//    }
//}
