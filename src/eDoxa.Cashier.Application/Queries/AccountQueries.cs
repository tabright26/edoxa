// Filename: AccountQueries.cs
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
        private readonly IMoneyAccountRepository _moneyAccountRepository;
        private readonly ITokenAccountRepository _tokenAccountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AccountQueries(IMoneyAccountRepository moneyAccountRepository, ITokenAccountRepository tokenAccountRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _moneyAccountRepository = moneyAccountRepository;
            _tokenAccountRepository = tokenAccountRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }

    public sealed partial class AccountQueries : IAccountQueries
    {
        public async Task<Option<AccountDTO>> GetAccountAsync(Currency currency)
        {
            var userId = _httpContextAccessor.GetUserId();

            AccountDTO mapper = null;

            if (currency == Currency.Money)
            {
                var account = await _moneyAccountRepository.GetMoneyAccountAsNoTrackingAsync(userId);

                mapper = _mapper.Map<AccountDTO>(account);
            }

            if (currency == Currency.Token)
            {
                var account = await _tokenAccountRepository.GetTokenAccountAsNoTrackingAsync(userId);

                mapper = _mapper.Map<AccountDTO>(account);
            }

            return mapper != null ? new Option<AccountDTO>(mapper) : new Option<AccountDTO>();
        }
    }
}
