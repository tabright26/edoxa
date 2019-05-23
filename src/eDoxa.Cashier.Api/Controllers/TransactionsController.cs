// Filename: TransactionsController.cs
// Date Created: 2019-05-20
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

using JetBrains.Annotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/transactions")]
    [ApiExplorerSettings(GroupName = "Transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionQueries _transactionQueries;

        public TransactionsController(ITransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }

        /// <summary>
        ///     Get transactions by account currency.
        /// </summary>
        [HttpGet(Name = nameof(GetTransactionsAsync))]
        public async Task<IActionResult> GetTransactionsAsync(
            [FromQuery] [CanBeNull]
            AccountCurrency currency
        )
        {
            currency = currency ?? AccountCurrency.All;

            var transactions = await _transactionQueries.GetTransactionsAsync(currency);

            if (!transactions.Any())
            {
                return this.NoContent();
            }

            return this.Ok(transactions);
        }
    }
}
