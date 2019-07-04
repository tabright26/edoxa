// Filename: AccountBalanceController.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Seedwork.Common.Enumerations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/account/balance")]
    [ApiExplorerSettings(GroupName = "Account")]
    public sealed class AccountBalanceController : ControllerBase
    {
        private readonly IAccountQuery _accountQuery;

        public AccountBalanceController(IAccountQuery accountQuery)
        {
            _accountQuery = accountQuery;
        }

        /// <summary>
        ///     Get account balance by currency.
        /// </summary>
        [HttpGet("{currency}")]
        public async Task<IActionResult> GetByCurrencyAsync(CurrencyType currency)
        {
            var balanceViewModel = await _accountQuery.FindUserBalanceViewModelAsync(currency);

            if (balanceViewModel == null)
            {
                return this.NotFound($"Account balance for currency {currency} not found.");
            }

            return this.Ok(balanceViewModel);
        }
    }
}
