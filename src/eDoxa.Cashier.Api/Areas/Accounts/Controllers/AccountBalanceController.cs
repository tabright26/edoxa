// Filename: AccountBalanceController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Responses;
using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Accounts.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BalanceResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByCurrencyAsync(Currency currency)
        {
            var response = await _accountQuery.FindUserBalanceResponseAsync(currency);

            if (response == null)
            {
                return this.NotFound($"Account balance for currency {currency} not found.");
            }

            return this.Ok(response);
        }
    }
}
