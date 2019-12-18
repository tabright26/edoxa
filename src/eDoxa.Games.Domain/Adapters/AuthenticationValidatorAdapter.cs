using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Newtonsoft.Json;

namespace eDoxa.Games.Domain.Adapters
{
    public abstract class AuthenticationValidatorAdapter<TAuthentication> : IAuthenticationValidatorAdapter<TAuthentication>
    where TAuthentication : GameAuthentication
    {
        public abstract Game Game { get; }

        public abstract Task<IDomainValidationResult> ValidateAuthenticationAsync(UserId userId, TAuthentication authentication);

        public async Task<IDomainValidationResult> ValidateAuthenticationAsync(UserId userId, GameAuthentication gameAuthentication)
        {
            return await this.ValidateAuthenticationAsync(userId, JsonConvert.DeserializeObject<TAuthentication>(JsonConvert.SerializeObject(gameAuthentication)));
        }
    }
}
