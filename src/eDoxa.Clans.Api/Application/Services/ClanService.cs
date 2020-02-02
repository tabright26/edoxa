// Filename: ClanService.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Api.Application.Services
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

        public async Task<DomainValidationResult<Clan>> CreateClanAsync(UserId userId, string name)
        {
            var result = new DomainValidationResult<Clan>();

            if (await _clanRepository.IsMemberAsync(userId))
            {
                result.AddFailedPreconditionError("User already in a clan.");
            }

            if (await _clanRepository.ExistsAsync(name))
            {
                result.AddFailedPreconditionError("Clan with the same name already exist");
            }

            if (result.IsValid)
            {
                var clan = new Clan(name, userId);

                _clanRepository.Create(clan);

                await _clanRepository.UnitOfWork.CommitAsync();

                return clan;
            }

            return result;
        }

        public async Task<DomainValidationResult<Clan>> UpdateClanAsync(Clan clan, UserId userId, string? summary)
        {
            var result = new DomainValidationResult<Clan>();

            if (!clan.MemberIsOwner(userId))
            {
                result.AddFailedPreconditionError($"The user ({userId}) isn't the clan owner.");
            }

            if (result.IsValid)
            {
                clan.Update(summary);

                await _clanRepository.UnitOfWork.CommitAsync();

                return clan;
            }
            
            return result;
        }

        public async Task<Stream> DownloadLogoAsync(Clan clan)
        {
            return await _clanRepository.DownloadLogoAsync(clan.Id);
        }

        public async Task<DomainValidationResult<object>> UploadLogoAsync(
            Clan clan,
            UserId userId,
            Stream stream,
            string fileName
        )
        {
            if (!clan.MemberIsOwner(userId))
            {
                return DomainValidationResult<object>.Failure($"The user ({userId}) isn't the clan owner.");
            }

            await _clanRepository.UploadLogoAsync(clan.Id, stream, fileName);

            return new DomainValidationResult<object>();
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

        public async Task<DomainValidationResult<Member>> KickMemberFromClanAsync(Clan clan, UserId userId, MemberId memberId)
        {
            var result = new DomainValidationResult<Member>();

            if (!clan.MemberIsOwner(userId))
            {
                result.AddFailedPreconditionError($"The user ({userId}) isn't the clan owner.");
            }

            if (!clan.HasMember(memberId))
            {
                result.AddFailedPreconditionError($"The memberId ({memberId}) isn't a member of the clan '{clan.Name}'.");
            }

            if (result.IsValid)
            {
                var member = clan.FindMember(memberId);

                clan.Kick(member);

                await _clanRepository.UnitOfWork.CommitAsync();

                return member;
            }

            return result;
        }

        public async Task<DomainValidationResult<Clan>> LeaveClanAsync(Clan clan, UserId userId)
        {
            var result = new DomainValidationResult<Clan>();

            if (!clan.HasMember(userId))
            {
                result.AddFailedPreconditionError($"The user ({userId}) isn't a member of the clan '{clan.Name}'.");
            }

            if (result.IsValid)
            {
                var member = clan.FindMember(userId);

                clan.Leave(member);

                await _clanRepository.UnitOfWork.CommitAsync();

                if (clan.Deleted)
                {
                    await this.DeleteClanAsync(clan);
                }

                return clan;
            }

            return result;
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

        public async Task<DomainValidationResult<Division>> CreateDivisionAsync(
            Clan clan,
            UserId userId,
            string name,
            string description
        )
        {
            var result = new DomainValidationResult<Division>();

            if (!clan.MemberIsOwner(userId))
            {
                result.AddFailedPreconditionError($"The user ({userId}) isn't the clan owner.");
            }

            if (result.IsValid)
            {
                var division = new Division(clan.Id, name, description);

                clan.CreateDivision(division);

                await _clanRepository.UnitOfWork.CommitAsync();

                return division;
            }

            return result;
        }

        public async Task<DomainValidationResult<Division>> DeleteDivisionAsync(Clan clan, UserId userId, DivisionId divisionId)
        {
            var result = new DomainValidationResult<Division>();

            if (!clan.MemberIsOwner(userId))
            {
                result.AddFailedPreconditionError($"The user ({userId}) isn't the clan owner.");
            }

            if (result.IsValid)
            {
                var division = clan.RemoveDivision(divisionId);

                await _clanRepository.UnitOfWork.CommitAsync();

                return division;
            }
            
            return result;
        }

        public async Task<DomainValidationResult<Division>> UpdateDivisionAsync(
            Clan clan,
            UserId userId,
            DivisionId divisionId,
            string name,
            string description
        )
        {
            if (!clan.MemberIsOwner(userId))
            {
                return DomainValidationResult<Division>.Failure($"The user ({userId}) isn't the clan owner.");
            }

            clan.UpdateDivision(divisionId, name, description);

            await _clanRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult<Division>();
        }

        public async Task<DomainValidationResult<Member>> AddMemberToDivisionAsync(
            Clan clan,
            UserId userId,
            DivisionId divisionId,
            MemberId memberId
        )
        {
            if (!clan.MemberIsOwner(userId))
            {
                return DomainValidationResult<Member>.Failure($"The user ({userId}) isn't the clan owner.");
            }

            clan.AddMemberToDivision(divisionId, memberId);
            await _clanRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult<Member>();
        }

        public async Task<DomainValidationResult<Member>> RemoveMemberFromDivisionAsync(
            Clan clan,
            UserId userId,
            DivisionId divisionId,
            MemberId memberId
        )
        {
            if (!clan.MemberIsOwner(userId))
            {
                return DomainValidationResult<Member>.Failure($"The user ({userId}) isn't the clan owner.");
            }

            clan.RemoveMemberFromDivision(divisionId, memberId);

            await _clanRepository.UnitOfWork.CommitAsync();

            return new DomainValidationResult<Member>();
        }

        private async Task DeleteClanAsync(Clan clan)
        {
            _clanRepository.Delete(clan);

            await _clanRepository.UnitOfWork.CommitAsync();
        }
    }
}
