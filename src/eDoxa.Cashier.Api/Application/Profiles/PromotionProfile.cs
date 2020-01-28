// Filename: PromotionProfile.cs
// Date Created: 2020-01-21
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class PromotionProfile : Profile
    {
        public PromotionProfile()
        {
            this.CreateMap<Promotion, PromotionDto>()
                .ForMember(promotion => promotion.PromotionalCode, config => config.MapFrom(promotion => promotion.PromotionalCode))
                .ForMember(promotion => promotion.Amount, config => config.MapFrom<DecimalValue>(promotion => promotion.Amount))
                .ForMember(promotion => promotion.Currency, config => config.MapFrom(promotion => promotion.Currency.ToEnum<EnumCurrency>()))
                .ForMember(promotion => promotion.Expired, config => config.MapFrom(promotion => promotion.IsExpired()))
                .ForMember(promotion => promotion.Canceled, config => config.MapFrom(promotion => promotion.IsCanceled()))
                .ForMember(promotion => promotion.Active, config => config.MapFrom(promotion => promotion.IsActive()));
        }
    }
}
