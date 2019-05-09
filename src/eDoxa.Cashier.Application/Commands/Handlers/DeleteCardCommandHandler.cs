// Filename: DeleteCardCommandHandler.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DeleteCardCommandHandler : ICommandHandler<DeleteCardCommand, IActionResult>
    {
        private readonly CardService _cardService;
        private readonly IUserInfoService _userInfoService;

        public DeleteCardCommandHandler(IUserInfoService userInfoService, CardService cardService)
        {
            _userInfoService = userInfoService;
            _cardService = cardService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var customerId = CustomerId.Parse(_userInfoService.CustomerId);

            await _cardService.DeleteAsync(customerId.ToString(), command.CardId.ToString(), cancellationToken: cancellationToken);

            return new OkResult();
        }
    }
}