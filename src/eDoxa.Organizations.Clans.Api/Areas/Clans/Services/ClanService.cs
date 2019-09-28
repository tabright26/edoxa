// Filename: AccountService.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.ServiceBus.Abstractions;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Services
{
    public sealed class ClanService : IClanService
    {
        private readonly IClanRepository _clanRepository;

        public ClanService(IClanRepository clanRepository)
        {
            _clanRepository = clanRepository;
        }

        public async Task<IReadOnlyCollection<Clan>> FetchClansAsync()
        {
            return await _clanRepository.FetchClansAsync();
        }

        public async Task<Clan?> FindClanAsync(ClanId clanId)
        {
            return await _clanRepository.FindClanAsync(clanId);
        }

        public async Task<ValidationResult> CreateClanAsync(UserId userId, string name)
        {
            var clans = await _clanRepository.FetchClansAsync();

            if (await this.HasMember(userId))
            {
                var failure = new ValidationFailure(string.Empty, "User already in a clan.");

                return failure.ToResult();
            }

            if (clans.Any(clan => clan.Name == name))
            {
                var failure = new ValidationFailure(string.Empty, "Clan with the same name already exist");

                return failure.ToResult();
            }

            _clanRepository.Create(new Clan(name, userId));
            await _clanRepository.CommitAsync();

            return new ValidationResult();
        }

        public Task<FileStream?> GetClanLogoAsync(ClanId clanId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> CreateOrUpdateClanLogoAsync(Clan clan, FileStream logo, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Member>> FetchMembersAsync(ClanId clanId)
        {
            return await _clanRepository.FetchMembersAsync(clanId);
        }

        public async Task<Member?> FindMemberAsync(Clan clan, MemberId memberId)
        {
            return await _clanRepository.FindMemberAsync(clan.Id, memberId);
        }

        public async Task<ValidationResult> KickMemberFromClanAsync(UserId userId, Clan clan, MemberId memberId)
        {
            if (!clan.IsOwner(userId)) //Is the admin of the clan.
            {
                var failure = new ValidationFailure(string.Empty, "Permission required.");

                return failure.ToResult();
            }

            if (!clan.RemoveNonOwnerMember(memberId)) //Clan specific rules
            {
                var failure = new ValidationFailure(string.Empty, "Member not in the clan or is owner.");

                return failure.ToResult();
            }

            await _clanRepository.CommitAsync();
            return new ValidationResult();
        }


        public async Task<ValidationResult> LeaveClanAsync(Clan clan, UserId userId)
        {
            clan.RemoveUser(userId);

            if (clan.IsEmpty())
            {
                _clanRepository.Delete(clan);
            }

            await _clanRepository.CommitAsync();
            return new ValidationResult();
        }

        public async Task<bool> HasMember(UserId userId)
        {
            return await _clanRepository.HasMember(userId);
        }
    }
}
