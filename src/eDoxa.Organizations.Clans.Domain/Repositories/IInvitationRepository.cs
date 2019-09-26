using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;

namespace eDoxa.Organizations.Clans.Domain.Repositories
{
    public interface IInvitationRepository
    {
        void Create(Invitation candidature);

        void Delete(Invitation candidature);

        Task<IReadOnlyCollection<Invitation>> FetchAsync();

        Task<Invitation?> FindAsync(InvitationId invitationId);

        Task CommitAsync();
    }
}
