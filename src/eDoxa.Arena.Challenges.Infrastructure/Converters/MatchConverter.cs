using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Converters
{
    internal sealed class MatchConverter : IMemberValueResolver<Participant, ParticipantModel, IReadOnlyCollection<Match>, ICollection<MatchModel>>
    {
        [NotNull]
        public ICollection<MatchModel> Resolve(
            [NotNull] Participant source,
            [NotNull] ParticipantModel destination,
            [NotNull] IReadOnlyCollection<Match> sourceMember,
            [NotNull] ICollection<MatchModel> destMember,
            [NotNull] ResolutionContext context
        )
        {
            var matches = context.Mapper.Map<ICollection<MatchModel>>(sourceMember);

            matches.ForEach(match => match.Participant = destination);

            return matches;
        }
    }
}
