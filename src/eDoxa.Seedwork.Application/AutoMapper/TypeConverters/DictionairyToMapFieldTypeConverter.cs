//// Filename: DictionairyToMapFieldTypeConverter.cs
//// Date Created: 2019-12-14
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Collections.Generic;

//using AutoMapper;

//using Google.Protobuf.Collections;

//namespace eDoxa.Seedwork.Application.AutoMapper.TypeConverters
//{
//    internal sealed class
//        DictionairyToMapFieldTypeConverter<TSourceKey, TSourceValue, TDestinationKey, TDestinationValue> : ITypeConverter<IDictionary<TSourceKey, TSourceValue>,
//            MapField<TDestinationKey, TDestinationValue>>
//    where TSourceKey : notnull
//    where TDestinationKey : notnull
//    {
//        public MapField<TDestinationKey, TDestinationValue> Convert(
//            IDictionary<TSourceKey, TSourceValue> source,
//            MapField<TDestinationKey, TDestinationValue> destination,
//            ResolutionContext context
//        )
//        {
//            destination ??= new MapField<TDestinationKey, TDestinationValue>();

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
