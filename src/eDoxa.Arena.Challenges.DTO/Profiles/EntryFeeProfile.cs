// Filename: EntryFeeProfile.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Domain.ValueObjects;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class EntryFeeProfile : Profile
    {
        public EntryFeeProfile()
        {
            this.CreateMap<EntryFee, EntryFeeDTO>()
                .ForMember(entryFee => entryFee.Amount, config => config.MapFrom(entryFee => entryFee.Amount))
                .ForMember(entryFee => entryFee.Type, config => config.MapFrom(entryFee => entryFee.Type))
                .ReverseMap()
                .ForMember(entryFee => entryFee.Amount, config => config.MapFrom(entryFee => entryFee.Amount))
                .ForMember(entryFee => entryFee.Type, config => config.MapFrom(entryFee => entryFee.Type));
        }
    }
}
