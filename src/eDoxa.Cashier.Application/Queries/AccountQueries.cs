﻿// Filename: AccountQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class AccountQueries
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AccountQueries(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }

    public sealed partial class AccountQueries : IAccountQueries
    {
        public async Task<Option<BalanceDTO>> GetBalanceAsync(CurrencyType currencyType)
        {
            var userId = _httpContextAccessor.GetUserId();

            var balance = await _accountRepository.GetBalanceAsNoTrackingAsync(userId, currencyType);

            return balance != null ? new Option<BalanceDTO>(_mapper.Map<BalanceDTO>(balance)) : new Option<BalanceDTO>();
        }
    }
}
