// Filename: TransactionsController.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Seedwork.Application.Extensions;

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
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionQuery transactionQuery, IMapper mapper)
        {
            _transactionQuery = transactionQuery;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Get transactions by currency, type and status.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TransactionDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(Currency? currency = null, TransactionType? type = null, TransactionStatus? status = null)
        {
            var userId = HttpContext.GetUserId();

            var responses = await _transactionQuery.FetchUserTransactionsAsync(userId, currency, type, status);

            if (!responses.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IReadOnlyCollection<TransactionDto>>(responses));
        }
    }
}
