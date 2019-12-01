// Filename: AuthFactorGeneratorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;

using FluentValidation.Results;

using Newtonsoft.Json;

namespace eDoxa.Games.Abstractions.Adapter
{
    public abstract class AuthenticationGeneratorAdapter<TRequest> : IAuthenticationGeneratorAdapter<TRequest>
    where TRequest : class
    {
        public abstract Game Game { get; }

        public abstract Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, TRequest request);

        public async Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, object request)
        {
            return await this.GenerateAuthenticationAsync(userId, JsonConvert.DeserializeObject<TRequest>(request.ToString()!));
        }
    }
}
