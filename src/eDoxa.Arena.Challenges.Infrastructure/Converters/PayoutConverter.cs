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
                config =>
                {
                    config.CreateMap<BucketDTO, Bucket>().ConstructUsing(x => new Bucket(new Prize(x.Prize), x.Size));

                    config.CreateMap<Bucket, BucketDTO>()
                          .ForMember(x => x.Prize, con => con.MapFrom<decimal>(x => x.Prize))
                          .ForMember(x => x.Size, con => con.MapFrom(x => x.Size));

                    config.CreateMap<Payout, PayoutDTO>().ForMember(x => x.Buckets, cob => cob.MapFrom(x => x.Buckets));

                    config.CreateMap<PayoutDTO, Payout>()
                          .ConstructUsing(x => new Payout(x.Buckets.Select(bucket => new Bucket(new Prize(bucket.Prize), bucket.Size)).ToList()));
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
            public IList<BucketDTO> Buckets { get; set; }
        }

        [JsonObject]
        private class BucketDTO
        {
            public decimal Prize { get; set; }

            public int Size { get; set; }
        }
    }
}
