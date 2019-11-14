using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using Newtonsoft.Json;

namespace eDoxa.Games.Abstractions.Adapter
{
    public abstract class AuthenticationValidatorAdapter<TAuthentication> : IAuthenticationValidatorAdapter<TAuthentication>
    where TAuthentication : Authentication
    {
        public abstract Game Game { get; }

        public abstract Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, TAuthentication authentication);

        public async Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, Authentication authentication)
        {
            return await this.ValidateAuthenticationAsync(userId, JsonConvert.DeserializeObject<TAuthentication>(JsonConvert.SerializeObject(authentication)));
        }
    }
}
