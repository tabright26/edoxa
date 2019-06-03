﻿// Filename: ScoreboardProfile.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class ScoreboardProfile : Profile
    {
        public ScoreboardProfile()
        {
            this.CreateMap<Scoreboard, ScoreboardDTO>().ConstructUsing(scoreboard => new ScoreboardDTO(scoreboard));
        }
    }
}