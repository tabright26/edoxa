﻿// Filename: TransactionQueries.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class TransactionQueries
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TransactionQueries(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private async Task<TransactionListDTO> GetTransactionsAsync(
            UserId userId,
            CurrencyType currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            var account = await _accountRepository.GetTransactionsAsNoTrackingAsync(userId);

            var transactions = account
                .Where(transaction => transaction.Currency.Type.HasFilter(currency) && transaction.Type.HasFilter(type) && transaction.Status.HasFilter(status))
                .OrderBy(transaction => transaction.Currency.Type)
                .ThenByDescending(transaction => transaction.Timestamp)
                .ToList();

            return _mapper.Map<TransactionListDTO>(transactions);
        }
    }

    public sealed partial class TransactionQueries : ITransactionQueries
    {
        public async Task<TransactionListDTO> GetTransactionsAsync(CurrencyType currency = null, TransactionType type = null, TransactionStatus status = null)
        {
            var userId = _httpContextAccessor.GetUserId();

            return await this.GetTransactionsAsync(userId, currency, type, status);
        }
    }
}
