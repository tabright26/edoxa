// Filename: DepositCommandHandler.cs
// Date Created: 2019-05-29
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

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.Services.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Security.Extensions;

using FluentValidation.Results;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class DepositCommandHandler : ICommandHandler<DepositCommand, Either<ValidationResult, TransactionDTO>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DepositCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [ItemNotNull]
        public async Task<Either<ValidationResult, TransactionDTO>> Handle([NotNull] DepositCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

            var either = await _accountService.DepositAsync(user.Id, _mapper.Map<Currency>(command.Currency), cancellationToken);

            return either.Match<Either<ValidationResult, TransactionDTO>>(result => result, transaction => _mapper.Map<TransactionDTO>(transaction));
        }
    }
}
