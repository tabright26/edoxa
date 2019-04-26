// Filename: UserTokenAccountController.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users/{userId}/token-account")]
    public class UserTokenAccountController : ControllerBase
    {
        private readonly ILogger<UserTokenAccountController> _logger;
        private readonly ITokenAccountQueries _queries;

        public UserTokenAccountController(ILogger<UserTokenAccountController> logger, ITokenAccountQueries queries)
        {
            _logger = logger;
            _queries = queries;
        }

        /// <summary>
        ///     Find a user's token account.
        /// </summary>
        [HttpGet(Name = nameof(FindTokenAccountAsync))]
        public async Task<IActionResult> FindTokenAccountAsync(UserId userId)
        {
            try
            {
                var account = await _queries.FindTokenAccountAsync(userId);

                if (account == null)
                {
                    return this.NotFound(string.Empty);
                }

                return this.Ok(account);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}