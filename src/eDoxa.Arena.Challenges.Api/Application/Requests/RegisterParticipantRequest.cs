// Filename: RegisterParticipantRequest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using MediatR;

namespace eDoxa.Arena.Challenges.Api.Application.Requests
{
    [DataContract]
    public sealed class RegisterParticipantRequest : IRequest
    {
        public RegisterParticipantRequest(ChallengeId challengeId) : this()
        {
            ChallengeId = challengeId;
        }

        public RegisterParticipantRequest()
        {
            // Required for unit tests.
        }

        [IgnoreDataMember] public ChallengeId ChallengeId { get; private set; }
    }
}
