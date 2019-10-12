using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Organizations.Clans.Domain.Repositories
{
    public interface IInvitationRepository
    {
        IUnitOfWork UnitOfWork { get; }

        void Create(Invitation invitation);

        void Delete(Invitation invitation);

        Task<IReadOnlyCollection<Invitation>> FetchAsync(UserId userId);

        Task<IReadOnlyCollection<Invitation>> FetchAsync(ClanId clanId);

        Task<Invitation?> FindAsync(InvitationId invitationId);

        Task<bool> ExistsAsync(UserId userId, ClanId clanId);
    }
}
