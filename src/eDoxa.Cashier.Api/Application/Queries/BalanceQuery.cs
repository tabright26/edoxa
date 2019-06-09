// Filename: BalanceQuery.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.Application.Abstractions;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Security.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Queries
{
    public sealed partial class BalanceQuery
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BalanceQuery(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }

    public sealed partial class BalanceQuery : IBalanceQuery
    {
        [ItemCanBeNull]
        public async Task<BalanceViewModel> GetBalanceAsync(CurrencyType currencyType)
        {
            var userId = _httpContextAccessor.GetUserId();

            var balance = await _accountRepository.GetBalanceAsNoTrackingAsync(userId, currencyType);

            return _mapper.Map<BalanceViewModel>(balance);
        }
    }
}
