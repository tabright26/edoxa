// Filename: UsersController.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Abstractions.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users")]
    [ApiExplorerSettings(GroupName = "Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserQuery _userQuery;

        public UsersController(IUserQuery userQuery)
        {
            _userQuery = userQuery;
        }

        /// <summary>
        ///     Find users.
        /// </summary>
        [HttpGet(Name = nameof(FindUsersAsync))]
        public async Task<IActionResult> FindUsersAsync()
        {
            var users = await _userQuery.FindUsersAsync();

            return users
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}