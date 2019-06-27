// Filename: TransactionQuery.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.Application.Abstractions;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Queries
{
    public sealed partial class TransactionQuery
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TransactionQuery(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }

    public sealed partial class TransactionQuery : ITransactionQuery
    {
        public async Task<IReadOnlyCollection<TransactionViewModel>> GetTransactionsAsync(
            CurrencyType currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            var userId = _httpContextAccessor.GetUserId();

            var account = await _accountRepository.GetTransactionsAsNoTrackingAsync(userId);

            var transactions = account
                .Where(transaction => transaction.Currency.Type.HasFilter(currency) && transaction.Type.HasFilter(type) && transaction.Status.HasFilter(status))
                .OrderBy(transaction => transaction.Currency.Type)
                .ThenByDescending(transaction => transaction.Timestamp)
                .ToList();

            return _mapper.Map<IReadOnlyCollection<TransactionViewModel>>(transactions);
        }
    }
}
