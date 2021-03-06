﻿// Filename: DoxatagProfile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;

namespace eDoxa.Identity.Api.Application.Profiles
{
    internal sealed class DoxatagProfile : Profile
    {
        public DoxatagProfile()
        {
            this.CreateMap<Doxatag, DoxatagDto>()
                .ForMember(doxatag => doxatag.UserId, config => config.MapFrom(doxatag => doxatag.UserId.ToString()))
                .ForMember(doxatag => doxatag.Name, config => config.MapFrom(doxatag => doxatag.Name))
                .ForMember(doxatag => doxatag.Code, config => config.MapFrom(doxatag => doxatag.Code))
                .ForMember(doxatag => doxatag.Timestamp, config => config.MapFrom(doxatag => doxatag.Timestamp.ToTimestampUtc()));
        }
    }
}
