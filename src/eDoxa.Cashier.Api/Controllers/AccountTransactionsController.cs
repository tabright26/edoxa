// Filename: AccountTransactionsController.cs
// Date Created: 2019-05-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/accounts/{currency}/transactions")]
    [ApiExplorerSettings(GroupName = "Accounts")]
    public class AccountTransactionsController : ControllerBase
    {
        private readonly ITransactionQueries _transactionQueries;

        public AccountTransactionsController(ITransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }

        /// <summary>
        ///     Get transactions by account currency.
        /// </summary>
        [HttpGet(Name = nameof(GetTransactionsAsync))]
        public async Task<IActionResult> GetTransactionsAsync(AccountCurrency currency)
        {
            var transactions = await _transactionQueries.GetTransactionsAsync(currency);

            if (!transactions.Any())
            {
                return this.NoContent();
            }

            return this.Ok(transactions);
        }
    }
}