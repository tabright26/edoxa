// Filename: TransactionsController.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
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
    [Route("api/transactions")]
    [ApiExplorerSettings(GroupName = "Transaction")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionQuery _transactionQuery;

        public TransactionsController(ITransactionQuery transactionQuery)
        {
            _transactionQuery = transactionQuery;
        }

        /// <summary>
        ///     Get transactions by currency, type and status.
        /// </summary>
        [HttpGet(Name = nameof(GetTransactionsAsync))]
        public async Task<IActionResult> GetTransactionsAsync(CurrencyType currency = null, TransactionType type = null, TransactionStatus status = null)
        {
            var transactions = await _transactionQuery.GetTransactionsAsync(currency, type, status);

            if (!transactions.Any())
            {
                return this.NoContent();
            }

            return this.Ok(transactions);
        }
    }
}
