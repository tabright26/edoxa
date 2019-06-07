using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.DTO;

namespace eDoxa.Arena.Challenges.Services.LeagueOfLegends.Adapters
{
    public sealed class LeagueOfLegendsMatchReferencesAdapter : IMatchReferencesAdapter
    {
        private readonly LeagueOfLegendsMatchReferenceDTO[] _matchReferences;

        public LeagueOfLegendsMatchReferencesAdapter(LeagueOfLegendsMatchReferenceDTO[] matchReferences)
        {
            _matchReferences = matchReferences;
        }

        public IEnumerable<MatchReference> MatchReferences => _matchReferences.Select(matchReference => new MatchReference(matchReference.GameId));
    }
}
