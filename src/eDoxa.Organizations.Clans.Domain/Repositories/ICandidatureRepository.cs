using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;

namespace eDoxa.Organizations.Clans.Domain.Repositories
{
    public interface ICandidatureRepository
    {
        void Create(Candidature candidature);

        void Delete(Candidature candidature);

        Task<IReadOnlyCollection<Candidature>> FetchAsync();

        Task<Candidature?> FindAsync(CandidatureId candidatureId);

        Task<bool> ExistsAsync(UserId userId, ClanId clanId);

        Task DeleteAllWith(UserId userId);

        Task CommitAsync();
    }
}
