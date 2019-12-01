using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Domain.Repositories
{
    public interface ICandidatureRepository
    { 
        IUnitOfWork UnitOfWork { get; }

        void Create(Candidature candidature);

        void Delete(Candidature candidature);

        Task<IReadOnlyCollection<Candidature>> FetchAsync(ClanId clanId);

        Task<IReadOnlyCollection<Candidature>> FetchAsync(UserId userId);

        Task<Candidature?> FindAsync(CandidatureId candidatureId);

        Task<bool> ExistsAsync(UserId userId, ClanId clanId);
    }
}
