// Filename: DoxatagService.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Application.Services
{
    public sealed class DoxatagService : IDoxatagService
    {
        private readonly IDoxatagRepository _doxatagRepository;

        public DoxatagService(IDoxatagRepository doxatagRepository)
        {
            _doxatagRepository = doxatagRepository;
        }

        public async Task<IReadOnlyCollection<Doxatag>> FetchDoxatagsAsync()
        {
            return await _doxatagRepository.FetchDoxatagsAsync();
        }

        public async Task<Doxatag?> FindDoxatagAsync(User user)
        {
            return await _doxatagRepository.FindDoxatagAsync(UserId.FromGuid(user.Id));
        }

        public async Task<IReadOnlyCollection<Doxatag>> FetchDoxatagHistoryAsync(User user)
        {
            return await _doxatagRepository.FetchDoxatagHistoryAsync(UserId.FromGuid(user.Id));
        }

        public async Task<IdentityResult> ChangeDoxatagAsync(User user, string doxatagName)
        {
            var codes = await _doxatagRepository.FetchDoxatagCodesByNameAsync(doxatagName);

            var code = Doxatag.GenerateUniqueCode(codes);

            var doxatag = new Doxatag(
                UserId.FromGuid(user.Id),
                doxatagName,
                code,
                new UtcNowDateTimeProvider());

            _doxatagRepository.Create(doxatag);

            await _doxatagRepository.UnitOfWork.CommitAsync();

            //await this.UpdateSecurityStampAsync(user);

            //return await this.UpdateUserAsync(user);

            return IdentityResult.Success;
        }
    }
}
