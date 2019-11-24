// Filename: ClanService.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Clans.Api.Areas.Clans.Services
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

        public async Task<ValidationResult> UpdateClanAsync(Clan clan, UserId userId, string? summary)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            clan.Update(summary);
            await _clanRepository.UnitOfWork.CommitAsync();
            return new ValidationResult();
        }

        private async Task DeleteClanAsync(Clan clan)
        {
            _clanRepository.Delete(clan);
            await _clanRepository.UnitOfWork.CommitAsync();
        }

        public async Task<Stream> DownloadLogoAsync(Clan clan)
        {
            return await _clanRepository.DownloadLogoAsync(clan.Id);
        }

        public async Task<ValidationResult> UploadLogoAsync(Clan clan, UserId userId, Stream stream, string fileName)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            await _clanRepository.UploadLogoAsync(clan.Id, stream, fileName);

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

        public async Task<ValidationResult> KickMemberFromClanAsync(Clan clan, UserId userId, MemberId memberId)
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

        public async Task<IReadOnlyCollection<Division>> FetchDivisionsAsync(ClanId clanId)
        {
            return await _clanRepository.FetchDivisionsAsync(clanId);
        }

        public async Task<IReadOnlyCollection<Member>> FetchDivisionMembersAsync(DivisionId divisionId)
        {
            var division = await _clanRepository.FindDivisionAsync(divisionId);

            if (division == null)
            {
                return new List<Member>();
            }

            return division.Members.ToList();
        }

        public async Task<ValidationResult> CreateDivisionAsync(Clan clan, UserId userId, string name, string description)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            clan.CreateDivision(name, description);
            await _clanRepository.UnitOfWork.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> DeleteDivisionAsync(Clan clan, UserId userId, DivisionId divisionId)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            clan.RemoveDivision(divisionId);
            await _clanRepository.UnitOfWork.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> UpdateDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, string name, string description)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            clan.UpdateDivision(divisionId, name, description);
            await _clanRepository.UnitOfWork.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> AddMemberToDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, MemberId memberId)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            clan.AddMemberToDivision(divisionId, memberId);
            await _clanRepository.UnitOfWork.CommitAsync();
            return new ValidationResult();
        }

        public async Task<ValidationResult> RemoveMemberFromDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, MemberId memberId)
        {
            if (!clan.MemberIsOwner(userId))
            {
                return new ValidationFailure(string.Empty, $"The user ({userId}) isn't the clan owner.").ToResult();
            }

            clan.RemoveMemberFromDivision(divisionId, memberId);
            await _clanRepository.UnitOfWork.CommitAsync();
            return new ValidationResult();
        }

    }
}
