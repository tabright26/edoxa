﻿// Filename: WithdrawalHandler.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

//TODO: Check to remove this.

//using System.Threading;
//using System.Threading.Tasks;

//using eDoxa.Cashier.Api.Extensions;
//using eDoxa.Cashier.Domain.AggregateModels;
//using eDoxa.Cashier.Domain.Services;

//using MediatR;

//using Microsoft.AspNetCore.Http;

//namespace eDoxa.Cashier.Api.Application.Requests.Handlers
//{
//    public sealed class WithdrawalRequestHandler : AsyncRequestHandler<WithdrawalRequest>
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IAccountService _accountService;

//        public WithdrawalRequestHandler(IHttpContextAccessor httpContextAccessor, IAccountService accountService)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _accountService = accountService;
//        }

//        protected override async Task Handle( WithdrawalRequest request, CancellationToken cancellationToken)
//        {
//            var userId = _httpContextAccessor.GetUserId();

//            var connectAccountId = _httpContextAccessor.GetConnectAccountId()!;

//            await _accountService.WithdrawalAsync(connectAccountId, userId, new Money(request.Amount), cancellationToken);
//        }
//    }
//}
