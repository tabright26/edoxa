// Filename: TransactionQueries.cs
// Date Created: 2019-05-16
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

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Entities;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class TransactionQueries
    {
        private readonly IMoneyAccountRepository _moneyAccountRepository;
        private readonly ITokenAccountRepository _tokenAccountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TransactionQueries(IMoneyAccountRepository moneyAccountRepository, ITokenAccountRepository tokenAccountRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _moneyAccountRepository = moneyAccountRepository;
            _tokenAccountRepository = tokenAccountRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private async Task<TransactionListDTO> GetMoneyTransactionsAsync(UserId userId)
        {
            var account = await _moneyAccountRepository.GetMoneyAccountAsNoTrackingAsync(userId);

            return _mapper.Map<TransactionListDTO>(account.Transactions);
        }

        private async Task<TransactionListDTO> GetTokenTransactionsAsync(UserId userId)
        {
            var account = await _tokenAccountRepository.GetTokenAccountAsNoTrackingAsync(userId);

            return _mapper.Map<TransactionListDTO>(account.Transactions);
        }
    }

    public sealed partial class TransactionQueries : ITransactionQueries
    {
        public async Task<TransactionListDTO> GetTransactionsAsync([CanBeNull] Currency currency)
        {
            var userId = _httpContextAccessor.GetUserId();

            var transactions = new TransactionListDTO();

            if (currency == null || currency == Currency.Money)
            {
                transactions.Items.AddRange(await this.GetMoneyTransactionsAsync(userId));
            }

            if (currency == null || currency == Currency.Token)
            {
                transactions.Items.AddRange(await this.GetTokenTransactionsAsync(userId));
            }

            return transactions.OrderBy(transaction => transaction.Currency).ThenByDescending(transaction => transaction.Timestamp).ToList();
        }
    }
}
