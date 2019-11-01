// Filename: AuthFactorGeneratorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using Newtonsoft.Json;

namespace eDoxa.Arena.Games.Abstractions.Adapter
{
    public abstract class AuthFactorGeneratorAdapter<TRequest> : IAuthFactorGeneratorAdapter<TRequest>
    where TRequest : class
    {
        public abstract Game Game { get; }

        public abstract Task<ValidationResult> GenerateAuthFactorAsync(UserId userId, TRequest request);

        public async Task<ValidationResult> GenerateAuthFactorAsync(UserId userId, object request)
        {
            return await this.GenerateAuthFactorAsync(userId, JsonConvert.DeserializeObject<TRequest>(request.ToString()));
        }
    }
}
