﻿// Filename: BalanceController.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
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
    [Route("api/balance")]
    [ApiExplorerSettings(GroupName = "Balance")]
    public sealed class BalanceController : ControllerBase
    {
        private readonly IAccountQuery _accountQuery;

        public BalanceController(IAccountQuery accountQuery)
        {
            _accountQuery = accountQuery;
        }

        [HttpGet("{currency}", Name = "Test")]
        [SwaggerOperation("Get account balance by currency.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BalanceDto))]
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