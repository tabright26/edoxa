// Filename: IdentityParserService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Security.Claims;

using eDoxa.Security;
using eDoxa.Seedwork.Domain.Common.ValueObjects;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Services
{
    public class IdentityParserParserService : IIdentityParserService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityParserParserService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Guid Subject()
        {
            return Guid.Parse(_context.HttpContext.User.FindFirstValue(JwtClaimTypes.Subject));
        }

        public string Email()
        {
            return _context.HttpContext.User.HasClaim(claim => claim.Type == JwtClaimTypes.Email) ?
                _context.HttpContext.User.FindFirstValue(JwtClaimTypes.Email) :
                null;
        }

        public string PhoneNumber()
        {
            return _context.HttpContext.User.HasClaim(claim => claim.Type == JwtClaimTypes.PhoneNumber) ?
                _context.HttpContext.User.FindFirstValue(JwtClaimTypes.PhoneNumber) :
                null;
        }

        public Name Name()
        {
            return _context.HttpContext.User.HasClaim(claim => claim.Type == JwtClaimTypes.GivenName) &&
                   _context.HttpContext.User.HasClaim(claim => claim.Type == JwtClaimTypes.FamilyName) ?
                new Name(
                    _context.HttpContext.User.FindFirstValue(JwtClaimTypes.GivenName),
                    _context.HttpContext.User.FindFirstValue(JwtClaimTypes.FamilyName)
                ) :
                null;
        }

        public BirthDate BirthDate()
        {
            return _context.HttpContext.User.HasClaim(claim => claim.Type == JwtClaimTypes.BirthDate) ?
                Domain.Common.ValueObjects.BirthDate.FromDate(DateTime.Parse(_context.HttpContext.User.FindFirstValue(JwtClaimTypes.BirthDate))) :
                null;
        }

        public Guid? GetClanId()
        {
            return _context.HttpContext.User.HasClaim(claim => claim.Type == CustomClaimTypes.UserClanIdClaimType) ?
                (Guid?) Guid.Parse(_context.HttpContext.User.FindFirstValue(CustomClaimTypes.UserClanIdClaimType)) :
                null;
        }

        public string GetCustomerId()
        {
            return _context.HttpContext.User.HasClaim(claim => claim.Type == CustomClaimTypes.UserCustomerIdClaimType) ?
                _context.HttpContext.User.FindFirstValue(CustomClaimTypes.UserCustomerIdClaimType) :
                null;
        }
    }
}