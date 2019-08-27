// Filename: DepositRequestHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

//TODO: Check to remove this.

//using System;
//using System.Threading;
//using System.Threading.Tasks;

//using eDoxa.Cashier.Api.Extensions;
//using eDoxa.Cashier.Domain.AggregateModels;
//using eDoxa.Cashier.Domain.Services;

//using MediatR;

//using Microsoft.AspNetCore.Http;

//namespace eDoxa.Cashier.Api.Application.Requests.Handlers
//{
//    public sealed class DepositRequestHandler : AsyncRequestHandler<DepositRequest>
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IAccountService _accountService;

//        public DepositRequestHandler(IHttpContextAccessor httpContextAccessor, IAccountService accountService)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _accountService = accountService;
//        }

//        protected override async Task Handle( DepositRequest request, CancellationToken cancellationToken)
//        {
//            var userId = _httpContextAccessor.GetUserId();

//            var customerId = _httpContextAccessor.GetCustomerId()!;

//            await _accountService.DepositAsync(customerId, userId, MapCurrency(request.Currency, request.Amount), cancellationToken);
//        }

//        // TODO: Must be Currency static method...
//        private static ICurrency MapCurrency(string currency, decimal amount)
//        {
//            if (Currency.FromName(currency) == Currency.Money)
//            {
//                return new Money(amount);
//            }

//            if (Currency.FromName(currency) == Currency.Token)
//            {
//                return new Token(amount);
//            }

//            throw new InvalidOperationException();
//        }
//    }
//}
