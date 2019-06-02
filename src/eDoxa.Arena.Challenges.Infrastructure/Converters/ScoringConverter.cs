// Filename: ChallengeScoringConverter.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Infrastructure.Converters
{
    //internal sealed class ScoringConverter : ValueConverter<IScoring, string>
    //{
    //    private static readonly IMapper Mapper = new Mapper(
    //        new MapperConfiguration(
    //            config =>
    //            {
    //                config.CreateMap<StatName, string>().ConvertUsing(name => name.ToString());
    //                config.CreateMap<StatWeighting, float>().ConvertUsing(weighting => Convert.ToSingle(weighting));
    //                config.CreateMap<IDictionary<string, float>, Scoring>().ConstructUsing(dictionary => new Scoring(dictionary));
    //            }
    //        )
    //    );

    //    public ScoringConverter() : base(
    //        scoring => JsonConvert.SerializeObject(Mapper.Map<Dictionary<string, float>>(scoring), Formatting.None),
    //        scoring => Mapper.Map<Scoring>(JsonConvert.DeserializeObject<Dictionary<string, float>>(scoring))
    //    )
    //    {
    //    }
    //}
}
