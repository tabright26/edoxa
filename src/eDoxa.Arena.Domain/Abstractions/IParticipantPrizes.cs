using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Arena.Domain.Abstractions
{
    public interface IParticipantPrizes : IReadOnlyDictionary<UserId, Prize>
    {
    }
}
