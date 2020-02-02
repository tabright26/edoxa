// Filename: AuthFactorGeneratorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Newtonsoft.Json;

namespace eDoxa.Games.Domain.Adapters
{
    public abstract class AuthenticationGeneratorAdapter<TRequest> : IAuthenticationGeneratorAdapter<TRequest>
    where TRequest : class
    {
        public abstract Game Game { get; }

        public abstract Task<DomainValidationResult<object>> GenerateAuthenticationAsync(UserId userId, TRequest request);

        public async Task<DomainValidationResult<object>> GenerateAuthenticationAsync(UserId userId, object request)
        {
            return await this.GenerateAuthenticationAsync(userId, JsonConvert.DeserializeObject<TRequest>(request.ToString()!));
        }
    }
}
