// Filename: PayoutConverter.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.Abstractions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Infrastructure.Converters
{
    internal sealed class PayoutConverter : ValueConverter<IPayout, string>
    {
        private static readonly IMapper Mapper = new Mapper(
            new MapperConfiguration(
                configuration =>
                {
                    configuration.CreateMap<Bucket, BucketDTO>()
                                 .ForMember(bucket => bucket.Prize, config => config.MapFrom<decimal>(bucket => bucket.Prize))
                                 .ForMember(bucket => bucket.Size, config => config.MapFrom<int>(bucket => bucket.Size));

                    configuration.CreateMap<Payout, PayoutDTO>()
                                 .ForMember(payout => payout.Currency, config => config.MapFrom(payout => payout.PrizeType.Value))
                                 .ForMember(payout => payout.Buckets, config => config.MapFrom(payout => payout.Buckets));

                    configuration.CreateMap<PayoutDTO, Payout>()
                                 .ConstructUsing(
                                     payout => new Payout(
                                         new Buckets(
                                             payout.Buckets.Select(
                                                 bucket => new Bucket(
                                                     new Prize(bucket.Prize, Currency.FromValue(payout.Currency)),
                                                     new BucketSize(bucket.Size)
                                                 )
                                             )
                                         )
                                     )
                                 );
                }
            )
        );

        public PayoutConverter() : base(
            scoring => JsonConvert.SerializeObject(Mapper.Map<PayoutDTO>(scoring), Formatting.None),
            scoring => Mapper.Map<Payout>(JsonConvert.DeserializeObject<PayoutDTO>(scoring))
        )
        {
        }

        [JsonObject]
        private class PayoutDTO
        {
            [JsonProperty("currency")]
            public int Currency { get; set; }

            [JsonProperty("buckets")]
            public IList<BucketDTO> Buckets { get; set; }
        }

        [JsonObject]
        private class BucketDTO
        {
            [JsonProperty("prize")]
            public decimal Prize { get; set; }

            [JsonProperty("size")]
            public int Size { get; set; }
        }
    }
}
