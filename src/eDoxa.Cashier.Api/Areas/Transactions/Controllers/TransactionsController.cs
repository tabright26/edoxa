// Filename: TransactionsController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Transactions.Responses;
using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Transactions.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionResponse>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> GetAsync(Currency? currency = null, TransactionType? type = null, TransactionStatus? status = null)
        {
            var responses = await _transactionQuery.FindUserTransactionResponsesAsync(currency, type, status);

            if (!responses.Any())
            {
                return this.NoContent();
            }

            return this.Ok(responses);
        }
    }
}
