using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
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
            var challenge = await _challengeQuery.FindChallengeAsync(integrationEvent.ChallengeId);
            
            await _transactionService.PayoutChallengeAsync(challenge, integrationEvent.Scoreboard);
        }
    }
}
