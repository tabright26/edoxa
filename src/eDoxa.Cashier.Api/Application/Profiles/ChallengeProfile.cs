// Filename: ChallengeProfile.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<IChallenge, ChallengePayoutDto>()
                .ForMember(challenge => challenge.ChallengeId, config => config.MapFrom(challenge => challenge.Id.ToString()))
                .ForMember(challenge => challenge.Entries, config => config.MapFrom(challenge => challenge.Payout.Entries))
                .ForMember(challenge => challenge.EntryFee, config => config.MapFrom(challenge => challenge.Payout.EntryFee))
                .ForMember(challenge => challenge.PrizePool, config => config.MapFrom(challenge => challenge.Payout.PrizePool))
                .ForMember(challenge => challenge.Buckets, config => config.MapFrom(challenge => challenge.Payout.Buckets));

            this.CreateMap<ChallengePayoutBucket, ChallengePayoutBucketDto>()
                .ForMember(bucket => bucket.Size, config => config.MapFrom<int>(bucket => bucket.Size))
                .ForMember(bucket => bucket.Prize, config => config.MapFrom<DecimalValue>(bucket => bucket.Prize.Amount));

            this.CreateMap<EntryFee, CurrencyDto>()
                .ForMember(entryFee => entryFee.Type, config => config.MapFrom(entryFee => entryFee.Type.ToEnum<EnumCurrencyType>()))
                .ForMember(entryFee => entryFee.Amount, config => config.MapFrom<DecimalValue>(entryFee => entryFee.Amount));

            this.CreateMap<ChallengePayoutPrizePool, CurrencyDto>()
                .ForMember(prizePool => prizePool.Type, config => config.MapFrom(prizePool => prizePool.Type.ToEnum<EnumCurrencyType>()))
                .ForMember(prizePool => prizePool.Amount, config => config.MapFrom<DecimalValue>(prizePool => prizePool.Amount));
        }
    }
}
