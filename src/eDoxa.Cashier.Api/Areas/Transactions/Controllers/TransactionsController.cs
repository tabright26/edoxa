// Filename: TransactionsController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Transactions.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/transactions")]
    [ApiExplorerSettings(GroupName = "Transactions")]
    public sealed class TransactionsController : ControllerBase
    {
        private readonly ITransactionQuery _transactionQuery;
        //private readonly IAccountService _accountService;
        //private readonly IMapper _mapper;

        public TransactionsController(ITransactionQuery transactionQuery/*, IAccountService accountService, IMapper mapper*/)
        {
            _transactionQuery = transactionQuery;
            //_accountService = accountService;
            //_mapper = mapper;
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

        //[HttpPost]
        //[SwaggerOperation("Create a transaction.")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        //[SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        //public async Task<IActionResult> PostAsync([FromBody] CreateTransactionRequest request)
        //{
        //    var account = await _accountService.FindAccountAsync(HttpContext.GetUserId());

        //    if (account == null)
        //    {
        //        return this.NotFound("User account not found.");
        //    }

        //    var result = await _accountService.CreateTransactionAsync(
        //        account,
        //        request.Amount,
        //        Currency.FromName(request.Currency),
        //        TransactionType.FromName(request.Type),
        //        new TransactionMetadata(request.Metadata));

        //    if (result.IsValid)
        //    {
        //        return this.Ok(_mapper.Map<TransactionResponse>(result.GetEntityFromMetadata<ITransaction>()));
        //    }

        //    result.AddToModelState(ModelState);

        //    return this.BadRequest(new ValidationProblemDetails(ModelState));
        //}
    }
}
