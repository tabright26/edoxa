// Filename: TransactionsController.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Grpc.Protos.Cashier.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/transactions")]
    [ApiExplorerSettings(GroupName = "Transactions")]
    public sealed class TransactionsController : ControllerBase
    {
        private readonly ITransactionQuery _transactionQuery;

        public TransactionsController(ITransactionQuery transactionQuery)
        {
            _transactionQuery = transactionQuery;
        }

        [HttpGet]
        [SwaggerOperation("Get transactions by currency, type and status.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TransactionDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(Currency? currency = null, TransactionType? type = null, TransactionStatus? status = null)
        {
            var responses = await _transactionQuery.FetchUserTransactionResponsesAsync(currency, type, status);

            if (!responses.Any())
            {
                return this.NoContent();
            }

            return this.Ok(responses);
        }
    }
}
