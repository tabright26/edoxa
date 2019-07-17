// Filename: TransactionsController.cs
// Date Created: 2019-06-25
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

using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Transaction.Controllers
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
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionViewModel>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(Currency currency = null, TransactionType type = null, TransactionStatus status = null)
        {
            var transactionViewModels = await _transactionQuery.FindUserTransactionViewModelsAsync(currency, type, status);

            if (!transactionViewModels.Any())
            {
                return this.NoContent();
            }

            return this.Ok(transactionViewModels);
        }
    }
}
