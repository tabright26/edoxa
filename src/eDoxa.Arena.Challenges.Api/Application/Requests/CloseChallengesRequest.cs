// Filename: CloseChallengesRequest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using MediatR;

namespace eDoxa.Arena.Challenges.Api.Application.Requests
{
    [DataContract]
    public sealed class CloseChallengesRequest : IRequest
    {
        public CloseChallengesRequest(ChallengeId challengeId)
        {
            ChallengeId = challengeId;
        }

#nullable disable
        public CloseChallengesRequest()
        {
            // Required by Fluent Validation
        }
#nullable restore

        [IgnoreDataMember]
        public ChallengeId ChallengeId { get; private set; }
    }
}
