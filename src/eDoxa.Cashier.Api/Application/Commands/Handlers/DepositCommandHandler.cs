// Filename: DepositCommandHandler.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class DepositCommandHandler : AsyncCommandHandler<DepositCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public DepositCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService,
            IMapper mapper
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _mapper = mapper;
        }

        protected override async Task Handle([NotNull] DepositCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var customerId = _httpContextAccessor.GetCustomerId();

            await _accountService.DepositAsync(customerId, userId, _mapper.Map<ICurrency>(command.Currency), cancellationToken);
        }
    }
}
