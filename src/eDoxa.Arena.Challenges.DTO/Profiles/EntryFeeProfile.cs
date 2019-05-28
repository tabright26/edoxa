// Filename: EntryFeeProfile.cs
// Date Created: 2019-05-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Domain;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class EntryFeeProfile : Profile
    {
        public EntryFeeProfile()
        {
            this.CreateMap<EntryFee, EntryFeeDTO>()
                .ForMember(entryFee => entryFee.Amount, config => config.MapFrom(entryFee => entryFee.Amount))
                .ForMember(entryFee => entryFee.Currency, config => config.MapFrom(entryFee => entryFee.Currency))
                .ReverseMap()
                .ForMember(entryFee => entryFee.Amount, config => config.MapFrom(entryFee => entryFee.Amount))
                .ForMember(entryFee => entryFee.Currency, config => config.MapFrom(entryFee => entryFee.Currency));
        }
    }
}
