// Filename: UsersController.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Application.Managers;
using eDoxa.Identity.Api.Areas.Users.ViewModels;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Areas.Users.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users")]
    [ApiExplorerSettings(GroupName = "User")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly CustomUserManager _userManager;
        private readonly IMapper _mapper;

        public UsersController(CustomUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find users.
        /// </summary>
        [HttpGet(Name = nameof(FindUsersAsync))]
        public async Task<IActionResult> FindUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            if (!users.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }
    }
}
