// Filename: ChallengeClosedIntegrationEventHandler.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeClosedIntegrationEventHandler : IIntegrationEventHandler<ChallengeClosedIntegrationEvent>
    {
        private readonly IAccountService _transactionService;
        private readonly IChallengeQuery _challengeQuery;

        public ChallengeClosedIntegrationEventHandler(IAccountService transactionService, IChallengeQuery challengeQuery)
        {
            _transactionService = transactionService;
            _challengeQuery = challengeQuery;
        }

        public async Task HandleAsync(ChallengeClosedIntegrationEvent integrationEvent)
        {
            var challenge = await _challengeQuery.FindChallengeAsync(ChallengeId.Parse(integrationEvent.ChallengeId));

            await _transactionService.PayoutChallengeAsync(
                challenge,
                integrationEvent.Scoreboard.ToDictionary(x => x.Key.ParseEntityId<UserId>(), x => (decimal?) x.Value)); // TODO
        }
    }
}
