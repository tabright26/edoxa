// Filename: ClanService.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

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
            if (await _clanRepository.IsMemberAsync(userId))
            {
                return new ValidationFailure(string.Empty, "User already in a clan.").ToResult();
            }

            if (await _clanRepository.ExistsAsync(name))
            {
                return new ValidationFailure("_error", "Clan with the same name already exist").ToResult();
            }

            var clan = new Clan(name, userId);

            _clanRepository.Create(clan);

            await _clanRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
        }

        public async Task<Stream> DownloadLogoAsync(Clan clan)
        {
            return await _clanRepository.DownloadLogoAsync(clan.Id);
        }

        public async Task<ValidationResult> UploadLogoAsync(Clan clan, UserId userId, IFormFile logo)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            await _clanRepository.UploadLogoAsync(clan.Id, logo);

            return new ValidationResult();
        }

        public async Task DeleteLogoAsync(ClanId clanId)
        {
            await _clanRepository.DeleteLogoAsync(clanId);
        }

        public async Task<IReadOnlyCollection<Member>> FetchMembersAsync(Clan clan)
        {
            return await _clanRepository.FetchMembersAsync(clan.Id);
        }

        public async Task<Member?> FindMemberAsync(Clan clan, MemberId memberId)
        {
            return await _clanRepository.FindMemberAsync(clan.Id, memberId);
        }

        public async Task AddMemberToClanAsync(ClanId clanId, IMemberInfo memberInfo)
        {
            var clan = await _clanRepository.FindClanAsync(clanId) ?? throw new InvalidOperationException(nameof(this.AddMemberToClanAsync));

            clan.AddMember(memberInfo);

            await _clanRepository.UnitOfWork.CommitAsync();
        }

        public async Task<ValidationResult> KickMemberFromClanAsync(UserId userId, Clan clan, MemberId memberId)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            if (!clan.HasMember(memberId))
            {
                return new ValidationFailure(string.Empty, $"The memberId ({memberId}) isn't a member of the clan '{clan.Name}'.").ToResult();
            }

            var member = clan.FindMember(memberId);

            clan.Kick(member);

            await _clanRepository.UnitOfWork.CommitAsync();

            return new ValidationResult();
        }

        public async Task<ValidationResult> LeaveClanAsync(Clan clan, UserId userId)
        {
            if (!clan.HasMember(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't a member of the clan '{clan.Name}'.").ToResult();
            }

            var member = clan.FindMember(userId);

            clan.Leave(member);

            await _clanRepository.UnitOfWork.CommitAsync();

            if (clan.Deleted)
            {
                await this.DeleteClanAsync(clan);
            }

            return new ValidationResult();
        }

        public async Task<bool> IsMemberAsync(UserId userId)
        {
            return await _clanRepository.IsMemberAsync(userId);
        }

        private async Task DeleteClanAsync(Clan clan)
        {
            _clanRepository.Delete(clan);

            await _clanRepository.UnitOfWork.CommitAsync();
        }
    }
}
