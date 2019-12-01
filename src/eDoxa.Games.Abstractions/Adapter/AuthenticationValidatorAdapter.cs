using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain.Misc;

using FluentValidation.Results;

using Newtonsoft.Json;

namespace eDoxa.Games.Abstractions.Adapter
{
    public abstract class AuthenticationValidatorAdapter<TAuthentication> : IAuthenticationValidatorAdapter<TAuthentication>
    where TAuthentication : GameAuthentication
    {
        public abstract Game Game { get; }

        public abstract Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, TAuthentication authentication);

        public async Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, GameAuthentication gameAuthentication)
        {
            return await this.ValidateAuthenticationAsync(userId, JsonConvert.DeserializeObject<TAuthentication>(JsonConvert.SerializeObject(gameAuthentication)));
        }
    }
}
