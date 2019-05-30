// Filename: AccountsController.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.DTO.Queries;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/accounts")]
    [ApiExplorerSettings(GroupName = "Accounts")]
    public sealed class AccountsController : ControllerBase
    {
        private readonly IAccountQueries _accountQueries;

        public AccountsController(IAccountQueries accountQueries)
        {
            _accountQueries = accountQueries;
        }

        /// <summary>
        ///     Get account by currency.
        /// </summary>
        [HttpGet("{currency}", Name = nameof(GetAccountAsync))]
        public async Task<IActionResult> GetAccountAsync(CurrencyType currencyType)
        {
            var account = await _accountQueries.GetAccountAsync(currencyType);

            return account.Select(this.Ok).Cast<IActionResult>().DefaultIfEmpty(this.NotFound("User money account not found.")).Single();
        }
    }
}
