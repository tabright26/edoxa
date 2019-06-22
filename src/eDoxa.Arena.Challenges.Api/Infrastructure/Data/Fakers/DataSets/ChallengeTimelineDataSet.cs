using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.DataSets
{
    public class ChallengeTimelineDataSet : DataSet
    {
        public ChallengeTimelineDataSet(string local = "en") : base(local)
        {
        }

        public ChallengeDuration Duration()
        {
            return new ChallengeDuration(Random.CollectionItem(ValueObject.GetValues<ChallengeDuration>().ToList()));
        }
    }
}
