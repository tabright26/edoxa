﻿// Filename: IUserManager.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.Services
{
    public interface IUserService
    {
        bool SupportsUserSecurityStamp { get; }

        bool SupportsUserRole { get; }

        bool SupportsUserEmail { get; }

        bool SupportsUserPhoneNumber { get; }

        bool SupportsUserClaim { get; }

        bool SupportsUserLockout { get; }

        bool SupportsQueryableUsers { get; }

        IQueryable<User> Users { get; }

        Task<Country> GetCountryAsync(User user);

        Task<UserProfile?> GetProfileAsync(User user);

        Task<UserDob> GetDobAsync(User user);

        Task<string?> GetFirstNameAsync(User user);

        Task<string?> GetLastNameAsync(User user);

        Task<Gender?> GetGenderAsync(User user);

        void Dispose();

        string GetUserName(ClaimsPrincipal principal);

        string GetUserId(ClaimsPrincipal principal);

        Task<User> GetUserAsync(ClaimsPrincipal principal);

        Task<string> GenerateConcurrencyStampAsync(User user);

        Task<IdentityResult> CreateAsync(User user);

        Task<IdentityResult> UpdateAsync(User user);

        Task<IdentityResult> DeleteAsync(User user);

        Task<User> FindByIdAsync(string userId);

        Task<User> FindByNameAsync(string userName);

        Task<IdentityResult> CreateAsync(User user, string password);

        Task UpdateNormalizedUserNameAsync(User user);

        Task<string> GetUserNameAsync(User user);

        Task<IdentityResult> SetUserNameAsync(User user, string userName);

        Task<string> GetUserIdAsync(User user);

        Task<bool> CheckPasswordAsync(User user, string password);

        Task<bool> HasPasswordAsync(User user);

        Task<IdentityResult> AddPasswordAsync(User user, string password);

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        Task<IdentityResult> RemovePasswordAsync(User user);

        Task<string> GetSecurityStampAsync(User user);

        Task<IdentityResult> UpdateSecurityStampAsync(User user);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);

        Task<User> FindByLoginAsync(string loginProvider, string providerKey);

        Task<IdentityResult> RemoveLoginAsync(User user, string loginProvider, string providerKey);

        Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo login);

        Task<IList<UserLoginInfo>> GetLoginsAsync(User user);

        Task<IdentityResult> AddClaimAsync(User user, Claim claim);

        Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims);

        Task<IdentityResult> ReplaceClaimAsync(User user, Claim claim, Claim newClaim);

        Task<IdentityResult> RemoveClaimAsync(User user, Claim claim);

        Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims);

        Task<IList<Claim>> GetClaimsAsync(User user);

        Task<IdentityResult> AddToRoleAsync(User user, string role);

        Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);

        Task<IdentityResult> RemoveFromRoleAsync(User user, string role);

        Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);

        Task<IList<string>> GetRolesAsync(User user);

        Task<bool> IsInRoleAsync(User user, string role);

        Task<string> GetEmailAsync(User user);

        Task<IdentityResult> UpdateEmailAsync(User user, string email);

        Task<User> FindByEmailAsync(string email);

        Task UpdateNormalizedEmailAsync(User user);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<bool> IsEmailConfirmedAsync(User user);

        Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);

        Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);

        Task<string> GetPhoneNumberAsync(User user);

        Task<IdentityResult> UpdatePhoneNumberAsync(User user, string phoneNumber);

        Task<IdentityResult> ChangePhoneNumberAsync(User user, string phoneNumber, string token);

        Task<bool> IsPhoneNumberConfirmedAsync(User user);

        Task<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber);

        Task<bool> VerifyChangePhoneNumberTokenAsync(User user, string token, string phoneNumber);

        Task<bool> VerifyUserTokenAsync(
            User user,
            string tokenProvider,
            string purpose,
            string token
        );

        Task<string> GenerateUserTokenAsync(User user, string tokenProvider, string purpose);

        void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<User> provider);

        Task<IList<string>> GetValidTwoFactorProvidersAsync(User user);

        Task<bool> VerifyTwoFactorTokenAsync(User user, string tokenProvider, string token);

        Task<string> GenerateTwoFactorTokenAsync(User user, string tokenProvider);

        Task<bool> GetTwoFactorEnabledAsync(User user);

        Task<IdentityResult> SetTwoFactorEnabledAsync(User user, bool enabled);

        Task<bool> IsLockedOutAsync(User user);

        Task<IdentityResult> SetLockoutEnabledAsync(User user, bool enabled);

        Task<bool> GetLockoutEnabledAsync(User user);

        Task<DateTimeOffset?> GetLockoutEndDateAsync(User user);

        Task<IdentityResult> SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd);

        Task<IdentityResult> AccessFailedAsync(User user);

        Task<IdentityResult> ResetAccessFailedCountAsync(User user);

        Task<int> GetAccessFailedCountAsync(User user);

        Task<IList<User>> GetUsersForClaimAsync(Claim claim);

        Task<IList<User>> GetUsersInRoleAsync(string roleName);

        Task<string> GetAuthenticationTokenAsync(User user, string loginProvider, string tokenName);

        Task<IdentityResult> SetAuthenticationTokenAsync(
            User user,
            string loginProvider,
            string tokenName,
            string tokenValue
        );

        Task<DomainValidationResult<UserProfile>> CreateProfileAsync(
            User user,
            string firstName,
            string lastName,
            Gender gender
        );

        Task<DomainValidationResult<UserProfile>> UpdateProfileAsync(User user, string firstName);
    }
}
